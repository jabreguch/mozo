// Infrastructure/FileStorageService.cs
using Microsoft.Extensions.Options;

namespace Mozo.Helper.Services.Storage;

public class StorageOptions
{
    public string FolderDocument { get; set; } = "";
    public string FolderResource { get; set; } = "";
    public string FolderTemporary { get; set; } = "";
    public string CacheFolder { get; set; } = "_cache";
    public long MaxFileSizeBytes { get; set; } = 10485760; // 10MB
    public string[] AllowedExtensions { get; set; } = Array.Empty<string>();
}
public class ImageStandardOptions
{
    public string DefaultFormat { get; set; } = "webp";
    public int Quality { get; set; } = 80;
    public int MaxWidth { get; set; } = 800;
    public int MaxHeight { get; set; } = 800;
    public bool AutoOrient { get; set; } = true;
    public bool StripMetadata { get; set; } = true;
}

public interface IFileStorageService
{
    Task<string> SaveAsync(byte[] content, string relativePath);
    Task<Stream?> GetAsync(string relativePath);
    Task DeleteAsync(string relativePath);
    bool Exists(string relativePath);
    bool EsExtensionPermitida(string extension);
    long GetMaxFileSize();
    string BuildPath(int? coEmpresa, int? coTipoEntidad, int? coEntidad, int? coTipo, string fileName);
    string BuildCachePath(string relativePath, int width, string format);
}

public class LocalFileStorageService : IFileStorageService
{
    private readonly StorageOptions _opts;
    private readonly string _basePath;

    public LocalFileStorageService(IOptions<StorageOptions> opts)
    {
        _opts = opts.Value;
        _basePath = _opts.FolderDocument;

        if (string.IsNullOrEmpty(_basePath))
            throw new InvalidOperationException("Storage:FolderDocument no está configurado");

        Directory.CreateDirectory(_basePath);
    }

    public string BuildPath(int? coEmpresa, int? coTipoEntidad, int? coEntidad, int? coTipo, string fileName)
    {
        var empresa = coEmpresa?.ToString() ?? "global";
        return Path.Combine(
            $"empresa_{empresa}",
            $"entidad_{coTipoEntidad}",
            coEntidad.ToString()!,
            $"tipo_{coTipo}",
            fileName
        ).Replace("\\", "/");
    }

    public async Task<string> SaveAsync(byte[] content, string relativePath)
    {
        // Validar tamaño
        if (content.Length > _opts.MaxFileSizeBytes)
            throw new InvalidOperationException(
                $"El archivo excede el tamaño máximo de {_opts.MaxFileSizeBytes / 1024 / 1024} MB"
            );

        var fullPath = Path.Combine(_basePath, relativePath);
        var dir = Path.GetDirectoryName(fullPath)!;
        Directory.CreateDirectory(dir);

        await File.WriteAllBytesAsync(fullPath, content);
        return relativePath;
    }

    public Task<Stream?> GetAsync(string relativePath)
    {
        var fullPath = Path.Combine(_basePath, relativePath);
        if (!File.Exists(fullPath)) return Task.FromResult<Stream?>(null);

        Stream stream = File.OpenRead(fullPath);
        return Task.FromResult<Stream?>(stream);
    }

    public Task DeleteAsync(string relativePath)
    {
        var fullPath = Path.Combine(_basePath, relativePath);
        if (File.Exists(fullPath)) File.Delete(fullPath);

        // Eliminar también del caché si existe
        EliminarCache(relativePath);

        return Task.CompletedTask;
    }

    public bool Exists(string relativePath) =>
        File.Exists(Path.Combine(_basePath, relativePath));

    public bool EsExtensionPermitida(string extension)
    {
        return _opts.AllowedExtensions.Contains(
            extension.ToLowerInvariant(),
            StringComparer.OrdinalIgnoreCase
        );
    }

    public long GetMaxFileSize() => _opts.MaxFileSizeBytes;

    /// <summary>
    /// Construye la ruta del caché para una imagen redimensionada
    /// </summary>
    public string BuildCachePath(string relativePath, int width, string format)
    {
        var dir = Path.GetDirectoryName(relativePath) ?? "";
        var nombre = Path.GetFileNameWithoutExtension(relativePath);
        var cacheFolder = _opts.CacheFolder;

        return Path.Combine(
            dir,
            cacheFolder,
            $"{nombre}_w{width}.{format}"
        ).Replace("\\", "/");
    }

    private void EliminarCache(string relativePath)
    {
        try
        {
            var dir = Path.GetDirectoryName(relativePath) ?? "";
            var nombre = Path.GetFileNameWithoutExtension(relativePath);
            var cacheDir = Path.Combine(_basePath, dir, _opts.CacheFolder);

            if (Directory.Exists(cacheDir))
            {
                var pattern = $"{nombre}_*.*";
                foreach (var file in Directory.GetFiles(cacheDir, pattern))
                {
                    File.Delete(file);
                }
            }
        }
        catch
        {
            // Best effort, no falla si no se puede limpiar caché
        }
    }
}