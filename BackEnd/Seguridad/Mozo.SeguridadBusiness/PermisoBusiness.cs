using Mozo.Model.Seguridad;
using Mozo.SeguridadData;

namespace Mozo.SeguridadBusiness;

public interface IPermisoBusiness
{
    Task<int> InsertAsync(PermisoModel c);
    Task UpdateAsync(PermisoModel c);
    Task UpdateClaveAsync(PermisoModel c);
    Task UpdateStateAsync(PermisoModel c);
    Task<PermisoModel?> SelByIdAsync(PermisoFilterDto c);
}
public class PermisoBusiness : IPermisoBusiness
{
    private readonly IPermisoData _data;
    public PermisoBusiness(IPermisoData data)
    {
        _data = data;
    }
    public async Task<int> InsertAsync(PermisoModel c) => await _data.InsertAsync(c);
    public async Task UpdateAsync(PermisoModel c) => await _data.UpdateAsync(c);
    public async Task UpdateClaveAsync(PermisoModel c) => await _data.UpdateClaveAsync(c);
    public async Task UpdateStateAsync(PermisoModel c) => await _data.UpdateStateAsync(c);
    public async Task<PermisoModel?> SelByIdAsync(PermisoFilterDto c) => await _data.SelByIdAsync(c);

}