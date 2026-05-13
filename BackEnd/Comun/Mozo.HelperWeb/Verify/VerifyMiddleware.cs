using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using Mozo.Comun.Implement.Mvc;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Mozo.Comun.Implement.Verify;

public partial class VerifyMiddleware
{
    private static readonly string[] suffixes =
    {
        ".png",
        ".jpg",
        ".jpeg"
    };

    private readonly IWebHostEnvironment _env;
    private readonly IMemoryCache _memoryCache;

    private readonly RequestDelegate _next;


    public VerifyMiddleware(RequestDelegate next, IWebHostEnvironment env, IMemoryCache memoryCache)
    {
        _next = next;
        _env = env;
        _memoryCache = memoryCache;
    }


    public static void Main()
    {
        string[] strings =
        {
            "This is a string.", "Hello!", "Nothing.",
            "Yes.", "randomize"
        };
        foreach (var value in strings)
        {
            var endsInPeriod = value.EndsWith(".");
            Console.WriteLine("'{0}' ends in a period: {1}",
                value, endsInPeriod);
        }
    }

    private bool IsImagePath(PathString path)
    {
        if (path == null || !path.HasValue)
            return false;

        foreach (var value in suffixes)
            if (path.Value.EndsWith(value))
                return true;
        //bool endsInPeriod = value.EndsWith(".");
        //Console.WriteLine("'{0}' ends in a period: {1}",
        //                value, endsInPeriod);
        return false;
        ///return suffixes.Any(x => x.EndsWith(path, StringComparison.OrdinalIgnoreCase));
    }

    public async Task Invoke(HttpContext httpContext)
    {
        var path = httpContext.Request.Path;
        try
        {
            //if (path.Value.Contains("1.jpg"))
            //{
            //    string dd = "";
            //}

            //if (httpContext.Request.Query.Count == 0 || !IsImagePath(path))
            if (!IsImagePath(path))
            {
                await _next.Invoke(httpContext);
                //return;
            }
            else
            {
                await _next.Invoke(httpContext);



                return;
                // hand to next middleware if we are dealing with an image but it doesn't have any usable resize querystring params
                var resizeParams = GetResizeParams(path, httpContext.Request.Query);
                if (!resizeParams.hasParams || (resizeParams.w == 0 && resizeParams.h == 0))
                {
                    await _next.Invoke(httpContext);
                    return;
                }

                // if we got this far, resize it
                //_logger.LogInformation($"Resizing {path.Value} with params {resizeParams}");

                // get the image location on disk


                //string TxDirectoryRoot = Path.Combine(Yo.Configuration.GetSection("AppSettings").GetSection("FolderRoot").Value, Yo.Configuration.GetSection("AppSettings").GetSection("FolderDocument").Value);
                var TxDirectoryRoot = ServiceProviderYo.Configuration.GetSection("AppSettings").GetSection("FolderRoot")
                    .Value;


                //string imagePath = Path.Combine(_env.WebRootPath, path.Value.Replace('/', Path.DirectorySeparatorChar).TrimStart(Path.DirectorySeparatorChar));
                var imagePath = Path.Combine(TxDirectoryRoot,
                    path.Value.Replace('/', Path.DirectorySeparatorChar).TrimStart(Path.DirectorySeparatorChar));

                // check file lastwrite
                var lastWriteTimeUtc = File.GetLastWriteTimeUtc(imagePath);
                if (lastWriteTimeUtc.Year == 1601) // file doesn't exist, pass to next middleware
                {
                    await _next.Invoke(httpContext);
                    return;
                }

                var imageData = GetImageData(imagePath, resizeParams, lastWriteTimeUtc);

                // write to stream
                httpContext.Response.ContentType = resizeParams.format == "png" ? "image/png" : "image/jpeg";
                httpContext.Response.ContentLength = imageData.Size;
                await httpContext.Response.Body.WriteAsync(imageData.ToArray(), 0, (int)imageData.Size);

                // cleanup
                imageData.Dispose();
            }
        }
        catch (Exception ex)
        {
            throw ex;
            //_logger.LogError($"Se detecto un error: {ex}");
            //await HandleExceptionAsync(httpContext, ex);
        }
    }

    private struct ResizeParams
    {
        public bool hasParams;
        public int w;
        public int h;
        public bool autorotate;
        public int quality; // 0 - 100
        public string format; // png, jpg, jpeg
        public string mode; // pad, max, crop, stretch
        public static readonly string[] modes = { "pad", "max", "crop", "stretch" };

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append($"w: {w}, ");
            sb.Append($"h: {h}, ");
            sb.Append($"autorotate: {autorotate}, ");
            sb.Append($"quality: {quality}, ");
            sb.Append($"format: {format}, ");
            sb.Append($"mode: {mode}");

            return sb.ToString();
        }
    }


    //private Task HandleExceptionAsync(HttpContext context, Exception exception)
    //{
    //    context.Response.ContentType = "application/json";
    //    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

    //    string NoMensaje = exception.Message;
    //    string NoOrigen = exception.Source.Text();
    //    MensajeModel Mensaje;
    //    if (NoMensaje.Contains("|"))
    //    {
    //        int NuInicio = NoMensaje.IndexOf("|");
    //        int NuFinal = NoMensaje.LastIndexOf("|");
    //        NoMensaje = NoMensaje.Substring(NuInicio + 1, (NuFinal - NuInicio) - 1);
    //        Mensaje = new MensajeModel() { NoMensaje = NoMensaje, CoTipoMensaje = EnuCommon.CoTipoMensaje.Peligro };
    //    }
    //    else
    //    {
    //        Mensaje = new MensajeModel() { NoMensaje = NoMensaje, CoTipoMensaje = EnuCommon.CoTipoMensaje.Peligro };
    //    }

    //    Mensaje.CoTipoMensaje = EnuCommon.CoTipoMensaje.Peligro;


    //    context.Response.ContentType = "application/json";

    //    return context.Response.WriteAsync(Mensaje.Serializa());

    //    //context.Response = context.Request.CreateErrorResponse(HttpStatusCode.BadRequest, m.Serializar());

    //}
}

public static class VerifyMiddlewareExtensions
{
    public static IServiceCollection AddVerify(this IServiceCollection services)
    {
        return services.AddMemoryCache();
    }

    public static IApplicationBuilder UseVerify(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<VerifyMiddleware>();
    }
}

//public static class ExceptionMiddlewareExtensions
//{
//    public static void ConfigureCustomExceptionMiddleware(this IApplicationBuilder app)
//    {
//        app.UseMiddleware<VerifyMiddleware>();
//    }
//}