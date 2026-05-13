using System.Data;
using System.Reflection;
using System.Text.Json;
using System.Web;

namespace Mozo.Helper.Global;


public static partial class Glo
{
    public static readonly IEnumerable<string> MimeTypesCompress = new[]
    {
        "image/svg+xml",
        "application/atom+xml",
        // General
        "text/plain",
        // Static files
        "text/css",
        "application/javascript",
        // MVC
        "text/html",
        "application/xml",
        "text/xml",
        "application/json",
        "text/json"
    };

    public static string ToParam(this object obj)
    {
        IEnumerable<string> properties = from p in obj.GetType().GetProperties()
                                         where p.GetValue(obj, null) != null
                                         select p.Name + "=" + HttpUtility.UrlEncode(p.GetValue(obj, null)!.ToString());

        return string.Join("&", properties.ToArray());
    }



    public static string Serializa(this object? value)
    {
        JsonSerializerOptions options = new JsonSerializerOptions
        {
            AllowTrailingCommas = true,
            WriteIndented = false,
            //IgnoreNullValues = true
            DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull
        };

        return JsonSerializer.Serialize(value, options);
    }

    public static List<T>? DeserializaLista<T>(this string data)
    {
        if (data.EsNulo()) return new List<T>();
        JsonSerializerOptions options = new JsonSerializerOptions
        {
            AllowTrailingCommas = true,
            WriteIndented = true,
            DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull
            //IgnoreNullValues = true
        };
        return JsonSerializer.Deserialize<List<T>>(data, options);
    }

    public static T? Deserializa<T>(this string data)
    {
        JsonSerializerOptions options = new()
        {
            AllowTrailingCommas = true,
            PropertyNameCaseInsensitive = true
        };

        //var options = new JsonSerializerOptions
        //{
        //  PropertyNameCaseInsensitive = true // 👈 esto hace que ignore mayúsculas/minúsculas
        //};


        return JsonSerializer.Deserialize<T>(data, options);
    }

    public static T? DeserializaByte<T>(this byte[] bytee)
    {
        if (bytee == null || bytee.Length == 0)
            return default!;

        JsonSerializerOptions options = new JsonSerializerOptions
        {
            AllowTrailingCommas = true
        };

        return JsonSerializer.Deserialize<T>(bytee, options);
    }

    public static async Task<T?> DeserializaStream<T>(this Stream stream)
    {
        if (stream == null || stream.CanRead == false)
            return default!;

        var options = new JsonSerializerOptions
        {
            AllowTrailingCommas = true
        };

        return await JsonSerializer.DeserializeAsync<T>(stream, options);
    }

    public static async Task<string?> StreamToStringAsync(Stream stream)
    {
        string? content = null;

        if (stream != null)
            using (var sr = new StreamReader(stream))
            {
                content = await sr.ReadToEndAsync();
            }

        return content;
    }

    public static string GetQueryString2(this object obj, string? prefix = null)
    {
        List<string> list = new List<string>();
        PropertyInfo[] propertyLst = obj.GetType().GetProperties();
        foreach (PropertyInfo p in propertyLst)
        {
            object? v = p.GetValue(obj, null);
            if (v != null && v.NoNulo())
            {
                string k = prefix != null ? prefix + "." + p.Name : p.Name;
                string pr = (v != null && v.GetType().Name != "String" && v.GetType().Name != "Int32") ?
                    GetQueryString2(v, k) :
                    Uri.EscapeDataString(k) + "=" + Uri.EscapeDataString(v.Text());
                list.Add(pr);
            }
        }
        return string.Join("&", list.ToArray());
    }








    public static T With<T>(this T item, Action<T> action)
    {
        action(item);
        return item;
    }




}
