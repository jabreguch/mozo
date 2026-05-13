using Mozo.Model.Seguridad;
using Mozo.SeguridadBusiness;
namespace Mozo.ApiSeguridad.Helper;

public static class MenuSeguridad
{
    public static async Task<List<MenuModel>?> SelAllArbolAsync(
        MenuFilterDto c, IMenuBusiness IMenu, IPaginaBusiness IPagina
    )
    {
        List<MenuModel>? MenuLst;
        List<PaginaModel>? PaginaLst;
        //List<SubPaginaModel>? SubPaginaLst;
        //List<PaginaFlotanteModel>? PaginaFlotanteLst;
        if (c.FlEstReg == 1)
        {
            MenuLst = (await IMenu.SelAllActiveAsync(c)).ToList();
            PaginaLst = (await IPagina.SelAllActivePaginaAsync(new() { CoModulo = c.CoModulo })).ToList();
            //SubPaginaLst = (await IPagina.SelAllActiveSubPaginaAsync(new() { CoModulo = c.CoModulo })).ToList();
            //PaginaFlotanteLst = (await IPagina.SelAllActivePaginaFlotanteAsync(new() { CoModulo = c.CoModulo })).ToList();
        }
        else
        {
            MenuLst = (await IMenu.SelAllAsync(c)).ToList();
            PaginaLst = (await IPagina.SelAllPaginaAsync(new() { CoModulo = c.CoModulo })).ToList();
            //SubPaginaLst = (await IPagina.SelAllSubPaginaAsync(new() { CoModulo = c.CoModulo })).ToList();
            //PaginaFlotanteLst = (await IPagina.SelAllPaginaFlotanteAsync(new() { CoModulo = c.CoModulo })).ToList();
        }

        MenuLst = MenuLst.OrderBy(x => x.NuOrden).ToList();
        PaginaLst = PaginaLst.OrderBy(x => x.NuOrden).ThenBy(b => b.NoControlador).ThenBy(c => c.NoAccion).ToList();
        //SubPaginaLst = SubPaginaLst.OrderBy(x => x.NuOrden).ThenBy(b => b.NoControlador).ThenBy(c => c.NoAccion).ToList();
        foreach (MenuModel Menu in MenuLst ?? new())
        {
            Menu.PaginaLst = PaginaLst.FindAll(x => x.CoMenu == Menu.CoMenu && x.CoTipoPagina == 1);
            /*
            foreach (PaginaModel Pagina in Menu.PaginaLst ?? new())
            {
                Pagina.SubPaginaLst = SubPaginaLst.FindAll(x => x.CoMenu == Pagina.CoMenu && x.CoPaginaPadre == Pagina.CoPagina);

                Pagina.PaginaFlotanteLst = PaginaFlotanteLst.FindAll(x => x.CoMenu == Pagina.CoMenu && x.CoPaginaPadre == Pagina.CoPagina);


            }
            */
        }

        return MenuLst;

    }
}