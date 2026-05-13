using Mozo.Model.Seguridad;
using Mozo.SeguridadData;

namespace Mozo.SeguridadBusiness;

public interface IPaginaBusiness
{
    Task<int> InsertAsync(PaginaModel c);
    Task UpdateAsync(PaginaModel c);
    Task UpdateStateAsync(PaginaModel c);
    Task DeleteByIdAsync(PaginaFilterDto c);
    Task<PaginaModel?> SelByIdAsync(PaginaFilterDto c);
    Task<IEnumerable<PaginaModel>> SelAllPaginaAsync(PaginaFilterDto c);    
    Task<IEnumerable<PaginaModel>> SelAllActivePaginaAsync(PaginaFilterDto c);
}
public class PaginaBusiness : IPaginaBusiness
{
    private readonly IPaginaData _data;
    public PaginaBusiness(IPaginaData data)
    {
        _data = data;
    }
    public async Task<int> InsertAsync(PaginaModel c) => await _data.InsertAsync(c);
    public async Task UpdateAsync(PaginaModel c) => await _data.UpdateAsync(c);
    public async Task UpdateStateAsync(PaginaModel c) => await _data.UpdateStateAsync(c);
    public async Task DeleteByIdAsync(PaginaFilterDto c) => await _data.DeleteByIdAsync(c);
    public async Task<PaginaModel?> SelByIdAsync(PaginaFilterDto c) => await _data.SelByIdAsync(c);
    public async Task<IEnumerable<PaginaModel>> SelAllPaginaAsync(PaginaFilterDto c) => await _data.SelAllPaginaAsync(c);    
    public async Task<IEnumerable<PaginaModel>> SelAllActivePaginaAsync(PaginaFilterDto c) => await _data.SelAllActivePaginaAsync(c);    

}