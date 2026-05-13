using Microsoft.Extensions.Primitives;

using Mozo.ApiSeguridad.Helper;
using Mozo.MaestroBusiness;
using Mozo.Model.Maestro;


namespace Mozo.Api.Maestro;
///<summary>
/// Endpoints para gestión de Archivo
///</summary>
///<history>
/// Create by Jonatan Abregu
///</history>
public static partial class ArchivoEndPoints
{
    /// <summary>
    /// Mapea todas las rutas de Archivo
    /// </summary>
    public static RouteGroupBuilder MapArchivo(this RouteGroupBuilder g)
    {
        g.WithSecurity();

        g.MapPost("/meta-data", InsertMetaDataAsync)
         .DisableAntiforgery()
         .WithResponses<ArchivoModel>(StatusCodes.Status200OK)
          .Produces(StatusCodes.Status400BadRequest)
         .WithDescription("Subir archivo")
         .Accepts<IFormFile>("multipart/form-data");

        g.MapPut("/meta-data", UpdateMetaDataAsync)
          .DisableAntiforgery()
          .WithResponses<ArchivoModel>(StatusCodes.Status200OK)
          .Produces(StatusCodes.Status400BadRequest)
           .WithDescription("Reemplazar archivo existente")
          .Accepts<IFormFile>("multipart/form-data");

        g.MapPut("/", UpdateAsync)
      .WithResponsesValue<int>(StatusCodes.Status200OK)
      .Produces(StatusCodes.Status400BadRequest)
     .WithDescription("Actualizar descripción/título/orden de un Archivo");


        g.MapDelete("/byid", DeleteByIdAsync)
             .WithResponses(StatusCodes.Status204NoContent)
              .WithDescription("Eliminar registro y archivo físico");

        g.MapGet("/byid", SelByIdAsync)
            .WithResponses<ArchivoModel>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound)
            .WithDescription("Descargar archivo por id");

        g.MapGet("/meta-data-byuk", SelMetaDataByUkAsync)
         .WithResponses<ArchivoModel>(StatusCodes.Status200OK)
         .Produces(StatusCodes.Status404NotFound)
         .WithDescription("Obtener archivo por UK (empresa+entidad+tipo+orden)");

        g.MapGet("/metadata-byid", SelMetaDataByIdAsync)
            .WithResponses<ArchivoModel>(StatusCodes.Status200OK)
         .Produces(StatusCodes.Status404NotFound)
         .WithDescription("Obtener metadata del archivo");


        g.MapGet("/", SelAllAsync)
          .WithResponses<IEnumerable<ArchivoModel>>(StatusCodes.Status200OK)
          .WithDescription("Obtener todos los archivos de una entidad");


        g.MapGet("/active", SelAllActiveAsync)
        .WithResponses<IEnumerable<ArchivoModel>>(StatusCodes.Status200OK)
      .WithDescription("Obtener archivos activos de una entidad");


        // ✅ NUEVO: Actualizar solo orden
        g.MapPut("/orden", UpdateOrdenAsync)
            .WithResponses(StatusCodes.Status204NoContent)
            .WithDescription("Actualizar orden de un archivo");

        // ✅ NUEVO: Actualizar orden masivo
        g.MapPut("/orden-masivo", UpdateOrdenMasivoAsync)
            .WithResponses(StatusCodes.Status204NoContent)
            .WithDescription("Reordenar varios archivos a la vez");


        return g;
    }
}
public static partial class ArchivoEndPoints
{
    private static async Task<IResult>
          InsertMetaDataAsync(
               HttpRequest request,
              IArchivoBusiness IArchivo)
    {
        if (!request.HasFormContentType)
            return Results.BadRequest("Content-Type debe ser multipart/form-data");

        var form = await request.ReadFormAsync();
        var file = form.Files.GetFile("file");
        if (file == null) return Results.BadRequest("No se recibió archivo");

        var parametros = ParseUploadParams(form);
        var archivo = await IArchivo.InsertMetaDataAsync(parametros, file);
        return Results.Ok(archivo);

        //  m.CoArchivo = await IArchivo.InsertAsync(m);
        // return Results.Created($"/{m.CoArchivo}", m.CoArchivo);

    }


    private static async Task<IResult>
      UpdateMetaDataAsync(
           HttpRequest request,
          IArchivoBusiness IArchivo)
    {
        if (!request.HasFormContentType)
            return Results.BadRequest("Content-Type debe ser multipart/form-data");

        var form = await request.ReadFormAsync();
        var file = form.Files.GetFile("file");
        if (file == null) return Results.BadRequest("No se recibió archivo");

        var parametros = ParseUploadParams(form);      
        parametros.CoArchivo = ParseInt(form["coArchivo"]) ?? 0;

        var archivo = await IArchivo.UpdateMetaDataAsync(parametros, file);
        return Results.Ok(archivo);

    }



    private static async Task<IResult>
        UpdateAsync(
            ArchivoModel m,
            IArchivoBusiness IArchivo
        )
    {

        await IArchivo.UpdateAsync(m);
        return Results.Ok(m.CoArchivo);
    }

    private static async Task<IResult> DeleteByIdAsync(
        [AsParameters] ArchivoFilterDto f,
        IArchivoBusiness IArchivo)
    {
        await IArchivo.DeleteByIdAsync(f);
        return Results.NoContent();
    }

    private static async Task<IResult>
        SelAllAsync(
            [AsParameters] ArchivoFilterDto f,
            IArchivoBusiness IArchivo
       )
    {
        IEnumerable<ArchivoModel> r = await IArchivo.SelAllAsync(f);
        r = r.OrderBy(x => x.CoTipo).ThenBy(y => y.NoTitulo);
        return Results.Ok(r);
    }


    private static async Task<IResult>
      SelMetaDataByIdAsync(
        [AsParameters] ArchivoFilterDto f,
        HttpContext ctx,
      IArchivoBusiness IArchivo
      )
    {
        ArchivoModel? i = await IArchivo.SelMetaDataByIdAsync(f);
        if (i == null) return Results.NotFound();
        return Results.Ok(i);
    }



    private static async Task<IResult>
        SelByIdAsync(
        [AsParameters] ArchivoFilterDto f,
        IArchivoBusiness IArchivo
        )
    {
        ArchivoModel? i = await IArchivo.SelByIdAsync(f);
        if (i == null)
            return Results.NotFound();
        return Results.Ok(i);
    }

    private static async Task<IResult>
    SelMetaDataByUkAsync(
    [AsParameters] ArchivoFilterDto f,
    IArchivoBusiness IArchivo
    )
    {
        ArchivoModel? i = await IArchivo.SelMetaDataByUkAsync(f);
        if (i == null)
            return Results.NotFound();
        return Results.Ok(i);
    }



    private static async Task<IResult>
     SelAllActiveAsync(
     [AsParameters] ArchivoFilterDto f,
     IArchivoBusiness IArchivo
     )
    {
        IEnumerable<ArchivoModel> r = await IArchivo.SelAllActiveAsync(f);
        r = r.OrderBy(x => x.CoTipo).ThenBy(y => y.NoTitulo);
        return Results.Ok(r);
    }


    // ✅ NUEVO
    private static async Task<IResult> UpdateOrdenAsync(
        ArchivoModel m,
        IArchivoBusiness biz)
    {
        await biz.UpdateOrdenAsync(m);
        return Results.NoContent();
    }

    // ✅ NUEVO
    private static async Task<IResult> UpdateOrdenMasivoAsync(
        OrdenMasivoRequest req,
        IArchivoBusiness biz)
    {
        await biz.UpdateOrdenMasivoAsync(req);
        return Results.NoContent();
    }



    private static ArchivoModel ParseUploadParams(IFormCollection form) => new()
    {
        FlEmpresaNotKey = ParseInt(form["flEmpresaNotKey"]) ?? 0,
        CoEmpresa = ParseInt(form["coEmpresa"]),
        CoTipoEntidad = ParseInt(form["coTipoEntidad"]) ?? 0,
        CoEntidad = ParseInt(form["coEntidad"]) ?? 0,
        CoTipo = ParseInt(form["coTipo"]) ?? 0,
        FlGaleria = ParseInt(form["flGaleria"]) ?? 0,
    };

    private static int? ParseInt(StringValues value) =>
        int.TryParse(value.ToString(), out var result) ? result : null;
}


