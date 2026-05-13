using Mozo.ApiSeguridad.Helper;
using Mozo.Model.Seguridad;
using Mozo.SeguridadBusiness;

namespace Mozo.Api.Seguridad;

///<summary>
/// Endpoints para gestión de Pagina
///</summary>
///<history>
/// Create by Jonatan Abregu
///</history>
public static partial class PaginaEndPoints
{
    ///<summary>
    /// Mapea todas las rutas de Pagina
    ///</summary>
    ///<history>
    /// Create by Jonatan Abregu
    ///</history>
    public static RouteGroupBuilder MapPagina(this RouteGroupBuilder g)
    {
        g.WithSecurity();

        g.MapPost("/", InsertAsync)
             .WithResponsesValue<int>(StatusCodes.Status200OK)
             .WithDescription("Insertar una Pagina");

        g.MapPut("/", UpdateAsync)
            .WithResponsesValue<int>(StatusCodes.Status200OK)
            .WithDescription("Actualizar una Pagina");

        g.MapDelete("/byid", DeleteByIdAsync)
            .WithResponses(StatusCodes.Status204NoContent)
             .WithDescription("Eliminar una Pagina");

        g.MapPatch("/state", UpdateStateAsync)
            .WithResponsesValue<int>(StatusCodes.Status200OK)
             .WithDescription("Activar o desactivar una Pagina");

        g.MapGet("/byid", SetByIdAsync)
             .WithResponses<PaginaModel>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound)
            .WithDescription("Obtener una Pagina");
        return g;
    }

}
public partial class PaginaEndPoints
{
    private static async Task<IResult>
         InsertAsync(
             PaginaModel m,
             IPaginaBusiness IPagina
        )
    {

        m.CoPagina = await IPagina.InsertAsync(m);
        return Results.Created($"/{m.CoPagina}", m.CoPagina);
    }
    private static async Task<IResult>
         UpdateAsync(
             PaginaModel m,
             IPaginaBusiness IPagina
      )
    {

        await IPagina.UpdateAsync(m);
        return Results.Ok(m.CoPagina);
    }

    private static async Task<IResult>
         DeleteByIdAsync(
             [AsParameters] PaginaFilterDto f,
             IPaginaBusiness IPagina
         )
    {
        await IPagina.DeleteByIdAsync(f);
        return Results.NoContent();
    }


    private static async Task<IResult>
         UpdateStateAsync(
             PaginaModel m,
             IPaginaBusiness IPagina
         )
    {

        await IPagina.UpdateStateAsync(m);
        return Results.Ok(m.CoMenu);
    }

    private static async Task<IResult>
         SetByIdAsync(
              [AsParameters] PaginaFilterDto f,
             IPaginaBusiness IPagina
          )
    {
        PaginaModel? i = await IPagina.SelByIdAsync(f);
        if (i is null)
            return Results.NotFound();
        return Results.Ok(i);
    }


}