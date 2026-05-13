
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

using Mozo.Helper.Services.Storage;
using Mozo.MaestroData;
using Mozo.Model.Maestro;

using System.Data;

namespace Mozo.MaestroBusiness;

public interface IArchivoBusiness
{
    Task<ArchivoModel> InsertMetaDataAsync(ArchivoModel c, IFormFile file);
    Task<ArchivoModel> UpdateMetaDataAsync(ArchivoModel c, IFormFile file);
    Task UpdateAsync(ArchivoModel c);
    Task UpdateOrdenAsync(ArchivoModel c);
    Task UpdateOrdenMasivoAsync(OrdenMasivoRequest req);
    Task DeleteByIdAsync(ArchivoFilterDto c);

    Task<ArchivoModel?> SelByIdAsync(ArchivoFilterDto c);
    Task<ArchivoModel?> SelMetaDataByUkAsync(ArchivoFilterDto c);
    Task<IEnumerable<ArchivoModel>> SelAllAsync(ArchivoFilterDto c);
    Task<ArchivoModel?> SelMetaDataByIdAsync(ArchivoFilterDto f);
    Task<IEnumerable<ArchivoModel>> SelAllActiveAsync(ArchivoFilterDto c);
}
public class ArchivoBusiness : IArchivoBusiness
{
    private readonly IArchivoData _data;
    private readonly IFileStorageService _storage;
    private readonly IImageProcessor _imageProcessor;
    private readonly ILogger<ArchivoBusiness> _logger;

    public ArchivoBusiness(
        IArchivoData data,
        IFileStorageService storage,
        IImageProcessor imageProcessor,
        ILogger<ArchivoBusiness> logger
       )
    {
        _data = data;
        _storage = storage;       
        _imageProcessor = imageProcessor;
        _logger = logger;
    }


    public Task UpdateOrdenAsync(ArchivoModel c) => _data.UpdateOrdenAsync(c);

    public Task UpdateOrdenMasivoAsync(OrdenMasivoRequest req) =>
        _data.UpdateOrdenMasivoAsync(req);

    public async Task<ArchivoModel?> SelByIdAsync(ArchivoFilterDto c)
    {

        ArchivoModel? i = await _data.SelByIdAsync(c);
        if (i != null) EnriquecerUrl(i);
        return i;
    }

    public async Task<ArchivoModel?> SelMetaDataByUkAsync(ArchivoFilterDto c)
    {

        ArchivoModel? i = await _data.SelMetaDataByUkAsync(c);
        if (i != null) EnriquecerUrl(i);
        return i;
    }
    public async Task<IEnumerable<ArchivoModel>> SelAllAsync(ArchivoFilterDto c)
    {

        List<ArchivoModel> r = (await _data.SelAllAsync(c)).ToList();
        r.OrderBy(x => x.CoTipo)
        .ThenBy(x => x.NuOrden)
        .ToList();
        foreach (ArchivoModel i in r) EnriquecerUrl(i);
        return r;

    }


    private async Task<ArchivoModel> UploadProcessAsync(IFormFile file, ArchivoModel p)
    {
        string extension = Path.GetExtension(file.FileName).ToLowerInvariant();
        string nombreSinExt = Path.GetFileNameWithoutExtension(file.FileName);
        string guid = Guid.NewGuid().ToString("N");

        // ⬇️ Validar extensión permitida
        if (!_storage.EsExtensionPermitida(extension))
        {
            throw new InvalidOperationException(
                $"Extensión '{extension}' no permitida"
            );
        }

        // ⬇️ Validar tamaño antes de procesar
        if (file.Length > _storage.GetMaxFileSize())
        {
            throw new InvalidOperationException(
                $"El archivo excede el tamaño máximo permitido"
            );
        }


        byte[] contenido;
        int? ancho = null;
        int? alto = null;
        string extensionFinal = extension;

        // Procesar solo si es imagen
        if (_imageProcessor.EsImagen(extension))
        {
            using Stream? stream = file.OpenReadStream();
            NormalizacionResult resultado = await _imageProcessor.NormalizarAsync(stream);

            contenido = resultado.Bytes;
            ancho = resultado.Ancho;
            alto = resultado.Alto;
            extensionFinal = resultado.Extension;
        }
        else
        {
            using var ms = new MemoryStream();
            await file.CopyToAsync(ms);
            contenido = ms.ToArray();
        }

        // Construir ruta
        string? nombreArchivo = $"{guid}{extensionFinal}";
        string? rutaRelativa = _storage.BuildPath(
            p.CoEmpresa,
            p.CoTipoEntidad,
            p.CoEntidad,
            p.CoTipo,
            nombreArchivo
        );

        // Guardar archivo físico
        await _storage.SaveAsync(contenido, rutaRelativa);

        // Insertar en BD
        ArchivoModel archivo = new()
        {
            FlEmpresaNotKey = p.FlEmpresaNotKey,
            CoEmpresa = p.CoEmpresa,
            CoArchivo = p.CoArchivo,
            CoTipoEntidad = p.CoTipoEntidad,
            CoEntidad = p.CoEntidad,
            CoTipo = p.CoTipo,
            FlGaleria = p.FlGaleria,
            NoArchivo = nombreArchivo,
            NoExtension = extensionFinal,
            NoRuta = rutaRelativa,
            NuBytes = contenido.Length,
            NuAlto = alto,
            NuAncho = ancho
        };
        return archivo;
    }


    //Task<int> InsertMetaDataAsync(ArchivoModel c, IFormFile file);
    public async Task<ArchivoModel> InsertMetaDataAsync(ArchivoModel c, IFormFile file)
    {
        ArchivoModel archivo = await UploadProcessAsync(file, c);
        archivo.CoArchivo = await _data.InsertMetaDataAsync(archivo);
        EnriquecerUrl(archivo);

        _logger.LogInformation(
          "Archivo {CoArchivo} subido: {Ruta} ({Bytes} bytes)",
          archivo.CoArchivo, archivo.NoRuta, archivo.NuBytes
      );

        return archivo;
    }

    public async Task DeleteByIdAsync(ArchivoFilterDto f)
    {
        ArchivoModel? archivo = await _data.SelMetaDataByIdAsync(f);
        if (archivo == null) return;

        // Eliminar registro
        await _data.DeleteByIdAsync(f);

        // Eliminar archivo físico
        if (!string.IsNullOrEmpty(archivo.NoRuta))
        {
            try
            {
                await _storage.DeleteAsync(archivo.NoRuta);
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex,
                    "No se pudo eliminar archivo físico: {Ruta}",
                    archivo.NoRuta
                );
            }
        }
        _logger.LogInformation("Archivo {CoArchivo} eliminado", f.CoArchivo);
    }

    public async Task<ArchivoModel> UpdateMetaDataAsync(ArchivoModel c, IFormFile file)
    {    // 1. Obtener el archivo anterior
        var filterAnterior = new ArchivoFilterDto
        {
            FlEmpresaNotKey = c.FlEmpresaNotKey,
            CoEmpresa = c.CoEmpresa,
            CoArchivo = c.CoArchivo
        };
        ArchivoModel? anterior = await _data.SelMetaDataByIdAsync(filterAnterior);
        if (anterior == null)
            throw new InvalidOperationException($"No se encontró el archivo {c.CoArchivo}");

        // 2. Eliminar el anterior (archivo físico)
        if (!string.IsNullOrEmpty(anterior.NoRuta))
        {
            try
            {
                await _storage.DeleteAsync(anterior.NoRuta);
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex,
                    "No se pudo eliminar archivo físico anterior: {Ruta}",
                    anterior.NoRuta
                );
            }
        }

        // 3. Actualizar el archivo
        ArchivoModel archivo = await UploadProcessAsync(file, c);
        await _data.UpdateMetaDataAsync(archivo);
        EnriquecerUrl(archivo);
        return archivo;
    }

    public async Task UpdateAsync(ArchivoModel c) => await _data.UpdateAsync(c);

    public async Task<IEnumerable<ArchivoModel>> SelAllActiveAsync(ArchivoFilterDto c)
    {

        List<ArchivoModel> r = (await _data.SelAllActiveAsync(c)).ToList();
        r.OrderBy(x => x.CoTipo)
        .ThenBy(x => x.NuOrden)
        .ToList();
        foreach (ArchivoModel i in r) EnriquecerUrl(i);
        return r;

    }



    public async Task<ArchivoModel?> SelMetaDataByIdAsync(ArchivoFilterDto f)
    {
        ArchivoModel? i = await _data.SelMetaDataByIdAsync(f);
        if (i != null) EnriquecerUrl(i);
        return i;
    }


    private static void EnriquecerUrl(ArchivoModel archivo)
    {
        archivo.Url = $"/uploads/{archivo.NoRuta}";
        archivo.ContentType = GetContentType(archivo.NoExtension!);
    }

    private static string GetContentType(string extension) => extension.ToLowerInvariant() switch
    {
        ".jpg" or ".jpeg" => "image/jpeg",
        ".png" => "image/png",
        ".webp" => "image/webp",
        ".gif" => "image/gif",
        ".pdf" => "application/pdf",
        ".doc" => "application/msword",
        ".docx" => "application/vnd.openxmlformats-officedocument.wordprocessingml.document",
        ".xls" => "application/vnd.ms-excel",
        ".xlsx" => "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
        _ => "application/octet-stream"
    };

}
