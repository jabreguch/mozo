using Mozo.MaestroData;
using Mozo.Model.Maestro;

namespace Mozo.MaestroBusiness;

public interface IPaisBusiness
{
    Task<IEnumerable<PaisModel>> SelAllActiveAsync();
}

public class PaisBusiness : IPaisBusiness
{
    private readonly IPaisData _data;
    public PaisBusiness(IPaisData data)
    {
        _data = data;
    }
    public async Task<IEnumerable<PaisModel>> SelAllActiveAsync()
    {
        var r = await _data.SelAllActiveAsync();
        return r.OrderBy(x => x.NoPais);
    }

}
