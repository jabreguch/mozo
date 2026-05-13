using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Mozo.Comun.Helper.Global;
using System.Collections;
using System.Linq;

namespace Mozo.Comun.Implement.Api;

public class ApiFilterAction : IActionFilter //, IAuthorizationFilter
{
    //public override void OnActionExecuting
    // (ActionExecutingContext filterContext)
    //{
    //    string acceptencoding = filterContext.HttpContext.
    //                            equest.Headers["Accept-Encoding"];

    //    if (!string.IsNullOrEmpty(acceptencoding))
    //    {
    //        acceptencoding = acceptencoding.ToLower();
    //        var response = filterContext.HttpContext.Response;
    //        if (acceptencoding.Contains("gzip"))
    //        {
    //            response.AppendHeader("Content-Encoding", "gzip");
    //            response.Filter = new GZipStream(response.Filter,
    //                                  CompressionMode.Compress);
    //        }
    //        else if (acceptencoding.Contains("deflate"))
    //        {
    //            response.AppendHeader("Content-Encoding", "deflate");
    //            response.Filter = new DeflateStream(response.Filter,
    //                              CompressionMode.Compress);
    //        }
    //    }
    //}

    public void OnActionExecuting(ActionExecutingContext context)
    {
        //return;
        var rv = context.RouteData.Values;
        object NoArea, NoControlador, NoAccion;
        rv.TryGetValue("area", out NoArea);
        rv.TryGetValue("controller", out NoControlador);
        rv.TryGetValue("action", out NoAccion);

        //string acceptencoding = context.HttpContext.Request.Headers["Accept-Encoding"];

        //if (!string.IsNullOrEmpty(acceptencoding))
        //{
        //    acceptencoding = acceptencoding.ToLower();
        //    var response = context.HttpContext.Response;
        //    if (acceptencoding.Contains("gzip"))
        //    {
        //        //byte[] barr = Encoding.ASCII.GetBytes("ssdasdasdasdas");
        //        //MemoryStream stream = new MemoryStream(barr);

        //        response.Headers.Add("Content-Encoding", "gzip");
        //        //GZipStream zipStream = new GZipStream(response.Body, CompressionMode.Compress);
        //        //zipStream.Write(response.Body, 0, response.Body.Length);

        //        //using (GZipStream zipStream = new GZipStream(fs, CompressionMode.Compress, false))
        //        //{
        //        //    zipStream.Write(bytes, 0, bytes.Length);
        //        //}

        //        response.Body = new GZipStream(response.Body,
        //                              CompressionMode.Compress,false);
        //    }
        //    else if (acceptencoding.Contains("deflate"))
        //    {
        //        response.Headers.Add("Content-Encoding", "deflate");
        //        response.Body = new DeflateStream(response.Body,
        //                          CompressionMode.Compress);
        //    }
        //}

        var FlAllowAnonymous =
            context.ActionDescriptor.EndpointMetadata.Any(em =>
                em.GetType() == typeof(AllowAnonymousAttribute)); //< -- Here it is
        if (FlAllowAnonymous) return;


        Accessor.CoEmpresa = null;
        Accessor.CoPermiso = null;
        Accessor.CoPersona = null;
        //Accessor.NoUsuario = null;
        //Accessor.CoIngreso = null;
        //Accessor.FeUtcIssued = null;
        //Accessor.FeUtcExpiration = null;

        var claimCoPersona = context.HttpContext.User.Claims.First(x => x.Type == "CoPersona");
        if (claimCoPersona != null)
        {
            var descriptor = context.ActionDescriptor as ControllerActionDescriptor;
            if (descriptor != null)
            {
                var parameters = descriptor.MethodInfo.GetParameters();
                foreach (var parameter in parameters)
                {
                    var argument = context.ActionArguments[parameter.Name];
                    var tObject = argument.GetType();
                    if (tObject.Namespace == "System.Collections.Generic")
                    {
                        var collection = (IList)argument;
                        //var argument2 = (IDictionary<string, object>)context.ActionArguments[parameter.Name];
                        foreach (var Item in collection)
                        {
                            var tItem = Item.GetType();
                            var propiedad = tItem.GetProperty("CoUsuarioLogin");
                            if (propiedad != null) propiedad.SetValue(Item, int.Parse(claimCoPersona.Value), null);
                        }
                    }
                    else
                    {
                        var propiedad = tObject.GetProperty("CoUsuarioLogin");
                        if (propiedad != null) propiedad.SetValue(argument, int.Parse(claimCoPersona.Value), null);
                    }
                }
            }

            Accessor.CoEmpresa = context.HttpContext.User.Claims.First(x => x.Type == "CoEmpresa").Value.Num();
            Accessor.CoPermiso = context.HttpContext.User.Claims.First(x => x.Type == "CoPermiso").Value.Num();
            Accessor.CoPersona = context.HttpContext.User.Claims.First(x => x.Type == "CoPersona").Value.Num();

            // Accessor.CoIngreso = context.HttpContext.User.Claims.First(x => x.Type == "CoIngreso").Value.Num();

            // Accessor.NoUsuario = context.HttpContext.User.Claims.First(x => x.Type == "NoUsuario").Value;
            //  Accessor.FeUtcIssued = long.Parse(context.HttpContext.User.Claims.First(claim => claim.Type == "iat").Value);
            //  Accessor.FeUtcExpiration = long.Parse(context.HttpContext.User.Claims.First(claim => claim.Type == "exp").Value);
        }

        //     if (context.HttpContext.Request.Headers != null)
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
    }
}