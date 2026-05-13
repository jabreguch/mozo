using Mozo.ApiSeguridad.Helper;
using Mozo.Model.Seguridad;
using Mozo.SeguridadBusiness;

namespace Mozo.Api.Seguridad;
///<summary>
/// Endpoints para gestión de Menu
///</summary>
///<history>
/// Create by Jonatan Abregu
///</history>
public static partial class MenuEndPoints
{
    /// <summary>
    /// Mapea todas las rutas de Menu
    /// </summary>
    public static RouteGroupBuilder MapMenu(this RouteGroupBuilder g)
    {
        g.WithSecurity();

        g.MapPost("/", InsertAsync)
            .WithResponsesValue<int>(StatusCodes.Status200OK)
            .WithDescription("Insertar un Menu");

        g.MapPut("/", UpdateAsync)
             .WithResponsesValue<int>(StatusCodes.Status200OK)
             .WithDescription("Actualizar un Menu");

        g.MapDelete("/byid", DeleteByIdAsync)
             .WithResponses(StatusCodes.Status204NoContent)
             .WithDescription("Eliminar un Menu");

        g.MapPatch("/state", UpdateStateAsync)
             .WithResponsesValue<int>(StatusCodes.Status200OK)
             .WithDescription("Activar o desactivar un Menu");

        g.MapGet("/byid", SelByIdAsync)
            .WithResponses<MenuModel>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound)
            .WithDescription("Obtener un Menu");

        g.MapGet("/arbol", SelAllArbolAsync)
            .WithResponses<List<MenuModel>>(StatusCodes.Status200OK)
            .WithDescription("Obtener el Menu de Opciones");

        return g;
    }

}
public partial class MenuEndPoints
{

    private static async Task<IResult>
         InsertAsync(
             MenuModel m,
             IMenuBusiness IMenu
        )
    {
        m.CoMenu = await IMenu.InsertAsync(m);
        return Results.Created($"/{m.CoMenu}", m.CoMenu);
    }

    private static async Task<IResult>
         UpdateAsync(
             MenuModel m,
             IMenuBusiness IMenu
       )
    {
        await IMenu.UpdateAsync(m);
        return Results.Ok(m.CoMenu);
    }

    private static async Task<IResult>
         DeleteByIdAsync(
             [AsParameters] MenuFilterDto f,
             IMenuBusiness IMenu
      )
    {
        await IMenu.DeleteByIdAsync(f);
        return Results.NoContent();
    }


    private static async Task<IResult>
         UpdateStateAsync(
             MenuModel m,
             IMenuBusiness IMenu
        )
    {

        await IMenu.UpdateStateAsync(m);
        return Results.Ok(m.CoMenu);
    }

    private static async Task<IResult>
         SelByIdAsync(
             [AsParameters] MenuFilterDto f,
             IMenuBusiness IMenu
        )
    {
        MenuModel? i = await IMenu.SelByIdAsync(f);
        if (i is null)
            return Results.NotFound();
        return Results.Ok(i);
    }


    private static async Task<IResult>
         SelAllArbolAsync(
             [AsParameters] MenuFilterDto f,
             IMenuBusiness IMenu,
             IPaginaBusiness IPagina
             )
    {

        return Results.Ok(await MenuSeguridad.SelAllArbolAsync(f, IMenu, IPagina));
    }

}