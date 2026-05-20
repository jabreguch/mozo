using Microsoft.AspNetCore.OutputCaching;

using Mozo.ApiSeguridad.Helper;
using Mozo.Model.Seguridad;
using Mozo.SeguridadBusiness;

namespace Mozo.Api.Seguridad;

///<summary>
/// Endpoints para gestión de Empresa
///</summary>
///<history>
/// Create by Jonatan Abregu
///</history>
public static partial class EmpresaEndPoints
{
    private const string CacheTag = "Empresa_SelAllActive";
    /// <summary>
    /// Mapea todas las rutas de Empresa
    /// </summary>
    public static RouteGroupBuilder MapEmpresa(this RouteGroupBuilder g)
    {
        g.WithSecurity();

        g.MapPost("/", InsertAsync)
             .WithResponsesValue<int>(StatusCodes.Status200OK)
             .WithDescription("Insertar una Empresa");

        g.MapPut("/", UpdateAsync)
             .WithResponsesValue<int>(StatusCodes.Status200OK)
             .WithDescription("Actualizar una Empresa");

        g.MapPatch("/state", UpdateStateAsync)
             .WithResponsesValue<int>(StatusCodes.Status200OK)
             .WithDescription("Activar o desactivar una Empresa");

        g.MapDelete("/byid", DeleteByIdAsync)
            .WithResponses(StatusCodes.Status204NoContent)
             .WithDescription("Eliminar una Empresa");

        g.MapGet("/byid", SelByIdAsync)
            .WithResponses<EmpresaModel>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound)
            .WithDescription("Obtener una Empresa");

        g.MapGet("/", SelAllAsync)
            .WithResponses<IEnumerable<EmpresaModel>>(StatusCodes.Status200OK)
            .WithDescription("Obtener todas las Empresas");

        g.MapGet("/active", SelAllActiveAsync)
            .CacheOutput(x => x.Expire(TimeSpan.FromHours(24)).Tag(CacheTag))
            .WithResponses<IEnumerable<EmpresaModel>>(StatusCodes.Status200OK)
            .WithDescription("Obtener todas las Empresas activas");

        return g;
    }

}


public static partial class EmpresaEndPoints
{
    private static async Task<IResult>
         InsertAsync(
             EmpresaModel m,
             IOutputCacheStore cacheStore,
             IEmpresaBusiness IEmpresa
      )
    {
        m.CoEmpresa = await IEmpresa.InsertAsync(m);
        await cacheStore.InvalidateCacheAsync(CacheTag);
        return Results.Created($"/{m.CoEmpresa}", m.CoEmpresa);
    }


    private static async Task<IResult>
        UpdateAsync(
            EmpresaModel m,
            IOutputCacheStore cacheStore,
            IEmpresaBusiness IEmpresa
     )
    {

        await IEmpresa.UpdateAsync(m);
        await cacheStore.InvalidateCacheAsync(CacheTag);
        return Results.Ok(m.CoEmpresa);
    }

    private static async Task<IResult>
        UpdateStateAsync(
            EmpresaModel m,
            IEmpresaBusiness IEmpresa,
            IOutputCacheStore cacheStore
            )
    {

        await IEmpresa.UpdateStateAsync(m);
        await cacheStore.InvalidateCacheAsync(CacheTag);
        return Results.Ok(m.CoEmpresa);
    }

    private static async Task<IResult>
        DeleteByIdAsync(
             [AsParameters] EmpresaFilterDto f,
            IEmpresaBusiness IEmpresa,
            IOutputCacheStore cacheStore
      )
    {
        await IEmpresa.DeleteByIdAsync(f);
        await cacheStore.InvalidateCacheAsync(CacheTag);
        return Results.NoContent();
    }


    private static async Task<IResult>
        SelByIdAsync(
              [AsParameters] EmpresaFilterDto f,
            IEmpresaBusiness IEmpresa
            )
    {
        EmpresaModel? i = await IEmpresa.SelByIdAsync(f);
        if (i is null)
            return TypedResults.NotFound();
        return Results.Ok(i);
    }

    private static async Task<IResult>
        SelAllAsync(
            [AsParameters] EmpresaFilterDto f,
            IEmpresaBusiness IEmpresa)
    {

        IEnumerable<EmpresaModel> r = await IEmpresa.SelAllAsync(f);
        return Results.Ok(r);
    }


    private static async Task<IResult>
        SelAllActiveAsync(IEmpresaBusiness IEmpresa)
    {
        IEnumerable<EmpresaModel> r = await IEmpresa.SelAllActiveAsync();
        return Results.Ok(r);
    }

}