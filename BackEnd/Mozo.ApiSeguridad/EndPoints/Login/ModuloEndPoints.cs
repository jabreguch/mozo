using Mozo.ApiSeguridad.Helper;
using Mozo.LoginBusiness;
using Mozo.Model.Seguridad;

namespace Mozo.Api.Login;

///<summary>
/// Endpoints para gestión de Modulo
///</summary>
///<history>
/// Create by Jonatan Abregu
///</history>
public static partial class ModuloEndPoints
{
    /// <summary>
    /// Mapea todas las rutas de Modulo
    /// </summary>
    public static RouteGroupBuilder MapModulo(this RouteGroupBuilder g)
    {
        g.WithSecurity();

        g.MapGet("/byid", SelByIdAsync)
            .WithResponses<ModuloModel>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound)
            .WithDescription("Obtener un Módulo");

        g.MapGet("/", SelAllAsync)
            .WithResponses<IEnumerable<ModuloModel>>(StatusCodes.Status200OK)
            .WithDescription("Obtener todos los Módulo");
        return g;
    }
}

public partial class ModuloEndPoints
{
    private static async Task<IResult>
         SelByIdAsync(
             [AsParameters] ModuloFilterDto f,
             IModuloBusiness IModulo
         )
    {
        ModuloModel? i = await IModulo.SelByIdAsync(f);
        if (i is null)
            return Results.NotFound();
        return Results.Ok(i);
    }

    private static async Task<IResult>
        SelAllAsync(
            [AsParameters] ModuloFilterDto f,
            IModuloBusiness IModulo
        )
    {
        IEnumerable<ModuloModel> r = await IModulo.SelAllAsync();
        return Results.Ok(r);
    }

}