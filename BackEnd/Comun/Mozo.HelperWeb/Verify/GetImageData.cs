using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using SkiaSharp;
using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Mozo.Comun.Implement.Verify;

public partial class VerifyMiddleware
{
    private ResizeParams GetResizeParams(PathString path, IQueryCollection query)
    {
        var resizeParams = new ResizeParams();

        // before we extract, do a quick check for resize params
        resizeParams.hasParams =
            resizeParams.GetType().GetTypeInfo()
                .GetFields().Where(f => f.Name != "hasParams")
                .Any(f => query.ContainsKey(f.Name));

        // if no params present, bug out
        if (!resizeParams.hasParams)
            return resizeParams;

        // extract resize params

        if (query.ContainsKey("format"))
            resizeParams.format = query["format"];
        else
            resizeParams.format = path.Value.Substring(path.Value.LastIndexOf('.') + 1);

        if (query.ContainsKey("autorotate"))
            bool.TryParse(query["autorotate"], out resizeParams.autorotate);

        var quality = 100;
        if (query.ContainsKey("quality"))
            int.TryParse(query["quality"], out quality);
        resizeParams.quality = quality;

        var w = 0;
        if (query.ContainsKey("w"))
            int.TryParse(query["w"], out w);
        resizeParams.w = w;

        var h = 0;
        if (query.ContainsKey("h"))
            int.TryParse(query["h"], out h);
        resizeParams.h = h;

        resizeParams.mode = "max";
        // only apply mode if it's a valid mode and both w and h are specified
        if (h != 0 && w != 0 && query.ContainsKey("mode") && ResizeParams.modes.Any(m => query["mode"] == m))
            resizeParams.mode = query["mode"];

        return resizeParams;
    }

    private SKData GetImageData(string imagePath, ResizeParams resizeParams, DateTime lastWriteTimeUtc)
    {
        // check cache and return if cached
        long cacheKey;
        unchecked
        {
            cacheKey = imagePath.GetHashCode() + lastWriteTimeUtc.ToBinary() + resizeParams.ToString().GetHashCode();
        }

        SKData imageData;
        byte[] imageBytes;
        var isCached = _memoryCache.TryGetValue(cacheKey, out imageBytes);
        //bool isCached = _memoryCache.TryGetValue(cacheKey, out imageBytes);
        if (isCached)
            //_logger.LogInformation("Serving from cache");
            return SKData.CreateCopy(imageBytes);

        SKEncodedOrigin origin; // this represents the EXIF orientation
        var bitmap =
            LoadBitmap(File.OpenRead(imagePath),
                out origin); // always load as 32bit (to overcome issues with indexed color)

        // if autorotate = true, and origin isn't correct for the rotation, rotate it
        if (resizeParams.autorotate && origin != SKEncodedOrigin.TopLeft)
            bitmap = RotateAndFlip(bitmap, origin);

        // if either w or h is 0, set it based on ratio of original image
        if (resizeParams.h == 0)
            resizeParams.h = (int)Math.Round(bitmap.Height * (float)resizeParams.w / bitmap.Width);
        else if (resizeParams.w == 0)
            resizeParams.w = (int)Math.Round(bitmap.Width * (float)resizeParams.h / bitmap.Height);

        // if we need to crop, crop the original before resizing
        if (resizeParams.mode == "crop")
            bitmap = Crop(bitmap, resizeParams);

        // store padded height and width
        var paddedHeight = resizeParams.h;
        var paddedWidth = resizeParams.w;

        // if we need to pad, or max, set the height or width according to ratio
        if (resizeParams.mode == "pad" || resizeParams.mode == "max")
        {
            var bitmapRatio = (float)bitmap.Width / bitmap.Height;
            var resizeRatio = (float)resizeParams.w / resizeParams.h;

            if (bitmapRatio > resizeRatio) // original is more "landscape"
                resizeParams.h = (int)Math.Round(bitmap.Height * ((float)resizeParams.w / bitmap.Width));
            else
                resizeParams.w = (int)Math.Round(bitmap.Width * ((float)resizeParams.h / bitmap.Height));
        }

        // resize
        var resizedImageInfo =
            new SKImageInfo(resizeParams.w, resizeParams.h, SKImageInfo.PlatformColorType, bitmap.AlphaType);
        var resizedBitmap = bitmap.Resize(resizedImageInfo, SKFilterQuality.High); //  SKBitmapResizeMethod.Lanczos3);
        //SKBitmap resizedBitmap = bitmap.Resize(resizedImageInfo, SKFilterQuality.High);
        //SKBitmap resizedBitmap = bitmap.Resize(resizedImageInfo, SKBitmapResizeMethod. );

        // optionally pad
        if (resizeParams.mode == "pad")
            resizedBitmap = Pad(resizedBitmap, paddedWidth, paddedHeight, resizeParams.format != "png");

        // encode
        if (resizedBitmap != null)
        {
            var resizedImage = SKImage.FromBitmap(resizedBitmap);
            var encodeFormat = resizeParams.format == "png" ? SKEncodedImageFormat.Png : SKEncodedImageFormat.Jpeg;
            imageData = resizedImage.Encode(encodeFormat, resizeParams.quality);
            // cache the result
            _memoryCache.Set(cacheKey, imageData.ToArray());
            // cleanup
            resizedImage.Dispose();
            bitmap.Dispose();
            resizedBitmap.Dispose();
            return imageData;
        }

        return null;
    }
}