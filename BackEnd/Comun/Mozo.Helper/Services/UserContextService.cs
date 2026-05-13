using Mozo.Helper.Global;

namespace Mozo.Helper.Services;

public class UserContextService
{
    private readonly IClaimsService _claimsService;

    public UserContextService(IClaimsService claimsService)
    {
        _claimsService = claimsService ?? throw new ArgumentNullException(nameof(claimsService));
    }

    public UserContext Create()
    {
        return new UserContext(_claimsService);
    }
}
