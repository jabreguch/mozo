using Mozo.ApiSeguridad.HelperWeb.Exceptions;

using Npgsql;

namespace Mozo.ApiSeguridad.Helper.Exceptions;

/// <summary>
/// Extensiones para ProblemDetails
/// </summary>
public static class ProblemDetailsExtensions
{
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

                switch (exception)
                {
                    case ValidationException validEx:
                        SetProblemDetails(problemDetails, StatusCodes.Status400BadRequest,
                            "Validación fallida", validEx.Message, validEx.Code, validEx.Errors);
                        break;

                    case UnauthorizedException unAuthEx:
                        SetProblemDetails(problemDetails, StatusCodes.Status401Unauthorized,
                            "No autorizado", unAuthEx.Message, "401");
                        break;

                    case ForbiddenException forbidEx:
                        SetProblemDetails(problemDetails, StatusCodes.Status403Forbidden,
                            "Acceso denegado", forbidEx.Message, "403");
                        break;

                    case NotFoundException notFoundEx:
                        SetProblemDetails(problemDetails, StatusCodes.Status404NotFound,
                            "Recurso no encontrado", notFoundEx.Message, "404");
                        break;

                    case ArgumentNullException argEx:
                        SetProblemDetails(problemDetails, StatusCodes.Status400BadRequest,
                            "Parámetro requerido",
                            $"Parámetro no proporcionado: {argEx.ParamName}", "400");
                        problemDetails.Extensions["field"] = argEx.ParamName;
                        break;

                    case InvalidOperationException invEx:
                        SetProblemDetails(problemDetails, StatusCodes.Status400BadRequest,
                            "Operación inválida", invEx.Message, "400");
                        break;
                    case NpgsqlException npgsqEx:
                        SetProblemDetails(problemDetails, StatusCodes.Status400BadRequest,
                            "Base de datos PostGress", npgsqEx.Message, "400");
                        break;
                    //
                    case ApiException apiEx:
                        SetProblemDetails(problemDetails, apiEx.StatusCode,
                            "Error en la API", apiEx.Message, apiEx.Code, apiEx.Errors);
                        break;

                    default:
                        SetProblemDetails(problemDetails, StatusCodes.Status500InternalServerError,
                            "Error interno del servidor", "Ocurrió un error inesperado", "500");
                        break;
                }

                // Agregar timestamp
                problemDetails.Extensions["timestamp"] = DateTime.UtcNow;

                // Agregar trace ID
                var traceId = httpContext.TraceIdentifier;
                if (!string.IsNullOrEmpty(traceId))
                {
                    problemDetails.Extensions["traceId"] = traceId;
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
}