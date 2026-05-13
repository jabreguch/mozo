using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace Mozo.Comun.Helper.Global;
//public interface IMemoryCache : IDisposable
//{
//    bool TryGetValue(object key, out object value);
//    ICacheEntry CreateEntry(object key);
//    void Remove(object key);
//}
//public static class CacheExtensions
//{
//    public static TItem Get<TItem>(this IMemoryCache cache, object key);

//    public static TItem Set<TItem>(this IMemoryCache cache, object key, TItem value, MemoryCacheEntryOptions options);

//    public static bool TryGetValue<TItem>(this IMemoryCache cache, object key, out TItem value);

//}

public static partial class Glo
{
    public static void SesionSet<T>(this ISession session, string key, T value)
    {
        var bytes = Encoding.ASCII.GetBytes(value.Serializa());
        session.Set(key, bytes);
    }

    public static T SesionGet<T>(this ISession session, string key)
    {
        string value = null;
        var b = session.TryGetValue(key, out var bytes);
        if (b) value = Encoding.ASCII.GetString(bytes);
        return value == null ? default : value.Deserializa<T>();
    }

    public static List<T> SesionGets<T>(this ISession session, string key)
    {
        string value = null;
        var b = session.TryGetValue(key, out var bytes);
        if (b) value = Encoding.ASCII.GetString(bytes);
        return value == null ? default : value.DeserializaLista<T>();
        //string value = session.GetString(key);
        //return value == null ? default : value.DeserializaLista<T>();
    }


    public static T ByteToObject<T>(this byte[] bytee)
    {
        var options = new JsonSerializerOptions
        {
            AllowTrailingCommas = true
        };
        var value = Encoding.ASCII.GetString(bytee);
        return value == null ? default : value.Deserializa<T>();
    }

    public static List<T> ByteToListObject<T>(this byte[] bytee)
    {
        var options = new JsonSerializerOptions
        {
            AllowTrailingCommas = true
        };
        var value = Encoding.ASCII.GetString(bytee);
        return value == null ? default : value.DeserializaLista<T>();
    }

    public static List<T> SesionGetsAndRemove<T>(this ISession session, string key)
    {
        string value = null;
        var b = session.TryGetValue(key, out var bytes);
        if (b) value = Encoding.ASCII.GetString(bytes);
        session.Remove(key);
        return value == null ? default : value.DeserializaLista<T>();
        //string value = session.GetString(key);
        //session.Remove(key);
        //return value == null ? default : value.DeserializaLista<T>();
    }


    public static T SesionGetAndRemove<T>(this ISession session, string key)
    {
        string value = null;
        byte[] bytes;
        var b = session.TryGetValue(key, out bytes);
        if (b) value = Encoding.ASCII.GetString(bytes);
        session.Remove(key);
        return value == null ? default : value.Deserializa<T>();

        //string value = session.GetString(key);
        //session.Remove(key);
        //return value == null ? default(T) : value.Deserializa<T>();
    }
}