using Microsoft.Extensions.Caching.Memory;
using Mozo.Comun.Helper.Enu;
using Mozo.Comun.Helper.Global;
using Mozo.Comun.Implement.Mvc;
using Mozo.Model.Seguridad;
using System;
using System.Net.Http;
using System.Text;

namespace Mozo.Comun.Implement.Http;

public interface IHttpClientToken
{
    GlobalCredentialTokenModel PostTokenAsync();
}

public class HttpClientToken : IHttpClientToken
{
    private readonly IMemoryCache _cache;
    private readonly HttpClient _client;

    //private readonly ConfigurationModel _configuration;

    public HttpClientToken(
        HttpClient client
        , IMemoryCache cache
    )
    {
        _client = client ?? throw new ArgumentNullException(nameof(client));
        _cache = cache;
    }

    public GlobalCredentialTokenModel PostTokenAsync()
    {
        var Credential = Yo.GetCredential();

        var Token = Credential.Token;
        //if (Token == null)
        //{
        //    Token = PostNewTokenAsync(Credential);
        //}
        //else
        //{

        //}

        var FeUtcExpiration = Token.FeExpiration.FechaUtc().AddMinutes(EnuCommon.GLOBAL_TOTAL_MINUTE_EXTRA_TOKEN);
        var FeUtcNow = DateTimeOffset.UtcNow.DateTime;
        if (FeUtcNow > FeUtcExpiration) Token = PostReNewTokenAsync(Credential);

        return Token;
    }

    //        using (var client = new HttpClient())
    //{
    //    var response = client.GetAsync("http://google.com").Result;

    //    if (response.IsSuccessStatusCode)
    //    {
    //        var responseContent = response.Content;

    //    // by calling .Result you are synchronously reading the result
    //    string responseString = responseContent.ReadAsStringAsync().Result;

    //    Console.WriteLine(responseString);
    //    }
    //}

    //public GlobalTokenModel PostNewTokenAsync(GlobalCredentialModel c)
    //{
    //    GlobalTokenModel Token = null;
    //    PermisoModel Permiso = new()
    //    {
    //        CoEmpresa = c.CoEmpresa,
    //        CoPermiso = c.Permiso.CoPermiso,
    //        NoUsuario = c.Permiso.NoUsuario,
    //        NoClave = c.Permiso.NoClave
    //    };

    //    StringContent data = new StringContent(Permiso.Serializa(), Encoding.UTF8, "application/json");
    //    HttpResponseMessage httpResponse = _client.PostAsync("Cuenta/PermisoApi/Token", data).Result;

    //    if (httpResponse.IsSuccessStatusCode)
    //    {
    //        HttpContent httpContent = httpResponse.Content;
    //        byte[] StreamToken = httpContent.ReadAsByteArrayAsync().Result;
    //        Token = StreamToken.DeserializaByte<GlobalTokenModel>();
    //    }
    //    Yo.SetToken(Token);
    //    return Token;
    //}

    public GlobalCredentialTokenModel PostReNewTokenAsync(GlobalCredentialModel Credential)
    {
        //CoEmpresa = c.CoEmpresa, CoIngreso = c.CoIngreso, copersona
        //if (c.NoRefreshToken != Ingreso.NoRefreshToken)
        IngresoModel Ingreso = new()
        {
            CoEmpresa = Credential.CoEmpresa,
            CoIngreso = Credential.Ingreso.CoIngreso,
            CoPersona = Credential.CoPersona,
            NoRefreshToken = Credential.Token.NoRefresh
        };

        var data = new StringContent(Ingreso.Serializa(), Encoding.UTF8, "application/json");
        var httpResponse = _client.PostAsync("Cuenta/IngresoApi/RenewToken", data).Result;

        if (httpResponse.IsSuccessStatusCode)
        {
            var httpContent = httpResponse.Content;
            var StreamToken = httpContent.ReadAsByteArrayAsync().Result;
            Credential.Token = StreamToken.DeserializaByte<GlobalCredentialTokenModel>();
        }

        Yo.SetCredential(Credential);

        return Credential.Token;
    }
}