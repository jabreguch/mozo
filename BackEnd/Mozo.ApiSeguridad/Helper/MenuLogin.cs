using Mozo.Helper.Enu;
using Mozo.LoginBusiness;
using Mozo.Model.Seguridad;
namespace Mozo.ApiSeguridad.Helper;


public static class MenuLogin
{

    //, IModuloUsuarioService _serviceModuloUsuario
    //  , IMenuService _serviceMenu
    //  , IPaginaService _servicePagina

    public static async Task<List<ModuloUsuarioModel>> SelAllModuloAsync(
     MenuModel c, IModuloUsuarioBusiness IModuloUsuario, IPerfilPaginaBusiness IPerfilPagina
    )
    {

        List<ModuloUsuarioModel> moduloUsuarioLst = (await IModuloUsuario.SelAllByPersonaAsync(new() { CoPersona = c.CoPersona })).ToList();
        moduloUsuarioLst = moduloUsuarioLst.OrderBy(x => x.NuOrden).ToList();

        List<PerfilPaginaModel> perfilPaginaLst = new();

        foreach (ModuloUsuarioModel moduloUsuario in moduloUsuarioLst)
        {
            perfilPaginaLst.AddRange((await IPerfilPagina.SelAllByModuloAndPerfilAsync(new()
            {
                CoModulo = moduloUsuario.CoModulo,
                CoPerfil = moduloUsuario.CoPerfil
            })).ToList());
        }
        perfilPaginaLst = perfilPaginaLst.OrderBy(x => x.NuOrden).ToList();

        foreach (ModuloUsuarioModel moduloUsuario in moduloUsuarioLst)
        {

            List<PerfilPaginaModel> menuTmpLst = perfilPaginaLst.Where(x => x.CoModulo == moduloUsuario.CoModulo & x.CoTipoPagina == EnuTipoGeneral.Seguridad.Pagina.Menu).ToList();

            if (menuTmpLst.Count() > 0)
            {
                moduloUsuario.MenuLst = new();
                foreach (PerfilPaginaModel menu in menuTmpLst)
                {
                    moduloUsuario.MenuLst.Add(new() { CoMenu = menu.CoMenu, NoMenu = menu.NoMenu, NuOrden = menu.NuOrden });
                }

                foreach (MenuModel menu in moduloUsuario.MenuLst)
                {
                    List<PerfilPaginaModel> paginaTmpLst = perfilPaginaLst.Where(x => x.CoModulo == moduloUsuario.CoModulo & x.CoMenu == menu.CoMenu && x.CoTipoPagina == EnuTipoGeneral.Seguridad.Pagina.Paginaa).ToList();
                    if (paginaTmpLst.Count() > 0)
                    {
                        menu.PaginaLst = new();
                        foreach (PerfilPaginaModel pagina in paginaTmpLst)
                        {
                            menu.PaginaLst.Add(new() { CoMenu = pagina.CoMenu, CoPagina = pagina.CoPagina, NuOrden = pagina.NuOrden, NoArea = pagina.NoArea, NoControlador = pagina.NoControlador, NoAccion = pagina.NoAccion, NoOpcion = pagina.NoOpcion });
                        }


                        /*
                        foreach (PaginaModel pagina in menu.PaginaLst)
                        {

                            List<PerfilPaginaModel> subPaginaTmpLst = perfilPaginaLst.Where(x => x.CoModulo == moduloUsuario.CoModulo & x.CoMenu == menu.CoMenu && x.CoPaginaPadre == pagina.CoPagina && x.CoTipoPagina == EnuTipoGeneral.Seguridad.Pagina.SubPagina).ToList();

                            if (subPaginaTmpLst.Count() > 0)
                            {
                                pagina.SubPaginaLst = new();
                                foreach (PerfilPaginaModel subPagina in subPaginaTmpLst)
                                {
                                    pagina.SubPaginaLst.Add(new() { CoMenu = subPagina.CoMenu, CoPagina = subPagina.CoPagina, CoPaginaPadre = subPagina.CoPaginaPadre, NuOrden = subPagina.NuOrden, NoArea = subPagina.NoArea, NoControlador = subPagina.NoControlador, NoAccion = subPagina.NoAccion, NoOpcion = subPagina.NoOpcion });
                                }
                            }


                            List<PerfilPaginaModel> paginaFlotanteTmpLst = perfilPaginaLst.Where(x => x.CoModulo == moduloUsuario.CoModulo & x.CoMenu == menu.CoMenu && x.CoPaginaPadre == pagina.CoPagina && x.CoTipoPagina == EnuTipoGeneral.Seguridad.Pagina.VistaFlotante).ToList();

                            if (paginaFlotanteTmpLst.Count() > 0)
                            {
                                pagina.PaginaFlotanteLst = new();
                                foreach (PerfilPaginaModel paginaFlotante in paginaFlotanteTmpLst)
                                {
                                    pagina.PaginaFlotanteLst.Add(new() { CoMenu = paginaFlotante.CoMenu, CoPagina = paginaFlotante.CoPagina, CoPaginaPadre = paginaFlotante.CoPaginaPadre, NoArea = paginaFlotante.NoArea, NoControlador = paginaFlotante.NoControlador, NoAccion = paginaFlotante.NoAccion });
                                }
                            }




                        }
                        */
                    }
                }
            }


        }






        foreach (ModuloUsuarioModel moduloUsuario in moduloUsuarioLst)
        {
            moduloUsuario.CoModulo = null;
            moduloUsuario.CoPerfil = null;
            moduloUsuario.NuOrden = null;

            foreach (MenuModel menu in moduloUsuario.MenuLst ?? new())
            {
                menu.CoMenu = null;
                menu.NuOrden = null;


                foreach (PaginaModel pagina in menu.PaginaLst ?? new())
                {
                    pagina.CoMenu = null;
                    pagina.CoPagina = null;
                    pagina.NuOrden = null;

                }


            }



        }













        return moduloUsuarioLst;

    }



}