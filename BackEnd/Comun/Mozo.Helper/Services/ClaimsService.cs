using Microsoft.AspNetCore.Http;

using System.Security.Claims;

namespace Mozo.Helper.Services;

public class ClaimsService : IClaimsService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public ClaimsService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public ClaimsPrincipal? User => _httpContextAccessor.HttpContext?.User;

    public bool IsAuthenticated => User?.Identity?.IsAuthenticated == true;

    public int? GetInt32(string claimType)
    {
        string? value = GetString(claimType);
        return int.TryParse(value, out int result) ? result : null;
    }

    public string? GetString(string claimType)
    {
        if (string.IsNullOrWhiteSpace(claimType))
            return null;

        return User?.FindFirst(claimType)?.Value;
    }
}
