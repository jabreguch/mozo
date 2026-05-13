using Mozo.MaestroData;
using Mozo.Model;

namespace Mozo.MaestroBusiness;
public interface IUbigeoBusiness
{
    Task<UbigeoModel?> SelById(UbigeoModel c);
    Task<IEnumerable<UbigeoModel>> SelAllDepartamento(UbigeoModel c);
    Task<IEnumerable<UbigeoModel>> SelAllProvincia(UbigeoModel c);
    Task<IEnumerable<UbigeoModel>> SelAllDistrito(UbigeoModel c);
}


public class UbigeoBusiness : IUbigeoBusiness
{
    private readonly IUbigeoData _data;
    public UbigeoBusiness(IUbigeoData data)
    {
        _data = data;
    }
    public async Task<UbigeoModel?> SelById(UbigeoModel c) => await _data.SelById(c);
    public async Task<IEnumerable<UbigeoModel>> SelAllDepartamento(UbigeoModel c)
    {
        IEnumerable<UbigeoModel> r = await _data.SelAllDepartamento(c);
        return r.OrderBy(s => s.CoDepartamento).ToList();
    }
    public async Task<IEnumerable<UbigeoModel>> SelAllProvincia(UbigeoModel c)
    {
        IEnumerable<UbigeoModel> r = await _data.SelAllProvincia(c);
        return r.OrderBy(s => s.CoProvincia).ToList();
    }
    public async Task<IEnumerable<UbigeoModel>> SelAllDistrito(UbigeoModel c)
    {
        IEnumerable<UbigeoModel> r = await _data.SelAllDistrito(c); ;
        return r.OrderBy(s => s.CoDistrito).ToList();
    }

}
