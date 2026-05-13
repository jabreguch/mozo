using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using Mozo.Comun.Implement.Mvc;
using Mozo.Model;
using Mozo.Model.Seguridad;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Mozo.Comun.Implement.Http;

public interface IHttpClientSeguridad : IHttpClientGeneric
{
    Task<GlobalCredentialModel> GetCredential(PermisoModel c);
    Task<GlobalCredentialModel> GetCredentialSystem();
    Task GetCompany();

    Task RegisterAccess(string NoIp = null);

    Task<SelectList> Empresa_GetAll();
    Task<SelectList> Modulo_GetAllSinArea();
    Task<SelectList> Modulo_GetAll();
    Task<IEnumerable<ModuloModel>> _Modulo_GetAll();

    Task<SelectList> ModuloUsuario_GetAllModulo(ModuloUsuarioModel c, string CoPersona = null);

    Task<SelectList> Perfil_GetAll(PerfilModel c);

    Task<SelectList> Permiso_GetAll(PermisoModel c);

    Task<SelectList> EmpresaModulo_GetAll(EmpresaModuloModel c, bool FlOnlyTypeMasterTable = false);
}

public class HttpClientSeguridad : IHttpClientSeguridad
{
    private readonly IMemoryCache _cache;
    private readonly HttpClient _client;
    private readonly ConfigurationModel _configuration;

    public HttpClientSeguridad(HttpClient client, IMemoryCache cache, IOptions<ConfigurationModel> configuration)
    {
        _client = client;
        _cache = cache;
        _configuration = configuration.Value;
        _client.DefaultRequestHeaders.Add("Accept-Encoding", "gzip");
    }

    public async Task<T> All<T>(string url, object model, int security = 1)
    {
        return await HttpClientUtil.All<T>(_client, url, model, security);
    }

    public async Task<T> One<T>(string url, object model, int security = 1)
    {
        return await HttpClientUtil.One<T>(_client, url, model, security);
    }

    public async Task<T> Post<T>(string url, object model, int security = 1)
    {
        return await HttpClientUtil.Post<T>(_client, url, model, security);
    }


    public async Task RegisterAccess(string NoIp = null)
    {
        var Credential = Yo.GetCredential();
        var Token = Credential.Token;
        IngresoModel Ingreso = new()
        {
            CoEmpresa = Credential.CoEmpresa,
            CoPersona = Credential.CoPersona,
            CoPermiso = Credential.Permiso.CoPermiso,
            NoIp = NoIp,
            NoRefreshToken = Token.NoRefresh
        };

        Ingreso = await HttpClientUtil.Post<IngresoModel>(_client, "Cuenta/IngresoApi/Insert", Ingreso);
        if (Ingreso != null)
        {
            Credential.Ingreso = new GlobalCredentialIngresoModel();
            Credential.Ingreso.CoIngreso = Ingreso.CoIngreso;
        }

        Yo.SetCredential(Credential);
    }

    public async Task GetCompany()
    {
        var Credential = Yo.GetCredential();
        var Empresa = await HttpClientUtil.One<EmpresaModel>(_client, "EmpresaApi/GetById",
            new EmpresaModel { CoEmpresa = Credential.CoEmpresa });
        if (Empresa != null)
        {
            Credential.Empresa = new GlobalCredentialEmpresaModel();
            Credential.Empresa.NoEmpresa = Empresa.NoEmpresa;
            Credential.Empresa.NoEmpresaCorto = Empresa.NoEmpresaCorto;
            Credential.Empresa.NoArchivo = Empresa.NoArchivo;
            Credential.Empresa.NoExtension = Empresa.NoExtension;
        }

        Yo.SetCredential(Credential);
    }

    public async Task<GlobalCredentialModel> GetCredential(PermisoModel c)
    {
        //GlobalCredentialModel Credential = new GlobalCredentialModel();
        GlobalCredentialModel Credential = await HttpClientUtil.One<GlobalCredentialModel>(_client, "Cuenta/PermisoApi/GetByUser", c, Security.Anonymous);


        //if (Permiso != null)
        //{
        //    Credential.CoEmpresa = Permiso.CoEmpresa;
        //    Credential.CoPersona = Permiso.CoPersona;
        //    Credential.NoPersona = Permiso.NoPersona;
        //    Credential.NoApellidoP = Permiso.NoApellidoP;
        //    Credential.NoApellidoM = Permiso.NoApellidoM;
        //    Credential.NoArchivo = Permiso.NoArchivo;
        //    Credential.NoExtension = Permiso.NoExtension;

        //    Credential.Permiso = new GlobalCredentialPermisoModel();
        //    Credential.Permiso.CoPermiso = Permiso.CoPermiso;
        //    Credential.Permiso.NoUsuario = Permiso.NoUsuario;
        //    Credential.Permiso.NoClave = Permiso.NoClave;
        //}
        Yo.SetCredential(Credential);
        return Credential;
    }

    public async Task<GlobalCredentialModel> GetCredentialSystem()
    {
        GlobalCredentialModel Credential = await HttpClientUtil.One<GlobalCredentialModel>(_client, "Cuenta/PermisoApi/GetUserSystem", new PermisoModel(), Security.Anonymous);
        Yo.SetCredential(Credential);
        return Credential;
    }

    public async Task<SelectList> Empresa_GetAll()
    {
        var list = await HttpClientUtil.All<IEnumerable<EmpresaModel>>(_client, "EmpresaApi/GetAllActivo",
            new EmpresaModel(), Security.Anonymous);
        return new SelectList(list, "CoEmpresa", "NoEmpresa");
    }

    public async Task<SelectList> Modulo_GetAllSinArea()
    {
        var list = await HttpClientUtil.All<IEnumerable<ModuloModel>>(_client, "ModuloApi/GetAllActivoSinArea",
            new EmpresaModel());
        return new SelectList(list, "CoModulo", "NoModulo");
    }

    public async Task<IEnumerable<ModuloModel>> _Modulo_GetAll()
    {
        IEnumerable<ModuloModel> list = null;
        if (_cache.TryGetValue("_Modulo_GetAll", out list))
        {
        }

        else
        {
            list = await HttpClientUtil.All<IEnumerable<ModuloModel>>(_client, "ModuloApi/GetAllActivo",
                new ModuloModel());
        }

        return list;
    }

    public async Task<SelectList> Modulo_GetAll()
    {
        return new(await _Modulo_GetAll(), "CoModulo", "NoArea");
    }

    //public async Task<SelectList> Modulo_GetAll()
    //{
    //    IEnumerable<ModuloModel> list = await HttpClientUtil.All<IEnumerable<ModuloModel>>(_client, "ModuloApi/GetAllActivo", new ModuloModel());
    //    return new SelectList(list, "CoModulo", "NoArea");
    //}

    public async Task<SelectList> ModuloUsuario_GetAllModulo(ModuloUsuarioModel c, string CoPersona = null)
    {
        var list = await HttpClientUtil.All<IEnumerable<ModuloUsuarioModel>>(_client, "ModuloUsuarioApi/GetAllModulo",
            c);
        return new SelectList(list, "CoPersona", "NoNombreCompleto", CoPersona);
    }

    public async Task<SelectList> Perfil_GetAll(PerfilModel c)
    {
        var list = await HttpClientUtil.All<IEnumerable<ModuloUsuarioModel>>(_client, "PerfilApi/GetAllActivo", c);
        return new SelectList(list, "CoPerfil", "NoPerfil");
    }

    public async Task<SelectList> Permiso_GetAll(PermisoModel c)
    {
        var list = await HttpClientUtil.All<IEnumerable<ModuloUsuarioModel>>(_client, "PermisoApi/GetAllActivo", c);
        return new SelectList(list, "CoPersona", "NoNombreCompleto");
    }

    public async Task<SelectList> EmpresaModulo_GetAll(EmpresaModuloModel c, bool FlOnlyTypeMasterTable = false)
    {
        var list = (await HttpClientUtil.All<IEnumerable<ModuloUsuarioModel>>(_client, "EmpresaModuloApi/GetAllActivo",
            c)).ToList();
        if (FlOnlyTypeMasterTable) list = list.FindAll(x => x.FlOnlyTypeMasterTable == 0);

        return new SelectList(list, "CoModulo", "NoModulo");
    }
}