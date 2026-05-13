using Mozo.ApiSeguridad.Helper;
using Mozo.MaestroBusiness;
using Mozo.Model.Maestro;

namespace Mozo.Api.Maestro;
///<summary>
/// Endpoints para gestión de RedSocial
///</summary>
///<history>
/// Create by Jonatan Abregu
///</history>
public static partial class RedSocialEndPoints
{
    /// <summary>
    /// Mapea todas las rutas de RedSocial
    /// </summary>
    public static RouteGroupBuilder MapRedSocial(this RouteGroupBuilder g)
    {
        g.WithSecurity();

        g.MapPost("/", InsertAsync)
         .WithResponsesValue<int>(StatusCodes.Status200OK)
         .WithDescription("Insertar una Red Social");

        g.MapPut("/", UpdateAsync)
          .WithResponsesValue<int>(StatusCodes.Status200OK)
          .WithDescription("Actualizar una Red Social");

        g.MapDelete("/byid", DeleteByIdAsync)
             .WithResponses(StatusCodes.Status204NoContent)
             .WithDescription("Eliminar una  Red Social");

        g.MapGet("/byid", SelByIdAsync)
            .WithResponses<RedSocialModel>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound)
            .WithDescription("Obtener una Red Social");

        g.MapGet("/", SelAllAsync)
          .WithResponses<IEnumerable<RedSocialModel>>(StatusCodes.Status200OK)
          .WithDescription("Obtener todas las Redes Social");

        return g;
    }
}
public static partial class RedSocialEndPoints
{
    static async Task<IResult>
        InsertAsync(
            RedSocialModel m,
            IRedSocialBusiness IRedSocial)
    {

        m.CoRedSocial = await IRedSocial.InsertAsync(m);
        return Results.Created($"/{m.CoRedSocial}", m.CoRedSocial);
    }
    static async Task<IResult>
        UpdateAsync(
            RedSocialModel m,
            IRedSocialBusiness IRedSocial
        )
    {

        await IRedSocial.UpdateAsync(m);
        return Results.Ok(m.CoRedSocial);
    }

    static async Task<IResult> DeleteByIdAsync(
        [AsParameters] RedSocialFilterDto f,
        IRedSocialBusiness IRedSocial)
    {
        await IRedSocial.DeleteByIdAsync(f);
        return Results.NoContent();
    }

    static async Task<IResult>
        SelAllAsync(
            [AsParameters] RedSocialFilterDto f,
            IRedSocialBusiness IRedSocial
       )
    {
        IEnumerable<RedSocialModel> r = await IRedSocial.SelAllAsync(f);
        r = r.OrderBy(x => x.CoTipoRedSocial).ThenBy(y => y.NoRedSocial);
        return Results.Ok(r);
    }


    static async Task<IResult>
        SelByIdAsync(
        [AsParameters] RedSocialFilterDto f,
        IRedSocialBusiness IRedSocial
        )
    {
        RedSocialModel? i = await IRedSocial.SelByIdAsync(f);
        if (i == null)
            return Results.NotFound();
        return Results.Ok(i);
    }

}



