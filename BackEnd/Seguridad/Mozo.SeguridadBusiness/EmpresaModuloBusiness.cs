using Mozo.Model.Seguridad;
using Mozo.SeguridadData;

namespace Mozo.SeguridadBusiness;

public interface IEmpresaModuloBusiness
{
    Task<int> InsertAsync(EmpresaModuloModel c);
    Task UpdateAsync(EmpresaModuloModel c);
    Task DeleteByIdAsync(EmpresaModuloFilterDto c);

    Task<EmpresaModuloModel?> SelByIdAsync(EmpresaModuloFilterDto c);
    Task<IEnumerable<EmpresaModuloModel>> SelAllAsync(EmpresaModuloFilterDto c);
}
public class EmpresaModuloBusiness : IEmpresaModuloBusiness
{
    private readonly IEmpresaModuloData _data;
    public EmpresaModuloBusiness(IEmpresaModuloData data)
    {
        _data = data;
    }

    public async Task<int> InsertAsync(EmpresaModuloModel c) => await _data.InsertAsync(c);
    public async Task UpdateAsync(EmpresaModuloModel c) => await _data.UpdateAsync(c);

    public async Task DeleteByIdAsync(EmpresaModuloFilterDto c) => await _data.DeleteByIdAsync(c);

    public async Task<EmpresaModuloModel?> SelByIdAsync(EmpresaModuloFilterDto c) => await _data.SelByIdAsync(c);

    public async Task<IEnumerable<EmpresaModuloModel>> SelAllAsync(EmpresaModuloFilterDto c)
    {
        IEnumerable<EmpresaModuloModel> r = await _data.SelAllAsync(c);
        return r.OrderBy(s => s.NuOrden);
    }

}