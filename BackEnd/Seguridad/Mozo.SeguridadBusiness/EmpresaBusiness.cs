using Mozo.Model.Seguridad;
using Mozo.SeguridadData;

namespace Mozo.SeguridadBusiness;

public interface IEmpresaBusiness
{
    Task<int> InsertAsync(EmpresaModel c);
    Task UpdateAsync(EmpresaModel c);
    Task UpdateStateAsync(EmpresaModel c);
    Task DeleteByIdAsync(EmpresaFilterDto c);
    Task<IEnumerable<EmpresaModel>> SelAllAsync(EmpresaFilterDto c);
    Task<IEnumerable<EmpresaModel>> SelAllActiveAsync();
    Task<EmpresaModel?> SelByIdAsync(EmpresaFilterDto c);

}
public class EmpresaBusiness : IEmpresaBusiness
{
    private readonly IEmpresaData _data;

    public EmpresaBusiness(IEmpresaData data)
    {
        _data = data;
    }

    public async Task<int> InsertAsync(EmpresaModel c) => await _data.InsertAsync(c);
    public async Task UpdateAsync(EmpresaModel c) => await _data.UpdateAsync(c);
    public async Task UpdateStateAsync(EmpresaModel c) => await _data.UpdateStateAsync(c);
    public async Task DeleteByIdAsync(EmpresaFilterDto c) => await _data.DeleteByIdAsync(c);
    public async Task<IEnumerable<EmpresaModel>> SelAllAsync(EmpresaFilterDto c)
    {
        IEnumerable<EmpresaModel> r = await _data.SelAllAsync(c);
        return r.OrderBy(s => s.CoEmpresa);
    }
    public async Task<IEnumerable<EmpresaModel>> SelAllActiveAsync()
    {
        IEnumerable<EmpresaModel> r = await _data.SelAllActiveAsync();
        return r.OrderBy(s => s.CoEmpresa);
    }
    public async Task<EmpresaModel?> SelByIdAsync(EmpresaFilterDto c) => await _data.SelByIdAsync(c);

}