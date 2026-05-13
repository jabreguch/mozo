using Mozo.LoginData;
using Mozo.Model.Seguridad;

namespace Mozo.LoginBusiness;

public interface IPaginaBusiness
{
    Task<IEnumerable<PaginaModel>> SelAllPaginaAsync(PaginaFilterDto c);
}
public class PaginaBusiness : IPaginaBusiness
{
    private readonly IPaginaData _data;
    public PaginaBusiness(IPaginaData data)
    {
        _data = data;
    }


    public async Task<IEnumerable<PaginaModel>> SelAllPaginaAsync(PaginaFilterDto c)
    {
        IEnumerable<PaginaModel> r = await _data.SelAllPaginaAsync(c);
        return r.OrderBy(x => x.NuOrden);
    }
  
}