using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

using Mozo.Helper.Enu;
using Mozo.Helper.Global;
using Mozo.HelperWeb.Api;
using Mozo.MaestroBusiness;
using Mozo.Model.Maestro;

using System.Text.Json;

namespace Mozo.Api.Maestro;

public static partial class UploadEndPoints
{
    public static RouteGroupBuilder MapUpload(this RouteGroupBuilder g)
    {
        g.DisableAntiforgery().RequireAuthorization();
        g.MapPost("/UploadSmall", UploadSmall);
        g.MapPost("/UploadMedium", UploadMedium);
        return g;
    }

}
public static partial class UploadEndPoints
{
    static async Task<Results<Ok<string>, ValidationProblem>> UploadSmall(IFormFile file, [FromForm] string metadata, ITipoGeneralBusiness ITipoGeneral, IConfiguration configuration, UserContext user)
    {
        ArchivoModel? archivo = JsonSerializer.Deserialize<ArchivoModel>(metadata);

        TipoGeneralModel? formato = await ITipoGeneral.SelByIdAsync(new() { CoGrupo = EnuTipoGeneral.FormatoArchivo.CoGrupo, CoTipo = archivo!.CoTipo });

        if (formato != null)
        {
            string pathFolderDocument = configuration.GetSection("AppSettings").GetSection("FolderDocument").Value!;
            string pathFile = EnuTipoGeneral.FormatoArchivo.GetPath(formato.NoComando, user.CoEmpresa, archivo.CoEntidad, archivo.CoEntidad.ToString() + "." + Path.GetExtension(file.Name));

            long fileSize = file.Length;
            if (fileSize < 10 * 1024 * 1024) // < 10 MB
            {
                string path = Path.Combine(pathFolderDocument, pathFile);
                using FileStream stream = new FileStream(path, FileMode.Create);
                await file.CopyToAsync(stream);
                return TypedResults.Ok("Archivo pequeño cargado con multipart/form-data");
            }
            else if (fileSize < 100 * 1024 * 1024) // 10–100 MB
                return Extension.CreateValidationProblem("500", "Este archivo debe subirse en chunks");
            else
                return Extension.CreateValidationProblem("500", "Archivo demasiado grande, usar URL prefirmada a storage externo");

        }
        else
        {
            return Extension.CreateValidationProblem("500", "El tipo de archivo no se ha registrado en la tabla de tipos generales");
        }


    }

    static async Task<Results<Ok<string>, ValidationProblem>> UploadMedium(HttpRequest request, ITipoGeneralBusiness ITipoGeneral, IConfiguration configuration, UserContext user)
    {
        IFormCollection form = await request.ReadFormAsync();

        IFormFile file = form.Files["chunk"]!;
        int chunkIndex = int.Parse(form["chunkIndex"]!);
        int totalChunks = int.Parse(form["totalChunks"]!);


        string metadataJson = form["metadata"]!;
        ArchivoModel archivo = JsonSerializer.Deserialize<ArchivoModel>(metadataJson!)!;

        TipoGeneralModel? formato = await ITipoGeneral.SelByIdAsync(new() { CoGrupo = EnuTipoGeneral.FormatoArchivo.CoGrupo, CoTipo = archivo!.CoTipo });


        // Nombre del fragmento: <nombreSinExtension>.partN.<ext>
        string baseName = Path.GetFileNameWithoutExtension(file.FileName);
        string extension = Path.GetExtension(file.FileName);
        string filename = $"{baseName}.part{chunkIndex}{extension}";



        string pathFolderDocument = configuration.GetSection("AppSettings").GetSection("FolderDocument").Value!;
        string relativePath = EnuTipoGeneral.FormatoArchivo.GetPath(formato.NoComando, user.CoEmpresa, archivo.CoEntidad, filename);


        string fullPath = Path.Combine(pathFolderDocument, relativePath);

        // Guardar chunk
        Directory.CreateDirectory(Path.GetDirectoryName(fullPath)!);

        using (var fs = new FileStream(fullPath, FileMode.Create, FileAccess.Write))
        {
            using Stream input = file.OpenReadStream();
            await input.CopyToAsync(fs);
        }

        // Si es el último, reensamblar
        if (chunkIndex == totalChunks - 1)
        {
            string finalName = $"{archivo.CoEntidad}{extension}";
            string finalRelativePath = EnuTipoGeneral.FormatoArchivo.GetPath(
                formato.NoComando, user.CoEmpresa, archivo.CoEntidad, finalName);

            string finalFullPath = Path.Combine(pathFolderDocument, finalRelativePath);

            Directory.CreateDirectory(Path.GetDirectoryName(finalFullPath)!);

            using (var finalStream = new FileStream(finalFullPath, FileMode.Create, FileAccess.Write))
            {
                for (int i = 0; i < totalChunks; i++)
                {
                    string partName = $"{baseName}.part{i}{extension}";
                    string partRelativePath = EnuTipoGeneral.FormatoArchivo.GetPath(
                        formato.NoComando, user.CoEmpresa, archivo.CoEntidad, partName);
                    string partFullPath = Path.Combine(pathFolderDocument, partRelativePath);

                    using var partStream = new FileStream(partFullPath, FileMode.Open, FileAccess.Read);
                    await partStream.CopyToAsync(finalStream);

                    if (File.Exists(partFullPath)) // ✅ seguro
                        File.Delete(partFullPath);
                }
            }

        }
        return TypedResults.Ok($"Chunk {chunkIndex + 1}/{totalChunks}recibido");



    }

}

