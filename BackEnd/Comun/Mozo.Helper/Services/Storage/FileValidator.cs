using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
namespace Mozo.Helper.Services.Storage;


public interface IFileValidator
{
    bool EsArchivoValido(IFormFile file, out string error);
}

public class FileValidator : IFileValidator
{
    private readonly StorageOptions _opts;
    private readonly HashSet<string> _extensiones;

    private static readonly Dictionary<string, byte[][]> _magicBytes = new()
    {
        [".jpg"] = [[0xFF, 0xD8, 0xFF]],
        [".jpeg"] = [[0xFF, 0xD8, 0xFF]],
        [".png"] = [[0x89, 0x50, 0x4E, 0x47]],
        [".gif"] = [[0x47, 0x49, 0x46, 0x38]],
        [".webp"] = [[0x52, 0x49, 0x46, 0x46]],
        [".pdf"] = [[0x25, 0x50, 0x44, 0x46]],
        [".docx"] = [[0x50, 0x4B, 0x03, 0x04]],
        [".xlsx"] = [[0x50, 0x4B, 0x03, 0x04]],
        [".zip"] = [[0x50, 0x4B, 0x03, 0x04]]
    };

    public FileValidator(IOptions<StorageOptions> opts)
    {
        _opts = opts.Value;
        var exts = _opts.AllowedExtensions?.Length > 0
            ? _opts.AllowedExtensions
            : new[] { ".jpg", ".jpeg", ".png", ".webp", ".pdf", ".docx", ".xlsx" };
        _extensiones = new HashSet<string>(exts, StringComparer.OrdinalIgnoreCase);
    }

    public bool EsArchivoValido(IFormFile file, out string error)
    {
        error = "";
        if (file.Length == 0) { error = "Archivo vacío"; return false; }
        if (file.Length > _opts.MaxFileSizeBytes)
        {
            error = $"Archivo supera {_opts.MaxFileSizeBytes / 1024 / 1024} MB";
            return false;
        }

        var ext = Path.GetExtension(file.FileName).ToLowerInvariant();
        if (!_extensiones.Contains(ext)) { error = $"Extensión {ext} no permitida"; return false; }

        if (_magicBytes.TryGetValue(ext, out var firmas))
        {
            using var stream = file.OpenReadStream();
            var header = new byte[8];
            stream.Read(header, 0, 8);

            var ok = firmas.Any(firma => header.Take(firma.Length).SequenceEqual(firma));
            if (!ok) { error = "El contenido no coincide con la extensión"; return false; }
        }
        return true;
    }

}