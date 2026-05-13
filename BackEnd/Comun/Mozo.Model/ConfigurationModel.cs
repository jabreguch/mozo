using static Mozo.Helper.Global.Glo;

namespace Mozo.Model;

public class ConfigurationModel
{
    //public static string GetUrlWebApi(string NameWebApi, ConfigurationModel config)
    //{
    //    config.Server.Api.Maestro.CheckSlashEnd();

    //    string urlServer = GetUrlServer(config);         
    //    string urlWebApi = config.Server.Api.GetType().GetProperty(NameWebApi).GetValue(config.Server.Api).ToString().CheckSlashEnd();// ").GetSection(NameWebApi).Value.CheckSlashEnd();
    //    return string.Concat(urlServer, urlWebApi);
    //}


    public int CoEmpresa { get; set; }
    public int CoModulo { get; set; }
    public SettingsModel? AppSettings { get; set; }
    public ServerModel? Server { get; set; }

    public EmailSenderModel? EmailSender { get; set; }


    //public static string UrlServer(IMemoryCache cache)
    //{
    //    string NoUrlBase = null;
    //    //if (cache == null) cache = CacheMemory;

    //    if (cache.TryGetValue("HttpConnect_UrlServer", out NoUrlBase)) { }
    //    else
    //    {
    //        bool IsHttps = bool.Parse(Configuration.GetSection("Server").GetSection("IsHttps").Value);
    //        NoUrlBase = string.Concat(IsHttps == true ? "https://" : "http://", Configuration.GetSection("Server").GetSection("Name").Value.CheckSlashEnd());
    //        cache.Set("HttpConnect_UrlServer", NoUrlBase);
    //    }
    //    return NoUrlBase;
    //}

    public static string GetUrlServer(ConfigurationModel config)
    {
        return string.Concat(config.Server.IsHttps ? "https://" : "http://", config.Server.Name.CheckSlashEnd());
    }
    // {


    //if (cache.TryGetValue("HttpConnect_UrlServer", out NoUrlBase)) { }
    //else
    //{
    //    bool IsHttps = bool.Parse(Configuration.GetSection("Server").GetSection("IsHttps").Value);
    //    NoUrlBase = string.Concat(IsHttps == true ? "https://" : "http://", Configuration.GetSection("Server").GetSection("Name").Value.CheckSlashEnd());
    //    cache.Set("HttpConnect_UrlServer", NoUrlBase);
    //}
    //  return string.Concat(config.Server.IsHttps == true ? "https://" : "http://", config.Server.Name.CheckSlashEnd());           
    // }

    public static string GetUrlWebApi(string NameWebApi, ConfigurationModel config)
    {
        // config.Server.Api.Maestro.CheckSlashEnd();

        var urlServer = GetUrlServer(config);
        var urlWebApi = config.Server.Api.GetType().GetProperty(NameWebApi).GetValue(config.Server.Api).ToString()
            .CheckSlashEnd(); // ").GetSection(NameWebApi).Value.CheckSlashEnd();
        return string.Concat(urlServer, urlWebApi);
    }
}

public class SettingsModel
{
    public string FolderRoot { get; set; }
    public string FolderTemporary { get; set; }
    public string FolderDocument { get; set; }
    public string FolderResource { get; set; }
    public int? CoEmpresaPrincipal { get; set; }
}

public class ServerModel
{
    public string Name { get; set; }
    public bool IsHttps { get; set; }
    public ApiModel Api { get; set; }
}

public class ApiModel
{
    public string Seguridad { get; set; }
    public string Maestro { get; set; }
    public string Empresa { get; set; }
    public string Condominio { get; set; }
    public string Biblioteca { get; set; }
    public string Soporte { get; set; }
    public string Contabilidad { get; set; }
    public string Expediente { get; set; }

    public string Credito { get; set; }

    public string Matricula { get; set; }

    public string Facturacion { get; set; }

    public string Urbano { get; set; }


    public string Inventario { get; set; }

}

public class EmailSenderModel
{
    public int Port { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public bool EnableSsl { get; set; }
    public string Host { get; set; }
}