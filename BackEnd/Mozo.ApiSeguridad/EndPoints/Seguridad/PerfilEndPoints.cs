using Mozo.ApiSeguridad.Helper;
using Mozo.Model.Seguridad;
using Mozo.SeguridadBusiness;

namespace Mozo.Api.Seguridad;

///<summary>
/// Endpoints para gestión de Perfil
///</summary>
///<history>
/// Create by Jonatan Abregu
///</history>
public static partial class PerfilEndPoints
{
    /// <summary>
    /// Mapea todas las rutas de Perfil
    /// </summary>
    public static RouteGroupBuilder MapPerfil(this RouteGroupBuilder g)
    {
        g.WithSecurity();

        g.MapPost("/", InsertAsync)
             .WithResponsesValue<int>(StatusCodes.Status200OK)
             .WithDescription("Insertar un Perfil");

        g.MapPut("/", UpdateAsync)
             .WithResponsesValue<int>(StatusCodes.Status200OK)
             .WithDescription("Actualizar un Perfil");

        g.MapDelete("/byid", DeleteByIdAsync)
             .WithResponses(StatusCodes.Status204NoContent)
             .WithDescription("Eliminar una Perfil");

        g.MapPatch("/state", UpdateStateAsync)
              .WithResponsesValue<int>(StatusCodes.Status200OK)
             .WithDescription("Activar o desactivar un Perfil");

        g.MapGet("/default-bymodulo", SelDefaultAsync)
             .WithResponses<PerfilModel>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound)
            .WithDescription("Obtener un Perfil por defecto");

        g.MapGet("/byid", SelByIdAsync)
            .WithResponses<PerfilModel>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound)
            .WithDescription("Obtener un Perfil");

        g.MapGet("/", SelAllAsync)
            .WithResponses<IEnumerable<PerfilModel>>(StatusCodes.Status200OK)
            .WithDescription("Obtener todas los Perfiles");


        g.MapGet("/active", SelAllActiveAsync)
            .WithResponses<IEnumerable<PerfilModel>>(StatusCodes.Status200OK)
            .WithDescription("Obtener todas los Perfiles activos");
        return g;
    }

}
public partial class PerfilEndPoints
{
    private static async Task<IResult>
         InsertAsync(
            PerfilModel m,
            IPerfilBusiness IPerfil
        )
    {
        m.CoPerfil = await IPerfil.InsertAsync(m);
        return Results.Created($"/{m.CoPerfil}", m.CoPerfil);
    }
    private static async Task<IResult>
         UpdateAsync(
             PerfilModel m,
             IPerfilBusiness IPerfil
        )
    {
        await IPerfil.UpdateAsync(m);
        return Results.Ok(m.CoPerfil);
    }
    private static async Task<IResult>
         DeleteByIdAsync(
          [AsParameters] PerfilFilterDto f,
             IPerfilBusiness IPerfil
        )
    {
        await IPerfil.DeleteByIdAsync(f);
        return Results.NoContent();
    }


    private static async Task<IResult>
             UpdateStateAsync(
                 PerfilModel m,
                 IPerfilBusiness IPerfil
        )
    {
        await IPerfil.UpdateStateAsync(m);
        return Results.Ok(m.CoPerfil);
    }

    private static async Task<IResult>
         SelAllAsync(
             [AsParameters] PerfilFilterDto f,
             IPerfilBusiness IPerfil
        )
    {

        IEnumerable<PerfilModel> r = await IPerfil.SelAllAsync(f);
        return Results.Ok(r);
    }
    private static async Task<IResult>
         SelByIdAsync(
             [AsParameters] PerfilFilterDto f,
             IPerfilBusiness IPerfil
        )
    {
        PerfilModel? i = await IPerfil.SelByIdAsync(f);
        if (i is null)
            return Results.NotFound();
        return Results.Ok(i);
    }

    private static async Task<IResult>
             SelAllActiveAsync(
                 [AsParameters] PerfilFilterDto f,
                 IPerfilBusiness IPerfil
       )
    {

        IEnumerable<PerfilModel> r = await IPerfil.SelAllActiveAsync(f);
        return Results.Ok(r);
    }

    private static async Task<IResult>
         SelDefaultAsync(
             [AsParameters] PerfilFilterDto f,
             IPerfilBusiness IPerfil
        )
    {

        PerfilModel? i = await IPerfil.SelDefaultAsync(f);
        if (i is null)
            return Results.NotFound();
        return Results.Ok(i);
    }


}