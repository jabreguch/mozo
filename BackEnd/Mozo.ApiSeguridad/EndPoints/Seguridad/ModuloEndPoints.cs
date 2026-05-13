using Mozo.ApiSeguridad.Helper;
using Mozo.Model.Seguridad;
using Mozo.SeguridadBusiness;

namespace Mozo.Api.Seguridad;

///<summary>
/// Endpoints para gestión de Módulos
///</summary>
///<history>
/// Create by Jonatan Abregu
///</history>
public static partial class ModuloEndPoints
{
    ///<summary>
    /// Mapea todas las rutas de Módulos
    ///</summary>
    ///<history>
    /// Create by Jonatan Abregu
    ///</history>
    public static RouteGroupBuilder MapModulo(this RouteGroupBuilder g)
    {
        g.WithSecurity();

        g.MapPost("/", InsertAsync)
            .WithResponsesValue<int>(StatusCodes.Status201Created)
            .WithDescription("Nuevo Módulo");

        g.MapPut("/", UpdateAsync)
            .WithResponsesValue<int>(StatusCodes.Status200OK)
            .WithDescription("Editar Módulo");

        g.MapPatch("/state", UpdateStateAsync)
            .WithResponsesValue<int>(StatusCodes.Status200OK)
            .WithDescription("Activa o desactivar Módulo");

        g.MapDelete("/byid", DeleteByIdAsync)
            .WithResponses(StatusCodes.Status204NoContent)
            .WithDescription("Eliminar Módulo");

        g.MapGet("/byid", SelByIdAsync)
            .WithResponses<ModuloModel>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound)
            .WithDescription("Obtener un Módulo");

        g.MapGet("/", SelAllAsync)
            .WithResponses<IEnumerable<ModuloModel>>(StatusCodes.Status200OK)
            .WithDescription("Obtener todos los Módulo");

        g.MapGet("/active", SelAllActiveAsync)
            .WithResponses<IEnumerable<ModuloModel>>(StatusCodes.Status200OK)
            .WithDescription("Obtener todos los Módulo activos");

    //    g.MapGet("/active-for-empresa", SelAllActiveForEmpresaAsync)
    //.WithResponses<IEnumerable<ModuloModel>>(StatusCodes.Status200OK)
    //.WithDescription("Obtener todos los Módulo activos para seleccionar en la empresa");

        //

        g.MapGet("/active/areas", SelAllActiveAreaAsync)
            .WithResponses<IEnumerable<ModuloModel>>(StatusCodes.Status200OK)
            .WithDescription("Obtener todos las areas de los Módulo activos");

        return g;
    }

}



public partial class ModuloEndPoints
{
    private static async Task<IResult>
        InsertAsync(
            ModuloModel m,
            IModuloBusiness IModulo
       )
    {
        m.CoModulo = await IModulo.InsertAsync(m);
        return Results.Created($"/{m.CoModulo}", m.CoModulo);
    }

    private static async Task<IResult>
        UpdateAsync(
            ModuloModel m,
            IModuloBusiness IModulo
        )
    {

        await IModulo.UpdateAsync(m);
        return Results.Ok(m.CoModulo);
    }


    private static async Task<IResult>
        DeleteByIdAsync(
             [AsParameters] ModuloFilterDto f,
            IModuloBusiness IModulo
        )
    {
        await IModulo.DeleteByIdAsync(f);
        return Results.NoContent();
    }


    private static async
        Task<IResult>
        UpdateStateAsync(
            ModuloModel m,
            IModuloBusiness IModulo
        )
    {

        await IModulo.UpdateStateAsync(m);
        return Results.Ok(m.CoModulo);
    }


    private static async Task<IResult>
        SelByIdAsync(
            [AsParameters] ModuloFilterDto f,
            IModuloBusiness IModulo
       )
    {
        ModuloModel? i = await IModulo.SelByIdAsync(f);
        if (i == null)
            return Results.NotFound();
        return Results.Ok(i);
    }

    private static async Task<IResult>
        SelAllAsync(
            [AsParameters] ModuloFilterDto f,
            IModuloBusiness IModulo
        )
    {

        IEnumerable<ModuloModel> r = await IModulo.SelAllAsync(f);
        return Results.Ok(r);
    }


    private static async Task<IResult>
        SelAllActiveAsync(
            IModuloBusiness IModulo
    )
    {
        IEnumerable<ModuloModel> r = await IModulo.SelAllActiveAsync();
        return Results.Ok(r);
    }

//    private static async Task<IResult>
//    SelAllActiveForEmpresaAsync(
//        IModuloBusiness IModulo
//)
//    {
//        IEnumerable<ModuloModel> r = await IModulo.SelAllActiveForEmpresaAsync();
//        return Results.Ok(r);
//    }

    private static async Task<IResult>
        SelAllActiveAreaAsync(
            IModuloBusiness IModulo
       )
    {
        IEnumerable<ModuloModel> r = await IModulo.SelAllActiveAreaAsync();
        return Results.Ok(r);
    }

}