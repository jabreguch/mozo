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
    public static class ImageByteResizer
    {
        public static byte[] Resize(this byte[] fileContents,
        int? Width, int? Height,
        SKFilterQuality quality = SKFilterQuality.Medium)
        {
            if (Width == null && Height == null)
            {
                return fileContents;
            }

            using MemoryStream ms = new MemoryStream(fileContents);
            using SKBitmap sourceBitmap = SKBitmap.Decode(ms);

            decimal? dWidth = null;
            decimal? dHeight = null;

            if (Width == null || Height == null)
            {
                decimal? scale = Width != null ? ((decimal) Width / sourceBitmap.Width) : ((decimal)Height /sourceBitmap.Height) ;
                dWidth = sourceBitmap.Width * scale;
                dHeight = sourceBitmap.Height * scale;
            }
            else
            {
                dWidth = Width > sourceBitmap.Width ? sourceBitmap.Width : Width;
                dHeight = Height > sourceBitmap.Height ? sourceBitmap.Height : Height;
            }

            int height = Math.Min((int)dHeight, sourceBitmap.Height);
            int width = Math.Min((int)dWidth, sourceBitmap.Width);

            using SKBitmap scaledBitmap = sourceBitmap.Resize(new SKImageInfo(width, height), quality);
            using SKImage scaledImage = SKImage.FromBitmap(scaledBitmap);
            using SKData data = scaledImage.Encode();

            return data.ToArray();
        }

       // public (byte[] FileContents, int Height, int Width) Scale(byte[] fileContents, int scale,
       //SKFilterQuality quality = SKFilterQuality.Medium)
       // {
       //     using MemoryStream ms = new MemoryStream(fileContents);
       //     using SKBitmap sourceBitmap = SKBitmap.Decode(ms);

       //     int maxWidth, maxHeight;

       //     maxWidth = sourceBitmap.Width * scale;
       //     maxHeight = sourceBitmap.Height * scale;

       //     int height = Math.Min(maxHeight, sourceBitmap.Height);
       //     int width = Math.Min(maxWidth, sourceBitmap.Width);

       //     using SKBitmap scaledBitmap = sourceBitmap.Resize(new SKImageInfo(width, height), quality);
       //     using SKImage scaledImage = SKImage.FromBitmap(scaledBitmap);


       //     using SKData data = scaledImage.Encode();

       //     return (data.ToArray(), height, width);
       // }

    }
}
