using Mozo.ApiSeguridad.Helper;
using Mozo.LoginBusiness;
using Mozo.Model.Seguridad;

namespace Mozo.Api.Login;

///<summary>
/// Endpoints para gestión de ModuloUsuario
///</summary>
///<history>
/// Create by Jonatan Abregu
///</history>
public static partial class ModuloUsuarioEndPoints
{
    /// <summary>
    /// Mapea todas las rutas de ModuloUsuario
    /// </summary>
    public static RouteGroupBuilder MapModuloUsuario(this RouteGroupBuilder g)
    {
        g.WithSecurity();

        g.MapGet("/modulos", SelByModuloAsync)
             .WithResponses<ModuloUsuarioModel>(StatusCodes.Status200OK)
             .Produces(StatusCodes.Status404NotFound)
             .WithDescription("Obtener el Módulo del usuario");

        g.MapGet("/personas", SelAllByPersonaAsync)
            .WithResponses<IEnumerable<ModuloUsuarioModel>>(StatusCodes.Status200OK)
             .WithDescription("Obtener todos los Módulos del usuario");
        return g;
    }
}

public partial class ModuloUsuarioEndPoints
{

    static async Task<IResult>
        SelByModuloAsync(
            [AsParameters] ModuloUsuarioFilterDto f,
            IModuloUsuarioBusiness IModuloUsuario
       )
    {
        ModuloUsuarioModel? i = await IModuloUsuario.SelByModuloAsync(new());
        if (i is null)
            return Results.NotFound();
        return Results.Ok(i);
    }

    static async Task<IResult>
        SelAllByPersonaAsync(
            [AsParameters] ModuloUsuarioFilterDto f,
            IModuloUsuarioBusiness IModuloUsuario
            )
    {
        IEnumerable<ModuloUsuarioModel> r = await IModuloUsuario.SelAllByPersonaAsync(f);
        return Results.Ok(r);
    }


}