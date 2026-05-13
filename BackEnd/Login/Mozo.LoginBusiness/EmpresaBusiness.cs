using Mozo.LoginData;
using Mozo.Model.Seguridad;

namespace Mozo.LoginBusiness;

public interface IEmpresaBusiness
{
    Task<EmpresaModel?> SelByIdAsync(EmpresaFilterDto c);
    Task<IEnumerable<EmpresaModel>> SelAllAsync();
}
public class EmpresaBusiness : IEmpresaBusiness
{
    private readonly IEmpresaData _data;
    public EmpresaBusiness(IEmpresaData data)
    {
        _data = data;
    }

    public async Task<EmpresaModel?> SelByIdAsync(EmpresaFilterDto c) => await _data.SelByIdAsync(c);

    public async Task<IEnumerable<EmpresaModel>> SelAllAsync()
    {
        IEnumerable<EmpresaModel> r = await _data.SelAllAsync();
        return r.OrderBy(x => x.CoEmpresa);
    }
}