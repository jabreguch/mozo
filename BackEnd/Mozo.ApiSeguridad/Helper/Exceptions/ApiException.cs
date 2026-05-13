namespace Mozo.ApiSeguridad.HelperWeb.Exceptions;

/// <summary>
/// Excepción base para la API
/// </summary>
public class ApiException : Exception
{
    public string Code { get; set; }
    public int StatusCode { get; set; }
    public List<ErrorDetail>? Errors { get; set; }

    public ApiException(
        string message,
        string code = "500",
        int statusCode = 500,
        List<ErrorDetail>? errors = null)
        : base(message)
    {
        Code = code ?? throw new ArgumentNullException(nameof(code));
        StatusCode = statusCode;
        Errors = errors ?? new List<ErrorDetail>();
    }
}

/// <summary>
/// Excepción de validación
/// </summary>
public class ValidationException : ApiException
{
    public ValidationException(string message, List<ErrorDetail>? errors = null)
        : base(message, "400", 400, errors ?? new List<ErrorDetail>())
    {
    }

    public ValidationException(string field, string message)
        : base($"Error de validación en {field}", "400", 400,
            new List<ErrorDetail> { ErrorDetail.Validation(field, message) })
    {
    }
}

/// <summary>
/// Excepción de no autorizado
/// </summary>
public class UnauthorizedException : ApiException
{
    public UnauthorizedException(string message = "No autorizado")
        : base(message, "401", 401)
    {
    }
}

/// <summary>
/// Excepción de acceso denegado
/// </summary>
public class ForbiddenException : ApiException
{
    public ForbiddenException(string message = "Acceso denegado")
        : base(message, "403", 403)
    {
    }
}

/// <summary>
/// Excepción de recurso no encontrado
/// </summary>
public class NotFoundException : ApiException
{
    public NotFoundException(string message = "Recurso no encontrado")
        : base(message, "404", 404)
    {
    }
}

/// <summary>
/// Excepción de error interno
/// </summary>
public class InternalServerException : ApiException
{
    public InternalServerException(string message = "Error interno del servidor")
        : base(message, "500", 500)
    {
    }
}

