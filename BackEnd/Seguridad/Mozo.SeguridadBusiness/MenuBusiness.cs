using Mozo.Model.Seguridad;
using Mozo.SeguridadData;

namespace Mozo.SeguridadBusiness;

public interface IMenuBusiness
{
    Task<int> InsertAsync(MenuModel c);
    Task UpdateAsync(MenuModel c);
    Task UpdateStateAsync(MenuModel c);
    Task DeleteByIdAsync(MenuFilterDto c);
    Task<IEnumerable<MenuModel>> SelAllAsync(MenuFilterDto c);
    Task<IEnumerable<MenuModel>> SelAllActiveAsync(MenuFilterDto c);
    Task<MenuModel?> SelByIdAsync(MenuFilterDto c);
}
public class MenuBusiness : IMenuBusiness
{
    private readonly IMenuData _data;
    public MenuBusiness(IMenuData data)
    {
        _data = data;
    }

    public async Task<int> InsertAsync(MenuModel c) => await _data.InsertAsync(c);
    public async Task UpdateAsync(MenuModel c) => await _data.UpdateAsync(c);
    public async Task UpdateStateAsync(MenuModel c) => await _data.UpdateStateAsync(c);
    public async Task DeleteByIdAsync(MenuFilterDto c) => await _data.DeleteByIdAsync(c);
    public async Task<IEnumerable<MenuModel>> SelAllAsync(MenuFilterDto c) => await _data.SelAllAsync(c);
    public async Task<IEnumerable<MenuModel>> SelAllActiveAsync(MenuFilterDto c) => await _data.SelAllActiveAsync(c);
    public async Task<MenuModel?> SelByIdAsync(MenuFilterDto c) => await _data.SelByIdAsync(c);
}