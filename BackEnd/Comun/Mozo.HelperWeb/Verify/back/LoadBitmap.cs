using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using SkiaSharp;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;


namespace Mozo.Comun.Implement.ImageResizer
{
    public partial class ImageResizerMiddleware
    {
        private SKBitmap LoadBitmap(Stream stream, out SKEncodedOrigin origin)
        {
            using (SKManagedStream s = new SKManagedStream(stream))
            {
                using (SKCodec codec = SKCodec.Create(s))
                {
                    origin = codec.EncodedOrigin;
                    SKImageInfo info = codec.Info;
                    SKBitmap bitmap = new SKBitmap(info.Width, info.Height, SKImageInfo.PlatformColorType, info.IsOpaque ? SKAlphaType.Opaque : SKAlphaType.Premul);

                    IntPtr length;
                    SKCodecResult result = codec.GetPixels(bitmap.Info, bitmap.GetPixels(out length));
                    if (result == SKCodecResult.Success || result == SKCodecResult.IncompleteInput)
                    {
                        return bitmap;
                    }
                    else
                    {
                        throw new ArgumentException("Unable to load bitmap from provided data");
                    }
                }
            }
        }
    }
}
