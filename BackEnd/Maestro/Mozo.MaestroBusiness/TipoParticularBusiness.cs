using Mozo.MaestroData;
using Mozo.Model.Maestro;

namespace Mozo.MaestroBusiness;

public interface ITipoParticularBusiness
{
    Task<int> InsertAsync(TipoParticularModel c);
    Task UpdateAsync(TipoParticularModel c);
    Task UpdateStateAsync(TipoParticularModel c);
    Task UpdateCommandAsync(TipoParticularModel c);
    Task UpdateDefaultAsync(TipoParticularModel c);
    Task DeleteByIdAsync(TipoParticularModel c);
    Task<IEnumerable<TipoParticularModel>> SelAllAsync(TipoParticularDto c);
    Task<int> SelAllCountAsync(TipoParticularDto c);
    Task<IEnumerable<TipoParticularModel>> SelAllHijoAsync(TipoParticularModel c);
    Task<IEnumerable<TipoParticularModel>> SelAllActiveHijoByGrupoAsync(TipoParticularModel c);
    Task<IEnumerable<TipoParticularModel>> SelAllGrupoByModuloAsync(TipoParticularModel c);
    Task<TipoParticularModel?> SelByIdAsync(TipoParticularModel c);
    Task<TipoParticularModel?> SelByIdGrupoAsync(TipoParticularModel c);
    Task<IEnumerable<TipoParticularModel>> SelAllActiveAsync(TipoParticularModel c);
    Task<IEnumerable<TipoParticularModel>> SelAllActiveHijoAsync(TipoParticularModel c);
    Task<int> SelOrdenNextAsync(TipoParticularModel c);
    Task<TipoParticularModel?> SelDefaultAsync(TipoParticularModel c);
}
public class TipoParticularBusiness : ITipoParticularBusiness
{
    private readonly ITipoParticularData _data;
    public TipoParticularBusiness(ITipoParticularData data)
    {
        _data = data;
    }

    public async Task<int> InsertAsync(TipoParticularModel c) => await _data.InsertAsync(c);
    public async Task UpdateCommandAsync(TipoParticularModel c) => await _data.UpdateCommandAsync(c);
    public async Task UpdateAsync(TipoParticularModel c) => await _data.UpdateAsync(c);
    public async Task UpdateStateAsync(TipoParticularModel c) => await _data.UpdateStateAsync(c);
    public async Task DeleteByIdAsync(TipoParticularModel c) => await _data.DeleteByIdAsync(c);
    public async Task<IEnumerable<TipoParticularModel>> SelAllAsync(TipoParticularDto c)
    {
        IEnumerable<TipoParticularModel> r = await _data.SelAllAsync(c);
        return r.OrderBy(s => s.NuOrden);
    }

    public async Task<int> SelAllCountAsync(TipoParticularDto c) => await _data.SelAllCountAsync(c);
    public async Task<IEnumerable<TipoParticularModel>> SelAllActiveAsync(TipoParticularModel c)
    {
        IEnumerable<TipoParticularModel> r = await _data.SelAllActiveAsync(c);
        return r.OrderBy(s => s.NuOrden).ToList();
    }
    public async Task<IEnumerable<TipoParticularModel>> SelAllActiveHijoAsync(TipoParticularModel c)
    {
        IEnumerable<TipoParticularModel> r = await _data.SelAllActiveHijoAsync(c);
        return r.OrderBy(s => s.NuOrden).ThenBy(s => s.NuSubOrden).ToList();
    }
    public async Task<IEnumerable<TipoParticularModel>> SelAllActiveHijoByGrupoAsync(TipoParticularModel c)
    {
        IEnumerable<TipoParticularModel> r = await _data.SelAllActiveHijoByGrupoAsync(c);
        return r.OrderBy(s => s.NuOrden).ThenBy(s => s.NoTipo).ThenBy(s => s.NuSubOrden);
    }
    public async Task<IEnumerable<TipoParticularModel>> SelAllGrupoByModuloAsync(TipoParticularModel c)
    {
        IEnumerable<TipoParticularModel> r = await _data.SelAllGrupoByModuloAsync(c);
        return r.OrderBy(s => s.NuOrden);
    }
    public async Task<IEnumerable<TipoParticularModel>> SelAllHijoAsync(TipoParticularModel c)
    {
        IEnumerable<TipoParticularModel> r = await _data.SelAllHijoAsync(c);
        return r.OrderBy(s => s.NuOrden);
    }
    public async Task<TipoParticularModel?> SelByIdAsync(TipoParticularModel c) => await _data.SelByIdAsync(c);
    public async Task<int> SelOrdenNextAsync(TipoParticularModel c) => await _data.SelOrdenNextAsync(c);
    public async Task<TipoParticularModel?> SelByIdGrupoAsync(TipoParticularModel c) => await _data.SelByIdGrupoAsync(c);
    public async Task UpdateDefaultAsync(TipoParticularModel c) => await _data.UpdateDefaultAsync(c);
    public async Task<TipoParticularModel?> SelDefaultAsync(TipoParticularModel c) => await _data.SelDefaultAsync(c);


}
