using Mozo.ApiSeguridad.HelperWeb.Exceptions;

namespace Mozo.ApiSeguridad.Helper.Response;

/// <summary>
/// Respuesta estándar de la API
/// </summary>
public class ApiResponse<T>
{
    public bool Success { get; set; }
    public string Message { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty;
    public T? Data { get; set; }
    public List<ErrorDetail>? Errors { get; set; }
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;
}

/// <summary>
/// Respuesta sin datos genéricos
/// </summary>
public class ApiResponse
{
    public bool Success { get; set; }
    public string Message { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty;
    public List<ErrorDetail>? Errors { get; set; }
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;
}