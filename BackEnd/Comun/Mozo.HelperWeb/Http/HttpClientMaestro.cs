using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using Mozo.Comun.Helper.Global;
using Mozo.Model;
using Mozo.Model.Maestro;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Mozo.Comun.Implement.Http;

public interface ITipo
{
    Task<IEnumerable<TipoModel>> GetAll(TipoModel c);
    Task<IEnumerable<TipoModel>> GetAllHijo(TipoModel c);
    Task<IEnumerable<TipoModel>> GetAllGrupo(TipoModel c);
    Task<IEnumerable<TipoModel>> GetAllHijoXGrupo(TipoModel c);
}

public interface ITipoGeneral
{
    Task<IEnumerable<TipoGeneralModel>> GetAll(TipoGeneralModel c);
}

public interface IHttpClientMaestro : IHttpClientGeneric, ITipo, ITipoGeneral
{
    Task<SelectList> Persona_GetAll(PersonaModel c, string CoPersona = null);
    Task<SelectList> Ubigeo_GetAllDistrito(UbigeoModel c, string CoDistrito = null);
    Task<SelectList> Ubigeo_GetAllProvincia(UbigeoModel c, string CoProvincia = null);
    Task<SelectList> Ubigeo_GetAllDepartamento(string CoDepartamento = null);
}

public class HttpClientMaestro : IHttpClientMaestro
{
    private readonly IMemoryCache _cache;
    private readonly HttpClient _client;
    private readonly ConfigurationModel _configuration;

    public HttpClientMaestro(HttpClient client, IMemoryCache cache, IOptions<ConfigurationModel> configuration)
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


    public async Task<SelectList> Persona_GetAll(PersonaModel c, string CoPersona = null)
    {
        var list = await HttpClientUtil.All<IEnumerable<PersonaModel>>(_client, "PersonaApi/GetAllActivo", c);
        return new SelectList(list, "CoPersona", "NoNombreCompleto", CoPersona);
    }

    //public async Task<SelectList> TipoGeneral_GetAllModulo(TipoGeneralModel c, string CoTipo = null, bool FlSigla = false)
    //{
    //    IEnumerable<TipoGeneralModel> list = await HttpClientUtil.All<IEnumerable<TipoGeneralModel>>(_client, "TipoGeneralApi/GetAllModuloActivo", c);
    //    return new SelectList(list, "CoTipo", FlSigla == false ? "NoTipo" : "NoSigla", CoTipo);
    //}

    public async Task<SelectList> Ubigeo_GetAllDistrito(UbigeoModel c, string CoDistrito = null)
    {
        var list = await HttpClientUtil.All<IEnumerable<UbigeoModel>>(_client, "UbigeoApi/GetAllDistrito", c);
        return new SelectList(list, "CoDistrito", "NoDistrito", CoDistrito);
    }

    public async Task<SelectList> Ubigeo_GetAllProvincia(UbigeoModel c, string CoProvincia = null)
    {
        var list = await HttpClientUtil.All<IEnumerable<UbigeoModel>>(_client, "UbigeoApi/GetAllProvincia", c);
        return new SelectList(list, "CoProvincia", "NoProvincia", CoProvincia);
    }

    public async Task<SelectList> Ubigeo_GetAllDepartamento(string CoDepartamento = null)
    {
        IEnumerable<UbigeoModel> list = null;
        if (_cache.TryGetValue("Ubigeo_GetAllDepartamento", out list))
        {
        }
        else
        {
            list = await HttpClientUtil.All<IEnumerable<UbigeoModel>>(_client, "UbigeoApi/GetAllDepartamento",
                new UbigeoModel());

            list.Where(c => c.CoDepartamento.Num() > 25).ToList().ForEach(cc => cc.NoTipoDocumento = "Exterior");
            list.Where(c => c.CoDepartamento.Num() <= 25).ToList().ForEach(cc => cc.NoTipoDocumento = "Interior");
            _cache.Set("Ubigeo_GetAllDepartamento", list);
        }

        return new SelectList(list, "CoDepartamento", "NoDepartamento", CoDepartamento, "NoTipoDocumento");
    }

    public async Task<IEnumerable<TipoGeneralModel>> GetAll(TipoGeneralModel c)
    {
        IEnumerable<TipoGeneralModel> list = null;
        if (_cache.TryGetValue("TipoGeneral_GetAll_" + c.CoGrupo, out list))
        {
        }
        else
        {
            list = await HttpClientUtil.All<IEnumerable<TipoGeneralModel>>(_client, "TipoGeneralApi/GetAllActivo", c);
            _cache.Set("TipoGeneral_GetAll_" + c.CoGrupo, list);
        }

        return list;
    }

    public async Task<IEnumerable<TipoModel>> GetAll(TipoModel c)
    {
        var list = await HttpClientUtil.All<IEnumerable<TipoModel>>(_client, "TipoApi/GetAllActivo", c);
        //if (_cache.TryGetValue("Tipo_GetAll_" + c.CoGrupo, out list))
        //{

        //}
        //else
        //{
        //    list = await HttpClientUtil.All<IEnumerable<TipoModel>>(_client, "TipoApi/GetAllActivo", c);
        //    _cache.Set("Tipo_GetAll_" + c.CoGrupo, list);
        //}
        return list;

        //IEnumerable<TipoModel> list = await HttpClientUtil.All<IEnumerable<TipoModel>>(_client, "TipoApi/GetAllActivo", c);

        //if (CoTipoIncluye > 0)
        //{
        //    list = list.Where(x => x.NoSigla.Contains(CoTipoIncluye.ToString()));
        //}
        //return new SelectList(list, "CoTipo", FlSigla == false ? "NoTipo" : "NoSigla", CoTipo);
    }

    public async Task<IEnumerable<TipoModel>> GetAllHijo(TipoModel c)
    {
        var list = await HttpClientUtil.All<IEnumerable<TipoModel>>(_client, "TipoApi/GetAllHijoActivo", c);
        return list;
    }

    public async Task<IEnumerable<TipoModel>> GetAllGrupo(TipoModel c)
    {
        var list = await HttpClientUtil.All<IEnumerable<TipoModel>>(_client, "TipoApi/GetAllGrupo", c);
        return list;
    }

    //public async Task<SelectList> Tipo_GetAllHijoXGrupo(TipoModel c, string CoTipo = null)
    //{

    //    return new SelectList(list, "CoSubTipo", "NoSubTipo", CoTipo, "NoTipo");
    //}


    public async Task<IEnumerable<TipoModel>> GetAllHijoXGrupo(TipoModel c)
    {
        var list = await HttpClientUtil.All<IEnumerable<TipoModel>>(_client, "TipoApi/GetAllHijoActivoXGrupo", c);
        return list;
    }
}