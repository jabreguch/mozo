using Mozo.Model.Seguridad;
using Mozo.SeguridadData;

namespace Mozo.SeguridadBusiness;

public interface IPerfilBusiness
{
    Task<int> InsertAsync(PerfilModel c);
    Task UpdateAsync(PerfilModel c);
    Task UpdateStateAsync(PerfilModel c);
    Task DeleteByIdAsync(PerfilFilterDto c);
    Task<PerfilModel?> SelByIdAsync(PerfilFilterDto c);
    Task<IEnumerable<PerfilModel>> SelAllAsync(PerfilFilterDto c);
    Task<IEnumerable<PerfilModel>> SelAllActiveAsync(PerfilFilterDto c);
    Task<PerfilModel?> SelDefaultAsync(PerfilFilterDto c);
}
public class PerfilBusiness : IPerfilBusiness
{
    private readonly IPerfilData _data;
    public PerfilBusiness(IPerfilData data)
    {
        _data = data;
    }
    public async Task<int> InsertAsync(PerfilModel c) => await _data.InsertAsync(c);
    public async Task UpdateAsync(PerfilModel c) => await _data.UpdateAsync(c);
    public async Task UpdateStateAsync(PerfilModel c) => await _data.UpdateStateAsync(c);
    public async Task DeleteByIdAsync(PerfilFilterDto c) => await _data.DeleteByIdAsync(c);
    public async Task<IEnumerable<PerfilModel>> SelAllAsync(PerfilFilterDto c)
    {
        var r = await _data.SelAllAsync(c);
        return r.OrderBy(x => x.NoPerfil);
    }
    public async Task<IEnumerable<PerfilModel>> SelAllActiveAsync(PerfilFilterDto c)
    {
        var r = await _data.SelAllActiveAsync(c);
        return r.OrderBy(x => x.NoPerfil);
    }
    public async Task<PerfilModel?> SelByIdAsync(PerfilFilterDto c) => await _data.SelByIdAsync(c);
    public async Task<PerfilModel?> SelDefaultAsync(PerfilFilterDto c) => await _data.SelDefaultAsync(c);
}