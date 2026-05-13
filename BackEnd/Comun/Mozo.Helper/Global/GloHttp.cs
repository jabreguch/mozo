using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;

using System.Reflection;

namespace Mozo.Helper.Global;

public static partial class Glo
{
    //100-199	100-101	Informational
    //200–299	200–206	Successful
    //300–399	300–305	Redirection
    //400–499	400–415	Client error
    //500–599	500–505	Server error
    public static string[] StatusHttp(int? code)
    {
        return code switch
        {
            100 => new[] { "100", "Continue", "continue" },
            101 => new[] { "101", "Switching Protocols", "switching-protocols" },
            >= 102 and <= 199 => new[] { "100", "Informational", "informational" },
            200 => new[] { "200", "OK", "ok" },
            201 => new[] { "201", "Created", "created" },
            202 => new[] { "202", "Accepted", "accepted" },
            203 => new[] { "203", "Non-Authoritative Information", "non-Authoritative" },
            204 => new[] { "204", "No Content", "no-content" },
            205 => new[] { "205", "Reset Content", "reset-content" },
            206 => new[] { "206", "Partial Content", "partial-content" },
            >= 207 and <= 299 => new[] { "200", "Successful", "continue" },
            300 => new[] { "300", "Multiple Choice", "multiple-choice" },
            301 => new[] { "301", "Moved Permanently", "moved-permanently" },
            302 => new[] { "302", "Found", "found" },
            303 => new[] { "303", "See Other", "see-other" },
            304 => new[] { "304", "Not Modified", "not-modified" },
            305 => new[] { "305", "Use Proxy", "use-proxy" },
            306 => new[] { "306", "(Unused)", "unused" },
            307 => new[] { "307", "Temporary Redirect", "temporary-redirect" },
            >= 308 and <= 399 => new[] { "300", "Redirection", "redirection" },
            400 => new[] { "400", "Bad Request", "bad-request" },
            401 => new[] { "401", "Unauthorized", "unauthorized" },
            402 => new[] { "402", "Payment Required", "payment-required" },
            403 => new[] { "403", "Forbidden", "forbidden" },
            404 => new[] { "404", "Not Found", "not-found" },
            405 => new[] { "405", "Method Not Allowed", "method-not-allowed" },
            406 => new[] { "406", "Not Acceptable", "not-acceptable" },
            407 => new[] { "407", "Proxy Authentication Required", "proxy-authentication-required" },
            408 => new[] { "408", "Request Timeout", "request-timeout" },
            409 => new[] { "409", "Conflict", "conflict" },
            410 => new[] { "410", "Gone", "gone" },
            411 => new[] { "411", "Length Required", "length-required" },
            412 => new[] { "412", "Precondition Failed", "precondition-failed" },
            413 => new[] { "413", "Request Entity Too Large", "request-entity-too-large" },
            414 => new[] { "414", "Request URI Too Long", "request-uri-too-long" },
            415 => new[] { "415", "Unsupported Media Type", "unsupported-media-type" },
            416 => new[] { "416", "Requested Range Not Satisfiable", "requested-range-not-satisfiable" },
            417 => new[] { "417", "Expectation Failed", "expectation-failed" },
            >= 418 and <= 499 => new[] { "400", "Client error", "client-error" },
            500 => new[] { "500", "Internal Server Error", "internal-server-error" },
            501 => new[] { "501", "Not Implemented", "not-implemented" },
            502 => new[] { "502", "Bad Gateway", "bad-gateway" },
            503 => new[] { "503", "Service Unavailable", "service-unavailable" },
            504 => new[] { "504", "Gateway Timeout", "gateway-timeout" },
            505 => new[] { "505", "HTTP Version Not Supported", "http-version-not-supported" },
            >= 506 and <= 599 => new[] { "500", "Server error", "server-error" },
            _ => new[] { "000", "Not determine", "not-determine" } // default value
        };
    }

    public static T? GetValue<T>(this HttpContext ctx, string fieldName, T? valueDefault = default) where T : IParsable<T>
    {
        StringValues valor = ctx.Request.Query[fieldName];
        if (StringValues.IsNullOrEmpty(valor))
            return valueDefault;

        return T.Parse(valor!, null);
    }

    public static T? GetModel<T>(this HttpContext ctx)
    {
        T model = Activator.CreateInstance<T>();
        foreach (KeyValuePair<string, StringValues> item in ctx.Request.Query)
        {
            PropertyInfo? property = model!.GetType().GetProperty(item.Key);
            if (property != null && property.CanWrite)
            {
                object value = item.Value.ToString();
                Type? propertyType = property.PropertyType;
                Type? targetType = IsNullableType(propertyType) ? Nullable.GetUnderlyingType(propertyType) : propertyType;
                if (value != null) value = Convert.ChangeType(value, targetType!);
                property.SetValue(model, value, null);
            }
        }
        //model = model.UserClaims(ctx)!;
        return model;
    }

    public static MultipartFormDataContent? GetForm<T>(this T enviar)
    {
        if (enviar == null)
            return default;

        MultipartFormDataContent form = new();
        Type t = enviar.GetType();
        foreach (PropertyInfo runtimeProperty in t.GetRuntimeProperties())
        {
            object? val = runtimeProperty.GetValue(enviar, null);
            if (val != null)
                form.Add(new StringContent(val.ToString()!), runtimeProperty.Name);
        }
        return form;
    }






}