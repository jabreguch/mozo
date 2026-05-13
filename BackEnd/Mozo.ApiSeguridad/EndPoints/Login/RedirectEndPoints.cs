using Mozo.ApiSeguridad.Helper;
using Mozo.LoginBusiness;
using Mozo.Model.Seguridad;

namespace Mozo.Api.Login;

///<summary>
/// Endpoints para gestión de Redirect
///</summary>
///<history>
/// Create by Jonatan Abregu
///</history>
public static partial class RedirectEndPoints
{
    /// <summary>
    /// Mapea todas las rutas de Redirect
    /// </summary>
    public static RouteGroupBuilder MapRedirect(this RouteGroupBuilder g)
    {
        g.WithSecurity();

        g.MapPost("/", InsertAsync)
        .WithResponsesValue<int>(StatusCodes.Status200OK)
         .WithDescription("Registra un Redireccionamiento");

        g.MapDelete("/byid", DeleteByIdAsync)
         .WithResponsesValue<int>(StatusCodes.Status200OK)
         .WithDescription("Elimina un Redireccionamiento");

        g.MapGet("/byid", SelByIdAsync)
         .WithResponses<RedirectModel>(StatusCodes.Status200OK)
         .Produces(StatusCodes.Status404NotFound)
         .WithDescription("Obtiene un Redireccionamiento");

        return g;
    }

}
public partial class RedirectEndPoints
{
    private static async Task<IResult>
         InsertAsync(
             RedirectModel m,
             IRedirectBusiness IRedirect
             )
    {
        m.CoRedirect = await IRedirect.InsertAsync(m);
        return Results.Created($"/{m.CoRedirect}", m.CoRedirect);
    }

    static async Task<IResult>
        DeleteByIdAsync(
            [AsParameters] RedirectFilterDto f,
            IRedirectBusiness IRedirect
            )
    {
        await IRedirect.DeleteByIdAsync(f);
        return Results.NoContent();
    }

    static async Task<IResult>
        SelByIdAsync(
            [AsParameters] RedirectFilterDto f,
            IRedirectBusiness IRedirect
        )
    {
        RedirectModel? i = await IRedirect.SelByIdAsync(f);
        if (i is null)
            return Results.NotFound();
        return Results.Ok(i);
    }


}