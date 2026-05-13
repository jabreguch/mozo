using Mozo.LoginData;
using Mozo.Model.Seguridad;

namespace Mozo.LoginBusiness;

public interface IMenuBusiness
{
    Task<IEnumerable<MenuModel>> SelAllAsync(MenuFilterDto c);

}
public class MenuBusiness : IMenuBusiness
{
    private readonly IMenuData _data;
    public MenuBusiness(IMenuData data)
    {
        _data = data;
    }

    public async Task<IEnumerable<MenuModel>> SelAllAsync(MenuFilterDto c)
    {
        IEnumerable<MenuModel> r = await _data.SelAllAsync(c);
        return r.OrderBy(x => x.NuOrden);
    }
}