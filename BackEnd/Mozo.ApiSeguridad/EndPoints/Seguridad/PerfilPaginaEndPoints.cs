using Mozo.ApiSeguridad.Helper;
using Mozo.Model.Seguridad;
using Mozo.SeguridadBusiness;

namespace Mozo.Api.Seguridad;

///<summary>
/// Endpoints para gestión de PerfilPagina
///</summary>
///<history>
/// Create by Jonatan Abregu
///</history>
public static partial class PerfilPaginaEndPoints
{
    /// <summary>
    /// Mapea todas las rutas de PerfilPagina
    /// </summary>
    public static RouteGroupBuilder MapPerfilPagina(this RouteGroupBuilder g)
    {
        g.WithSecurity();

        g.MapPost("/", SaveAsync)
             .WithResponsesValue<int>(StatusCodes.Status200OK)
             .WithDescription("Insertar una PerfilPagina");

        g.MapGet("/", SelAllAsync)
            .WithResponses<IEnumerable<MenuModel>>(StatusCodes.Status200OK)
            .WithDescription("Obtener todas los PerfilPagina");

        return g;
    }

}

public partial class PerfilPaginaEndPoints
{

    private static async Task<IResult>
          SaveAsync(
              PerfilModel m,
              IPerfilPaginaBusiness IPerfilPagina
          )
    {
        await IPerfilPagina.DeleteByModuloAndPerfilAsync(new() { CoModulo = m.CoModulo, CoPerfil = m.CoPerfil });
        foreach (PerfilPaginaModel item in m.PerfilPaginaLst ?? new())
        {
            item.CoModulo = m.CoModulo;
            item.CoPerfil = m.CoPerfil;
            await IPerfilPagina.InsertAsync(item);
        }
        return TypedResults.Created($"/{m.CoPerfil}", m.CoPerfil);
    }

    static async Task<IResult>
        SelAllAsync(
            [AsParameters] PerfilPaginaFilterDto f,
            IMenuBusiness IMenu,
            IPaginaBusiness IPagina,
            IPerfilPaginaBusiness IPerfilPagina
     )
    {

        List<MenuModel> MenuLst = (await MenuSeguridad.SelAllArbolAsync(new() { CoModulo = f.CoModulo, FlEstReg = 1 }, IMenu, IPagina)) ?? new();


        List<PerfilPaginaModel> PrivilegioPerfilLst = (await IPerfilPagina.SelAllAsync(new() { CoModulo = f.CoModulo, CoPerfil = f.CoPerfil })).ToList();

        PerfilPaginaModel? tmp = null;

        foreach (MenuModel Menu in MenuLst ?? new())
        {

            tmp = PrivilegioPerfilLst.FirstOrDefault(x => x.CoModulo == Menu.CoModulo && x.CoMenu == Menu.CoMenu);
            if (tmp != null)
                Menu.FlEstReg = 1;
            else
                Menu.FlEstReg = 0;


            foreach (PaginaModel Pagina in Menu.PaginaLst ?? new())
            {
                tmp = PrivilegioPerfilLst.FirstOrDefault(x => x.CoModulo == Pagina.CoModulo && x.CoMenu == Pagina.CoMenu && x.CoPagina == Pagina.CoPagina);
                if (tmp != null)
                    Pagina.FlEstReg = 1;
                else
                    Pagina.FlEstReg = 0;
                /*
                foreach (PaginaFlotanteModel Flotante in Pagina.PaginaFlotanteLst ?? new())
                {
                    tmp = PrivilegioPerfilLst.FirstOrDefault(x => x.CoModulo == Flotante.CoModulo && x.CoMenu == Flotante.CoMenu && x.CoPagina == Flotante.CoPagina);
                    if (tmp != null)
                        Flotante.FlEstReg = 1;
                    else
                        Flotante.FlEstReg = 0;
                }
                */

                //foreach (ServicioWebModel WebService in Pagina.ServicioWebLst ?? new())
                //{
                //    tmp = PrivilegioPerfilLst.FirstOrDefault(x => x.CoModulo == WebService.CoModulo && x.CoMenu == WebService.CoMenu && x.CoPagina == WebService.CoPagina);
                //    if (tmp != null)
                //        WebService.CoEstReg = 1;
                //    else
                //        WebService.CoEstReg = 0;
                //}


                //foreach (SubPaginaModel SubPagina in Pagina.SubPaginaLst ?? new())
                //{
                //    tmp = PrivilegioPerfilLst.FirstOrDefault(x => x.CoModulo == SubPagina.CoModulo && x.CoMenu == SubPagina.CoMenu && x.CoPagina == SubPagina.CoPagina);
                //    if (tmp != null)
                //        SubPagina.FlEstReg = 1;
                //    else
                //        SubPagina.FlEstReg = 0;


                //foreach (PaginaFlotanteModel Flotante in SubPagina.PaginaFlotanteLst ?? new())
                //{
                //    tmp = PrivilegioPerfilLst.FirstOrDefault(x => x.CoModulo == Flotante.CoModulo && x.CoMenu == Flotante.CoMenu && x.CoPagina == Flotante.CoPagina);
                //    if (tmp != null)
                //    {
                //        Flotante.CoEstReg = 1;
                //        Flotante.CoEstReg2 = true;
                //    }
                //    else
                //    {
                //        Flotante.CoEstReg = 0;
                //        Flotante.CoEstReg2 = false;
                //    }
                //}

                //foreach (var WebService in SubPagina.ServicioWebLst.AsNotNull())
                //{
                //    tmp = PrivilegioPerfilLst.FirstOrDefault(x => x.CoModulo == WebService.CoModulo && x.CoMenu == WebService.CoMenu && x.CoPagina == WebService.CoPagina);
                //    if (tmp != null)
                //        WebService.CoEstReg = 1;
                //    else
                //        WebService.CoEstReg = 0;
                //}
                //}
            }
        }
        return TypedResults.Ok(MenuLst);
    }

}