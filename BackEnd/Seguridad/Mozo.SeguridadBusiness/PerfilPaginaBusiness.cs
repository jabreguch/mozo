using Mozo.Model.Seguridad;
using Mozo.SeguridadData;

namespace Mozo.SeguridadBusiness;

public interface IPerfilPaginaBusiness
{
    Task<int> InsertAsync(PerfilPaginaModel c);
    Task DeleteByModuloAndPerfilAsync(PerfilPaginaFilterDto c);
    Task<IEnumerable<PerfilPaginaModel>> SelAllAsync(PerfilPaginaFilterDto c);
}
public class PerfilPaginaBusiness : IPerfilPaginaBusiness
{
    private readonly IPerfilPaginaData _data;
    public PerfilPaginaBusiness(IPerfilPaginaData data)
    {
        _data = data;
    }

    public async Task<int> InsertAsync(PerfilPaginaModel c) => await _data.InsertAsync(c);
    
    public async Task DeleteByModuloAndPerfilAsync(PerfilPaginaFilterDto c) => await _data.DeleteByModuloAndPerfilAsync(c);
    public async Task<IEnumerable<PerfilPaginaModel>> SelAllAsync(PerfilPaginaFilterDto c) => await _data.SelAllAsync(c);

}