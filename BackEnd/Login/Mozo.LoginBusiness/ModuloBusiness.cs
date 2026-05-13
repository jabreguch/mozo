using Mozo.LoginData;
using Mozo.Model.Seguridad;

namespace Mozo.LoginBusiness;

public interface IModuloBusiness
{
    Task<ModuloModel?> SelByIdAsync(ModuloFilterDto c);
    Task<IEnumerable<ModuloModel>> SelAllAsync();

}
public class ModuloBusiness : IModuloBusiness
{
    private readonly IModuloData _data;
    public ModuloBusiness(IModuloData data)
    {
        _data = data;
    }


    public async Task<IEnumerable<ModuloModel>> SelAllAsync()
    {
        IEnumerable<ModuloModel> r = await _data.SelAllAsync();
        return r.OrderBy(x => x.NuOrden);
    }

    public async Task<ModuloModel?> SelByIdAsync(ModuloFilterDto c) => await _data.SelByIdAsync(c);


}