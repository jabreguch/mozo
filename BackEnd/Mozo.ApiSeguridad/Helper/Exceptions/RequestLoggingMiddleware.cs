using System.Diagnostics;

namespace Mozo.ApiSeguridad.Helper.Exceptions;

/// <summary>
/// Middleware para logging de requests HTTP
/// </summary>
public class RequestLoggingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<RequestLoggingMiddleware> _logger;

    public RequestLoggingMiddleware(RequestDelegate next, ILogger<RequestLoggingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var stopwatch = Stopwatch.StartNew();
        var traceId = Activity.Current?.Id ?? context.TraceIdentifier;

        // Obtener información del usuario
        var userId = context.User?.FindFirst("NoUsuario")?.Value ?? "Anonymous";
        var coEmpresa = context.User?.FindFirst("CoEmpresa")?.Value ?? "N/A";
        var userIp = context.Connection.RemoteIpAddress?.ToString() ?? "Unknown";
        var method = context.Request.Method;
        var path = context.Request.Path;

        // ✅ Log de request entrante (solo para endpoints de API, no para Swagger)
        if (!path.StartsWithSegments("/swagger") && !path.StartsWithSegments("/_framework"))
        {
            _logger.LogInformation(
                "==> {Method} {Path} | Usuario: {Usuario} | Empresa: {Empresa} | IP: {IP} | TraceId: {TraceId}",
                method, path, userId, coEmpresa, userIp, traceId);
        }

        try
        {
            await _next(context);
        }
        finally
        {
            stopwatch.Stop();

            // ✅ Log de respuesta (solo para endpoints de API)
            if (!path.StartsWithSegments("/swagger") && !path.StartsWithSegments("/_framework"))
            {
                var logLevel = context.Response.StatusCode switch
                {
                    >= 500 => LogLevel.Error,
                    >= 400 => LogLevel.Warning,
                    _ => LogLevel.Information
                };

                _logger.Log(logLevel,
                    "<== {Method} {Path} | Status: {StatusCode} | Duración: {Duration}ms | Usuario: {Usuario} | TraceId: {TraceId}",
                    method, path, context.Response.StatusCode, stopwatch.ElapsedMilliseconds, userId, traceId);
            }
        }
    }
}