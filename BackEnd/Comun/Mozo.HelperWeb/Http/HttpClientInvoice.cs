using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using Mozo.Model;
using Mozo.Model.Inventario;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Mozo.Comun.Implement.Http;

public interface IHttpClientInvoice : IHttpClientGeneric
{
    Task<SelectList> Inventario_Almacen_GetAll(ArticuloStockModel c, string CoAlmacen = null);
}

public class HttpClientInvoice : IHttpClientInvoice
{
    private readonly IMemoryCache _cache;
    private readonly HttpClient _client;
    private readonly ConfigurationModel _configuration;

    public HttpClientInvoice(HttpClient client, IMemoryCache cache, IOptions<ConfigurationModel> configuration)
    {
        _client = client;
        _cache = cache;
        _configuration = configuration.Value;
        _client.DefaultRequestHeaders.Add("Accept-Encoding", "gzip");
    }

    public async Task<T> All<T>(string NoUrl, object Model, int security = 1)
    {
        return await HttpClientUtil.All<T>(_client, NoUrl, Model, security);
    }

    public async Task<T> One<T>(string NoUrl, object Model, int security = 1)
    {
        return await HttpClientUtil.One<T>(_client, NoUrl, Model, security);
    }

    public async Task<T> Post<T>(string NoUrl, object Model, int security = 1)
    {
        return await HttpClientUtil.Post<T>(_client, NoUrl, Model, security);
    }

    //public async Task<SelectList> Local_GetAll()
    //{
    //    IEnumerable<LocalModel> list = await HttpClientUtil.All<IEnumerable<LocalModel>>(_client, "LocalApi/GetAllActivo", new LocalModel());
    //    return new SelectList(list, "CoLocal", "NoLocal");
    //}

    public async Task<SelectList> Inventario_Almacen_GetAll(ArticuloStockModel c, string CoAlmacen = null)
    {
        var list = await HttpClientUtil.All<IEnumerable<ArticuloStockModel>>(_client,
            "Inventario/AlmacenApi/GetAllActive", c);
        return new SelectList(list, "CoAlmacen", "NoLocalAndAlmacen", CoAlmacen, "NoLocal");
    }
}