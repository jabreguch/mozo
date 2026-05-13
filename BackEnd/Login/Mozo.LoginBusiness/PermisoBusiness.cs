using Mozo.Helper.Global;
using Mozo.LoginData;
using Mozo.Model.Seguridad;

namespace Mozo.LoginBusiness;

public interface IPermisoBusiness
{
    Task<PermisoModel?> SelByUserAsync(PermisoFilterDto c);
    Task<PermisoModel?> SelByIdAsync(PermisoFilterDto c);
    Task UpdateLanguageAsync(PermisoModel c);
}
public class PermisoBusiness : IPermisoBusiness
{
    private readonly IPermisoData _data;
    public PermisoBusiness(IPermisoData data, UserContext user)
    {
        _data = data;
    }

    public async Task<PermisoModel?> SelByIdAsync(PermisoFilterDto c) => await _data.SelByIdAsync(c);
    public async Task<PermisoModel?> SelByUserAsync(PermisoFilterDto c) => await _data.SelByUserAsync(c);
    public async Task UpdateLanguageAsync(PermisoModel c) => await _data.UpdateLanguageAsync(c);


}
