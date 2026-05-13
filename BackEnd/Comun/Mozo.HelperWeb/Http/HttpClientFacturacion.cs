using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using Mozo.Model;
using System.Net.Http;
using System.Threading.Tasks;

namespace Mozo.Comun.Implement.Http;
public interface IHttpClientFacturacion : IHttpClientGeneric
{

}

public class HttpClientFacturacion : IHttpClientFacturacion
{
    private readonly IMemoryCache _cache;
    private readonly HttpClient _client;
    private readonly ConfigurationModel _configuration;

    public HttpClientFacturacion(HttpClient client, IMemoryCache cache, IOptions<ConfigurationModel> configuration)
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


}