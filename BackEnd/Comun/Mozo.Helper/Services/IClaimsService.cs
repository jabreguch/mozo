using System.Security.Claims;

namespace Mozo.Helper.Services;

public interface IClaimsService
{
    ClaimsPrincipal? User { get; }
    bool IsAuthenticated { get; }
    int? GetInt32(string claimType);
    string? GetString(string claimType);
}
