namespace Mozo.ApiSeguridad.HelperWeb.Exceptions;

/// <summary>
/// Detalle de error
/// </summary>
public class ErrorDetail
{
    public string Field { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty;

    public ErrorDetail() { }

    public ErrorDetail(string field, string message, string code = "400")
    {
        Field = field ?? throw new ArgumentNullException(nameof(field));
        Message = message ?? throw new ArgumentNullException(nameof(message));
        Code = code ?? throw new ArgumentNullException(nameof(code));
    }

    /// <summary>
    /// Crea un ErrorDetail de validación
    /// </summary>
    public static ErrorDetail Validation(string field, string message)
    {
        return new(field, message, "400");
    }

    /// <summary>
    /// Crea un ErrorDetail de autorización
    /// </summary>
    public static ErrorDetail Unauthorized(string message)
    {
        return new("Authorization", message, "401");
    }

    /// <summary>
    /// Crea un ErrorDetail de no encontrado
    /// </summary>
    public static ErrorDetail NotFound(string message)
    {
        return new("Resource", message, "404");
    }

    public override string ToString() => $"{Field}: {Message} ({Code})";
}