using Mozo.ApiSeguridad.HelperWeb.Exceptions;

using Npgsql;
using NpgsqlTypes;
using System.Diagnostics;

namespace Mozo.ApiSeguridad.Helper.Exceptions;

/// <summary>
/// Extensiones para ProblemDetails
/// </summary>
public static class ProblemDetailsExtensions
{
    /// <summary>
    /// 
    /// </summary>
    public static IServiceCollection AddCustomProblemDetails(this IServiceCollection services)
    {
        services.AddProblemDetails(options =>
        {
            options.CustomizeProblemDetails = context =>
            {
                var httpContext = context.HttpContext;
                var exception = context.Exception;

                if (exception == null)
                    return;

                var problemDetails = context.ProblemDetails;
                var traceId = Activity.Current?.Id ?? httpContext.TraceIdentifier;

                // ✅ Obtener logger del contexto
                var loggerFactory = httpContext.RequestServices.GetRequiredService<ILoggerFactory>();
                var logger = loggerFactory.CreateLogger("Mozo.Api.ExceptionHandler");

                // ✅ Obtener información del usuario para logging
                var userId = httpContext.User?.FindFirst("NoUsuario")?.Value ?? "Anonymous";
                var coEmpresa = httpContext.User?.FindFirst("CoEmpresa")?.Value ?? "N/A";
                var userIp = httpContext.Connection.RemoteIpAddress?.ToString() ?? "Unknown";
                var path = httpContext.Request.Path;
                var method = httpContext.Request.Method;

                // ✅ Determinar nivel de log y configurar problem details
                LogLevel logLevel;

                switch (exception)
                {
                    case ValidationException validEx:
                        logLevel = LogLevel.Warning;
                        SetProblemDetails(problemDetails, StatusCodes.Status400BadRequest,
                            "Validación fallida", validEx.Message, validEx.Code, validEx.Errors);
                        // ✅ Log específico para validación
                        logger.LogWarning(
                            "[VALIDATION] {Method} {Path} | Usuario: {UserId} | Empresa: {CoEmpresa} | IP: {IP} | TraceId: {TraceId} | Errors: {@Errors}",
                            method, path, userId, coEmpresa, userIp, traceId, validEx.Errors);
                        break;

                    case UnauthorizedException unAuthEx:
                        logLevel = LogLevel.Warning;
                        SetProblemDetails(problemDetails, StatusCodes.Status401Unauthorized,
                            "No autorizado", unAuthEx.Message, "401");
                        // ✅ Log de seguridad
                        logger.LogWarning(
                            "[SECURITY] UNAUTHORIZED {Method} {Path} | Usuario: {UserId} | IP: {IP} | TraceId: {TraceId} | Mensaje: {Message}",
                            method, path, userId, userIp, traceId, unAuthEx.Message);
                        break;

                    case ForbiddenException forbidEx:
                        logLevel = LogLevel.Warning;
                        SetProblemDetails(problemDetails, StatusCodes.Status403Forbidden,
                            "Acceso denegado", forbidEx.Message, "403");
                        // ✅ Log de seguridad
                        logger.LogWarning(
                            "[SECURITY] FORBIDDEN {Method} {Path} | Usuario: {UserId} | Empresa: {CoEmpresa} | IP: {IP} | TraceId: {TraceId} | Mensaje: {Message}",
                            method, path, userId, coEmpresa, userIp, traceId, forbidEx.Message);
                        break;

                    case NotFoundException notFoundEx:
                        logLevel = LogLevel.Information;
                        SetProblemDetails(problemDetails, StatusCodes.Status404NotFound,
                            "Recurso no encontrado", notFoundEx.Message, "404");
                        logger.LogInformation(
                            "[NOT_FOUND] {Method} {Path} | Usuario: {UserId} | TraceId: {TraceId} | Mensaje: {Message}",
                            method, path, userId, traceId, notFoundEx.Message);
                        break;

                    case ArgumentNullException argEx:
                        logLevel = LogLevel.Warning;
                        SetProblemDetails(problemDetails, StatusCodes.Status400BadRequest,
                            "Parámetro requerido",
                            $"Parámetro no proporcionado: {argEx.ParamName}", "400");
                        problemDetails.Extensions["field"] = argEx.ParamName;
                        logger.LogWarning(
                             "[ARGUMENT_NULL] {Method} {Path} | Usuario: {UserId} | TraceId: {TraceId} | Parámetro: {ParamName}",
                             method, path, userId, traceId, argEx.ParamName);
                        break;

                    case InvalidOperationException invEx:
                        logLevel = LogLevel.Warning;
                        SetProblemDetails(problemDetails, StatusCodes.Status400BadRequest,
                            "Operación inválida", invEx.Message, "400");
                        break;
                    case NpgsqlException npgsqEx:
                        logLevel = LogLevel.Error;
                        string dbMessage = GetSafeDbErrorMessage(npgsqEx);
                        SetProblemDetails(problemDetails, StatusCodes.Status500InternalServerError,
                           "Error de base de datos", dbMessage, "DB_ERROR");

                        // ✅ Log de error de BD (con detalles completos en el log, mensaje genérico al usuario)
                        logger.LogError(npgsqEx,
                            "[DATABASE_ERROR] {Method} {Path} | Usuario: {UserId} | Empresa: {CoEmpresa} | TraceId: {TraceId} | SqlState: {SqlState}",
                            method, path, userId, coEmpresa, traceId, npgsqEx.SqlState);
                        break;

                    case ApiException apiEx:
                        logLevel = apiEx.StatusCode >= 500 ? LogLevel.Error : LogLevel.Warning;
                        SetProblemDetails(problemDetails, apiEx.StatusCode,
                            "Error en la API", apiEx.Message, apiEx.Code, apiEx.Errors);
                        logger.Log(logLevel, apiEx,
                              "[API_EXCEPTION] {Method} {Path} | Usuario: {UserId} | TraceId: {TraceId} | StatusCode: {StatusCode} | Code: {Code}",
                              method, path, userId, traceId, apiEx.StatusCode, apiEx.Code);
                        break;

                    default:
                        logLevel = LogLevel.Error;
                        SetProblemDetails(problemDetails, StatusCodes.Status500InternalServerError,
                            "Error interno del servidor", "Ocurrió un error inesperado", "500");
                        // ✅ Log de error inesperado (CRÍTICO)
                        logger.LogError(exception,
                            "[UNHANDLED_EXCEPTION] {Method} {Path} | Usuario: {UserId} | Empresa: {CoEmpresa} | IP: {IP} | TraceId: {TraceId} | ExceptionType: {ExceptionType}",
                            method, path, userId, coEmpresa, userIp, traceId, exception.GetType().Name);
                        break;
                }

                // Agregar timestamp
                problemDetails.Extensions["timestamp"] = DateTime.UtcNow;

                // Agregar trace ID
              
                if (!string.IsNullOrEmpty(traceId))
                {
                    problemDetails.Extensions["traceId"] = traceId;
                }
                // ✅ En desarrollo, agregar más detalles
                var environment = httpContext.RequestServices.GetRequiredService<IWebHostEnvironment>();
                if (environment.IsDevelopment() && exception != null)
                {
                    problemDetails.Extensions["exceptionType"] = exception.GetType().Name;
                    problemDetails.Extensions["stackTrace"] = exception.StackTrace;

                    if (exception.InnerException != null)
                    {
                        problemDetails.Extensions["innerException"] = exception.InnerException.Message;
                    }
                }
            };
        });

        return services;
    }

    /// <summary>
    /// Helper para establecer los detalles del problema
    /// </summary>
    private static void SetProblemDetails(
        Microsoft.AspNetCore.Mvc.ProblemDetails problemDetails,
        int statusCode,
        string title,
        string detail,
        string code,
        List<ErrorDetail>? errors = null)
    {
        problemDetails.Status = statusCode;
        problemDetails.Title = title;
        problemDetails.Detail = detail;
        problemDetails.Extensions["code"] = code;
        problemDetails.Extensions["status"] = statusCode; // ✅ SIEMPRE AGREGAR

        if (errors?.Any() == true)
        {
            problemDetails.Extensions["errors"] = errors
                .Select(e => new
                {
                    e.Field,
                    e.Message,
                    e.Code
                }).ToList();
        }
    }

    private static string GetSafeDbErrorMessage(NpgsqlException ex)
    {
        // ✅ Mapear códigos SQL comunes a mensajes amigables
        return ex.SqlState switch
        {
            "23505" => "El registro ya existe (violación de llave única)",
            "23503" => "No se puede eliminar porque está siendo usado por otros registros",
            "23502" => "Falta un campo requerido",
            "42P01" => "Tabla no encontrada",
            "42703" => "Columna no encontrada",
            _ => "Error al procesar la operación en la base de datos"
        };
    }

}