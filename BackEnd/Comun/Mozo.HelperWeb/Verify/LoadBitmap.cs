using SkiaSharp;
using System;
using System.IO;

namespace Mozo.Comun.Implement.Verify;

public partial class VerifyMiddleware
{
    private SKBitmap LoadBitmap(Stream stream, out SKEncodedOrigin origin)
    {
        using (var s = new SKManagedStream(stream))
        {
            using (var codec = SKCodec.Create(s))
            {
                origin = codec.EncodedOrigin;
                var info = codec.Info;
                var bitmap = new SKBitmap(info.Width, info.Height, SKImageInfo.PlatformColorType,
                    info.IsOpaque ? SKAlphaType.Opaque : SKAlphaType.Premul);

                IntPtr length;
                var result = codec.GetPixels(bitmap.Info, bitmap.GetPixels(out length));
                if (result == SKCodecResult.Success || result == SKCodecResult.IncompleteInput)
                    return bitmap;
                throw new ArgumentException("Unable to load bitmap from provided data");
            }
        }
    }
}