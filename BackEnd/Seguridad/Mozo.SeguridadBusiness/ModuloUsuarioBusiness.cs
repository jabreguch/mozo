using Mozo.Model.Seguridad;
using Mozo.SeguridadData;

namespace Mozo.SeguridadBusiness;

public interface IModuloUsuarioBusiness
{
    Task<int> InsertAsync(ModuloUsuarioModel c);
    Task UpdateAsync(ModuloUsuarioModel c);
    Task UpdateConfiguracionAsync(ModuloUsuarioModel c);
    Task DeleteByIdAsync(ModuloUsuarioFilterDto c);
    Task<ModuloUsuarioModel?> SelByIdAsync(ModuloUsuarioFilterDto c);
    Task<IEnumerable<ModuloUsuarioModel>> SelAllByModuloAsync(ModuloUsuarioFilterDto c);
    Task<IEnumerable<ModuloUsuarioModel>> SelAllByPersonaAsync(ModuloUsuarioFilterDto c);
    Task<ModuloUsuarioModel?> SelByModuloAndPersonaAsync(ModuloUsuarioFilterDto c);
}
public class ModuloUsuarioBusiness : IModuloUsuarioBusiness
{
    private readonly IModuloUsuarioData _data;
    public ModuloUsuarioBusiness(IModuloUsuarioData data)
    {
        _data = data;
    }


    public async Task<int> InsertAsync(ModuloUsuarioModel c) => await _data.InsertAsync(c);

    public async Task UpdateAsync(ModuloUsuarioModel c) => await _data.UpdateAsync(c);

    public async Task UpdateConfiguracionAsync(ModuloUsuarioModel c) => await _data.UpdateConfiguracionAsync(c);

    public async Task DeleteByIdAsync(ModuloUsuarioFilterDto c) => await _data.DeleteByIdAsync(c);

    public async Task<IEnumerable<ModuloUsuarioModel>> SelAllByModuloAsync(ModuloUsuarioFilterDto c)
    {
        IEnumerable<ModuloUsuarioModel> r = await _data.SelAllByModuloAsync(c);
        return r;
    }

    public async Task<IEnumerable<ModuloUsuarioModel>> SelAllByPersonaAsync(ModuloUsuarioFilterDto c)
    {
        IEnumerable<ModuloUsuarioModel> r = await _data.SelAllByPersonaAsync(c);
        return r;
    }

    public async Task<ModuloUsuarioModel?> SelByIdAsync(ModuloUsuarioFilterDto c)
    {
        ModuloUsuarioModel? r = await _data.SelByIdAsync(c);
        return r;
    }

    public async Task<ModuloUsuarioModel?> SelByModuloAndPersonaAsync(ModuloUsuarioFilterDto c)
    {
        ModuloUsuarioModel? r = await _data.SelByModuloAndPersonaAsync(c);
        return r;
    }

}