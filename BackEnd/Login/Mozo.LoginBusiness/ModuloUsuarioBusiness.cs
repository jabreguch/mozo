using Mozo.LoginData;
using Mozo.Model.Seguridad;

namespace Mozo.LoginBusiness;

public interface IModuloUsuarioBusiness
{
    Task<ModuloUsuarioModel?> SelByIdAsync(ModuloUsuarioFilterDto c);
    Task<ModuloUsuarioModel?> SelByModuloAsync(ModuloUsuarioFilterDto c);
    Task<IEnumerable<ModuloUsuarioModel>> SelAllByPersonaAsync(ModuloUsuarioFilterDto c);

}
public class ModuloUsuarioBusiness : IModuloUsuarioBusiness
{
    private readonly IModuloUsuarioData _data;
    public ModuloUsuarioBusiness(IModuloUsuarioData data)
    {
        _data = data;
    }

    public async Task<IEnumerable<ModuloUsuarioModel>> SelAllByPersonaAsync(ModuloUsuarioFilterDto c)
    {
        IEnumerable<ModuloUsuarioModel> r = await _data.SelAllByPersonaAsync(c);
        return r.OrderBy(x => x.NuOrden);
    }
    public async Task<ModuloUsuarioModel?> SelByIdAsync(ModuloUsuarioFilterDto c) => await _data.SelByIdAsync(c);

    public async Task<ModuloUsuarioModel?> SelByModuloAsync(ModuloUsuarioFilterDto c) => await _data.SelByModuloAsync(c);

}