namespace Mozo.ApiSeguridad.Helper;


[AttributeUsage(AttributeTargets.Parameter, AllowMultiple = false)]
public sealed class FromClaimsAttribute : Attribute
{
    public string ClaimType { get; }
    public FromClaimsAttribute(string claimType) => ClaimType = claimType;
}


public static class ClaimsBinder
{
    public static ValueTask<T?> BindAsync<T>(HttpContext context, string claimType)
    {
        string? value = context.User.FindFirst(claimType)?.Value;
        if (value == null) return ValueTask.FromResult<T?>(default);

        Type targetType = Nullable.GetUnderlyingType(typeof(T)) ?? typeof(T);
        object? converted = Convert.ChangeType(value, targetType);
        return ValueTask.FromResult((T?)converted);
    }
}

public record UserClaims
{
    public static ValueTask<UserClaims> BindAsync(HttpContext context)
    {
        string? coPersona = context.User.FindFirst("CoPersona")?.Value ?? null;
        string? coEmpresa = context.User.FindFirst("CoEmpresa")?.Value ?? null;


        return ValueTask.FromResult(new UserClaims
        {
            CoPersona = coPersona != null ? int.Parse(coPersona) : null,
            CoEmpresa = coEmpresa != null ? int.Parse(coEmpresa) : null,
        });
    }

    public int? CoPersona { get; set; }
    public int? CoEmpresa { get; set; }
}