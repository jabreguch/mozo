using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Formats.Webp;
using SixLabors.ImageSharp.Processing;

namespace Mozo.Helper.Services.Storage;


public interface IImageProcessor
{
    bool EsImagen(string extension);
    Task<NormalizacionResult> NormalizarAsync(Stream input);
    Task<NormalizacionResult> NormalizarPorCalidadAsync(Stream input, int coCalidad, bool validarPeso = true);
}
public record NormalizacionResult(byte[] Bytes, int Ancho, int Alto, string Extension, string ContentType);

public record CalidadConfig(int MaxLado, int Calidad, int PesoMaximoKB, int CalidadFallback = 65);

public class ImageProcessor : IImageProcessor
{
    private readonly ImageStandardOptions _opts;
    private readonly ILogger<ImageProcessor> _logger;

    private static readonly HashSet<string> _extensionesValidas = new(StringComparer.OrdinalIgnoreCase)
    {
        ".jpg", ".jpeg", ".png", ".gif", ".webp", ".bmp", ".heic", ".heif"
    };

    private static readonly Dictionary<int, CalidadConfig> _calidades = new()
    {
        { 1, new(MaxLado: 200, Calidad: 75, PesoMaximoKB: 30) },
        { 2, new(MaxLado: 400, Calidad: 80, PesoMaximoKB: 70) },
        { 3, new(MaxLado: 800, Calidad: 80, PesoMaximoKB: 130) }
    };

    public ImageProcessor(IOptions<ImageStandardOptions> opts, ILogger<ImageProcessor> logger)
    {
        _opts = opts.Value;
        _logger = logger;
    }

    public bool EsImagen(string ext) => _extensionesValidas.Contains(ext);

    public async Task<NormalizacionResult> NormalizarAsync(Stream input)
    {
        using var image = await CargarImagenAsync(input);
        AplicarOrientacionYMetadata(image);
        RedimensionarSiExcede(image, _opts.MaxWidth, _opts.MaxHeight);
        return await CodificarAsync(image, _opts.DefaultFormat, _opts.Quality);
    }

    public async Task<NormalizacionResult> NormalizarPorCalidadAsync(
        Stream input, int coCalidad, bool validarPeso = true)
    {
        var config = _calidades.TryGetValue(coCalidad, out var c)
            ? c
            : new CalidadConfig(_opts.MaxWidth, _opts.Quality, 130);

        using var image = await CargarImagenAsync(input);
        AplicarOrientacionYMetadata(image);
        RedimensionarSiExcede(image, config.MaxLado, config.MaxLado);

        var resultado = await CodificarComoWebpAsync(image, config.Calidad);

        if (!validarPeso || resultado.Bytes.Length <= config.PesoMaximoKB * 1024)
            return resultado;

        _logger.LogInformation(
            "Imagen calidad {CoCalidad} excedió peso ({Peso}KB), reintentando con calidad {Fallback}",
            coCalidad, resultado.Bytes.Length / 1024, config.CalidadFallback
        );

        return await CodificarComoWebpAsync(image, config.CalidadFallback);
    }

    private static async Task<Image> CargarImagenAsync(Stream input)
    {
        if (input.CanSeek) input.Position = 0;
        return await Image.LoadAsync(input);
    }

    private void AplicarOrientacionYMetadata(Image image)
    {
        if (_opts.AutoOrient) image.Mutate(x => x.AutoOrient());

        if (_opts.StripMetadata)
        {
            image.Metadata.ExifProfile = null;
            image.Metadata.IptcProfile = null;
            image.Metadata.XmpProfile = null;
        }
    }

    private static void RedimensionarSiExcede(Image image, int maxAncho, int maxAlto)
    {
        if (image.Width <= maxAncho && image.Height <= maxAlto) return;

        image.Mutate(x => x.Resize(new ResizeOptions
        {
            Mode = ResizeMode.Max,
            Size = new Size(maxAncho, maxAlto)
        }));
    }

    private static async Task<NormalizacionResult> CodificarAsync(
        Image image, string formato, int calidad)
    {
        return formato.ToLowerInvariant() switch
        {
            "webp" => await CodificarComoWebpAsync(image, calidad),
            "png" => await CodificarComoPngAsync(image),
            _ => await CodificarComoJpegAsync(image, calidad)
        };
    }

    private static async Task<NormalizacionResult> CodificarComoWebpAsync(Image image, int calidad)
    {
        using var ms = new MemoryStream();
        await image.SaveAsWebpAsync(ms, new WebpEncoder
        {
            Quality = calidad,
            FileFormat = WebpFileFormatType.Lossy
        });
        return new NormalizacionResult(ms.ToArray(), image.Width, image.Height, ".webp", "image/webp");
    }

    private static async Task<NormalizacionResult> CodificarComoPngAsync(Image image)
    {
        using var ms = new MemoryStream();
        await image.SaveAsPngAsync(ms);
        return new NormalizacionResult(ms.ToArray(), image.Width, image.Height, ".png", "image/png");
    }

    private static async Task<NormalizacionResult> CodificarComoJpegAsync(Image image, int calidad)
    {
        using var ms = new MemoryStream();
        await image.SaveAsJpegAsync(ms, new JpegEncoder { Quality = calidad });
        return new NormalizacionResult(ms.ToArray(), image.Width, image.Height, ".jpg", "image/jpeg");
    }
}
