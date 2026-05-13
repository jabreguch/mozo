using Microsoft.AspNetCore.StaticFiles;

using Mozo.Helper.Global;

namespace Mozo.HelperWeb.Api;

public static class FileManage
{
    //public static string PathJsonFileSharedSettings(this IHostEnvironment env)
    //{
    //    //IHostEnvironment env = hostingContext.HostingEnvironment;
    //    //var sharedFolder = Path.Combine(env.ContentRootPath, "..\\..", "WebMozo");
    //    //config.AddJsonFile(Path.Combine(sharedFolder, "appsharedsettings.json"));
    //    var sharedFolder = Path.Combine(env.ContentRootPath, "..\\..");
    //    return Path.Combine(sharedFolder, "appsharedsettings.json");
    //}

    public static string TypeMime(this string NoExtencion)
    {
        switch (NoExtencion.Mayuscula())
        {
            case "3DM":
                return "x-world/x-3dmf";
            case "3DMF":
                return "x-world/x-3dmf";
            case "AFL":
                return "video/animaflex";
            case "AI":
                return "application/postscript";
            case "AIF":
                return "audio/aiff";
            case "AVI":
                return "video/avi";
            case "JPG":
                return "image/jpg";
            case "JPEG":
                return "image/jpeg";
            case "TIF":
                return "image/tiff";
            case "JS":
                return "application/javascript";
            case "BMP":
                return "image/bmp";
            case "PNG":
                return "image/png";
            case "GIF":
                return "image/gif";
            case "ICO":
                return "image/x-icon";
            case "CSS":
                return "text/css";
            case "PDF":
                return "application/pdf";
            case "JAVA":
                return "text/plain";
            case "TXT":
                return "text/plain";
            case "HTM":
                return "text/html";
            case "HTML":
                return "text/html";
            case "HTMLS":
                return "text/html";
            case "RTF":
                return "application/rtf";
            case "XLS":
                return "application/vnd.ms-excel";
            case "DBF":
                return "application/vnd.ms-excel";
            case "XLSX":
                return "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            case "DOC":
                return "application/msword";
            case "DOT":
                return "application/msword";
            case "DOCX":
                return "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
            case "DWG":
                return "application/acad";
            case "FLI":
                return "video/fli";
            case "PPT":
                return "application/vnd.ms-powerpoint";
            case "PPS":
                return "application/vnd.ms-powerpoint";
            case "PPTX":
                return "application/vnd.openxmlformats-officedocument.presentationml.presentation";
            case "PPSX":
                return "application/vnd.openxmlformats-officedocument.presentationml.presentation";
            //case "PPSX":
            //    return "application/vnd.openxmlformats-officedocument.presentationml.slideshow";
            case "MP3":
                return "audio/mpeg";
            case "MDB":
                return "application/vnd.ms-access";
            case "MDBX":
                return "application/vnd.ms-access";
            case "ZIP":
                return "application/zip";
            case "GZ":
                return "application/gzip";
            case "7Z":
                return "application/x-7z-compressed";
            case "JSON":
                return "application/json";
            case "RAR":
                return "application/vnd.rar";
            case "XML":
                return "application/xml";
            default:
                return "application/octet-stream";
        }
    }

    //public static string GetArchivoExtension(this string NoRutaCompleta)
    //{
    //    string NoArchivoExtension = null;
    //    if (NoRutaCompleta.NoNulo())
    //    {
    //        NoArchivoExtension = NoRutaCompleta.Substring(NoRutaCompleta.Length - 4, 4).Text();
    //        NoArchivoExtension = NoArchivoExtension.Replace(".", "").ToLower();
    //    }

    //    return NoArchivoExtension;
    //}

    //public static string GetArchivoNombre(this string NoRutaCompleta)
    //{
    //    string NoArchivo = null;
    //    if (NoRutaCompleta.NoNulo())
    //    {
    //        var NuInicio = NoRutaCompleta.LastIndexOf("\\") + 1;
    //        var NuFinal = NoRutaCompleta.LastIndexOf(".");
    //        NoArchivo = NoRutaCompleta.Substring(NuInicio, NuFinal - NuInicio).Text();

    //        NoArchivo = NoArchivo.RemoveDiacritics();
    //        NoArchivo = NoArchivo.RestringeASCII(255);
    //    }

    //    return NoArchivo;
    //}


    //public static HtmlString Anchor(this string url, string noExtension = null)
    //{
    //    //<a href="#" class="btn btn-primary"><span class="svg-icon svg-icon-1"><svg>...</svg></span>Caption</a>

    //    //<i class="fas fa-envelope-open-tex"></i>
    //    //<a href="#" class="btn btn-icon btn-primary"><i class="fas fa-envelope-open-text fs-4 me-2"></i></a>

    //    StringBuilder sb = new StringBuilder("<a");
    //    string icon = "fa-envelope-open-tex";
    //    if (noExtension.NoNulo())
    //    {
    //        icon = noExtension.Minuscula() switch
    //        {
    //            "jpg" => "fa-envelope-open-tex",
    //            "tif" => "fa-envelope-open-tex",
    //            "gif" => "fa-envelope-open-tex",

    //            "pdf" => "fa-envelope-open-tex",
    //            "doc" => "fa-envelope-open-tex",

    //            _ => "fa-envelope-open-tex" // default value
    //        };
    //    }
    //    sb.Append(" href=\""+ url + "\"");
    //    sb.Append(" target=\"_blank\"");

    //    sb.Append("<i class=\"fas fa-envelope-open-text fs-4 me-2\"> ");

    //    sb.Append("</a>");
    //    return new HtmlString(sb.ToString());
    //}

    //public static HtmlString GetUrlArchivoFileAnchor(this TipoSeleccionModel TipoSeleccion)
    //{
    //    var url = GetUrlArchivoFile(TipoSeleccion, null);

    //    var sb = new StringBuilder("<a");
    //    var icon = "bi bi-file-earmark-text";
    //    var extension = TipoSeleccion.NoExtension.Minuscula();
    //    if (extension.EsNulo()) return new HtmlString("");
    //    if (extension.NoNulo())
    //        icon = extension.Minuscula() switch
    //        {
    //            "jpg" => "bi bi-filetype-jpg",
    //            "tif" => "bi bi-filetype-tiff",
    //            "gif" => "bi bi-filetype-gif",

    //            "pdf" => "bi bi-file-pdf",
    //            "doc" => "fa fa-solid fa-file-word",
    //            "docx" => "fa fa-solid fa-file-word",

    //            _ => "bi bi-file-earmark-text" // default value
    //        };
    //    sb.Append(" href=\"" + url + "\"");
    //    sb.Append(" target=\"_blank\"");

    //    sb.Append(" title=\"Descargar archivo " + extension + "\"");

    //    sb.Append("<i class=\"" + icon + " fs-4 me-1\"></i>");

    //    sb.Append("</a>");
    //    return new HtmlString(sb.ToString());
    //}

    //public static string GetUrlArchivoFile(this TipoSeleccionModel TipoSeleccion)
    //{
    //    return GetUrlArchivoFile(TipoSeleccion, null);
    //}

    //public static string GetUrlArchivoFile(this TipoSeleccionModel TipoSeleccion, bool refresh = false)
    //{
    //    return GetUrlArchivoFile(TipoSeleccion, null, null, refresh);
    //}

    //public static string GetUrlArchivoFile(this TipoSeleccionModel TipoSeleccion, int? Width = null, int? Height = null,
    //    bool refresh = false)
    //{
    //    //if (!File.Exists(httpContext.Request.Path))
    //    //          {
    //    //              //"url(" + mzlocation.getPath() + "assets/media/svg/files/blank-image.svg)"
    //    //              httpContext.Request.Path= "/assets/media/svg/files/blank-image.svg";
    //    //          }

    //    if (TipoSeleccion == null) return null;

    //    var NoArchivo = TipoSeleccion.NoArchivo;

    //    if (NoArchivo.EsNulo()) return null;

    //    var NoExtension = TipoSeleccion.NoExtension;

    //    var CoModulo = TipoSeleccion.CoModulo.Text();
    //    var CoEmpresa = TipoSeleccion.CoEmpresa.Text();
    //    var CoTipoFormato = TipoSeleccion.CoTipoFormato.Text();


    //    var FolderEmpresa = string.Concat("Empresa", CoEmpresa);
    //    var FolderModulo = string.Concat("Modulo", CoModulo);
    //    var FolderTypeFile = string.Concat("Tipo", CoTipoFormato);

    //    string TxSize = null;
    //    if (Width != null || Height != null) TxSize = "?w=" + Width + "&h=" + Height;



    //    if (CoEmpresa != null)
    //    {
    //        return Url2.Get(new[] { EnuCommon.GLOBAL_NAME_FOLDER_DOCUMENT, FolderEmpresa, FolderModulo, FolderTypeFile, string.Concat(NoArchivo, ".", NoExtension, TxSize) }) + (refresh ? "?v=" + DateTime.Now.ToString("yyyyMMddHHmmss") : "");
    //        //if (!File.Exists(url))
    //        //{
    //        //    url = Url2.Get(new[] { "assets/media/svg/files/blank-image.svg" });

    //        //}
    //        //return url;
    //    }
    //    else
    //    {
    //        return Url2.Get(new[] { EnuCommon.GLOBAL_NAME_FOLDER_DOCUMENT, FolderModulo, FolderTypeFile, string.Concat(NoArchivo, ".", NoExtension, TxSize) }) + (refresh ? "?v=" + DateTime.Now.ToString("yyyyMMddHHmmss") : "");
    //        //if (!File.Exists(url))
    //        //{
    //        //    url = Url2.Get(new[] { "assets/media/svg/files/blank-image.svg" });

    //        //}
    //        //return url;
    //    }
    //}
    //public static string GetPathArchivoFile(this TipoSeleccionModel TipoSeleccion, IConfiguration configuration)
    //{
    //    var config = new ConfigurationModel();
    //    configuration.Bind(config);

    //    var NoArchivo = TipoSeleccion.NoArchivo;

    //    if (NoArchivo.EsNulo()) return null;

    //    var NoExtension = TipoSeleccion.NoExtension;

    //    var CoModulo = TipoSeleccion.CoModulo.Text();
    //    var CoEmpresa = TipoSeleccion.CoEmpresa.Text();
    //    var CoTipoFormato = TipoSeleccion.CoTipoFormato.Text();


    //    var FolderEmpresa = string.Concat("Empresa", CoEmpresa);
    //    var FolderModulo = string.Concat("Modulo", CoModulo);
    //    var FolderTypeFile = string.Concat("Tipo", CoTipoFormato);

    //    if (CoEmpresa != null)
    //        //return Url2.Get(new string[] { EnuCommon.GLOBAL_NAME_FOLDER_DOCUMENT, FolderEmpresa, FolderModulo, FolderTypeFile, string.Concat(NoArchivo, ".", NoExtension) });
    //        return config.AppSettings.FolderRoot + "\\" + EnuCommon.GLOBAL_NAME_FOLDER_DOCUMENT + "\\" + FolderEmpresa +
    //               "\\" + FolderModulo + "\\" + FolderTypeFile + "\\" + string.Concat(NoArchivo, ".", NoExtension);
    //    return config.AppSettings.FolderRoot + "\\" + EnuCommon.GLOBAL_NAME_FOLDER_DOCUMENT + "\\" + FolderModulo +
    //           "\\" + FolderTypeFile + "\\" + string.Concat(NoArchivo, ".", NoExtension);
    //}

    //    public static TipoSeleccionModel SetArchivoFile(this TipoSeleccionModel tipoSeleccion, IConfiguration configuration)
    //    {
    ////        Path.GetExtension
    //        var NoArchivo = tipoSeleccion.NoArchivo;
    //        var NoExtension = tipoSeleccion.NoExtension;

    //        var CoModulo = tipoSeleccion.CoModulo.Text();
    //        var CoEmpresa = tipoSeleccion.CoEmpresa.Text();
    //        var CoTipoFormato = tipoSeleccion.CoTipoFormato.Text();

    //        if (NoArchivo.EsNulo()) return tipoSeleccion;

    //        string TxDirectoryRoot = configuration.GetSection("AppSettings").GetSection("FolderRoot").Value;


    //        var TxDirectoryOrigen = Path.Combine(TxDirectoryRoot,
    //            configuration.GetSection("AppSettings").GetSection("FolderTemporary").Value,
    //            string.Concat(NoArchivo, ".", NoExtension));

    //        var TxDirectoryDestino = Path.Combine(TxDirectoryRoot,
    //            configuration.GetSection("AppSettings").GetSection("FolderDocument").Value);

    //        var FolderEmpresa = string.Concat("Empresa", CoEmpresa);
    //        var FolderModulo = string.Concat("Modulo", CoModulo);
    //        var FolderTypeFile = string.Concat("Tipo", CoTipoFormato);

    //        if (tipoSeleccion.CoEmpresa != null)
    //            TxDirectoryDestino = Path.Combine(TxDirectoryDestino, FolderEmpresa, FolderModulo, FolderTypeFile);
    //        else
    //            TxDirectoryDestino = Path.Combine(TxDirectoryDestino, FolderModulo, FolderTypeFile);


    //        if (!Directory.Exists(TxDirectoryDestino)) Directory.CreateDirectory(TxDirectoryDestino);

    //        var TxFileDestino = Path.Combine(TxDirectoryDestino, string.Concat(tipoSeleccion.Id, ".", NoExtension));
    //        if (File.Exists(TxFileDestino))
    //        {
    //            var TxDirectoryDelete = Path.Combine(TxDirectoryDestino, "Delete");
    //            if (!Directory.Exists(TxDirectoryDelete)) Directory.CreateDirectory(TxDirectoryDelete);

    //            var Now = DateTime.Now.ToString("yyyyMMddHhmmss");
    //            File.Move(TxFileDestino,
    //                Path.Combine(TxDirectoryDelete, string.Concat(tipoSeleccion.Id, "X", Now, ".", NoExtension)));
    //        }

    //        File.Copy(TxDirectoryOrigen, TxFileDestino);

    //        File.Delete(TxDirectoryOrigen);

    //        return tipoSeleccion;
    //    }

    //    public static T SetArchivoBlob<T>(this T obj, IConfiguration Configuration)
    //    {
    //        var t = obj.GetType();

    //        string NoArchivo = null;
    //        string NoExtension = null;
    //        var PpyNoArchivo = t.GetProperty("NoArchivo");
    //        if (PpyNoArchivo.GetValue(obj, null).NoNulo()) NoArchivo = PpyNoArchivo.GetValue(obj, null).ToString();

    //        var PpyNoExtension = t.GetProperty("NoExtension");
    //        if (PpyNoExtension.GetValue(obj, null).NoNulo()) NoExtension = PpyNoExtension.GetValue(obj, null).ToString();

    //        //PropertyInfo PryENVIARA = t.GetProperty("ENVIARA");

    //        //IPathProvider pathProvider;

    //        //string RutaCompleta = pathProvider.MapPath("Sample.PNG");
    //        //Uri(Yo.Configuration.GetSection("WebService").GetSection("Seguridad").Value);

    //        var TxDirectoryRoot = Configuration.GetSection("AppSettings").GetSection("FolderRoot").Value;
    //        var RutaCompleta = Path.Combine(TxDirectoryRoot,
    //            Configuration.GetSection("AppSettings").GetSection("FolderTemporary").Value,
    //            string.Concat(NoArchivo, ".", NoExtension));

    //        //string RutaCompleta = Path.Combine(Configuration.GetSection("AppSettings").GetSection("PathTemporary").Value, string.Concat(NoArchivo, ".", NoExtension));
    //        var fs = new FileStream(RutaCompleta, FileMode.Open, FileAccess.Read);
    //        var filesize = fs.Length;
    //        var BlArchivo = new byte[Convert.ToInt32(fs.Length - 1) + 1];
    //        fs.Read(BlArchivo, 0, Convert.ToInt32(filesize));
    //        fs.Close();
    //        fs.Close();
    //        fs = null;

    //        var PpyBlArchivo = t.GetProperty("BlArchivo");
    //        PpyBlArchivo.SetValue(obj, BlArchivo, null);

    //        //File.Delete(TXARCHIVORUTACOMPLETA.GetPath());

    //        return obj;
    //    }


    //    public static T SetArchivoNombreExtension<T>(this T obj, string rutaCompleta = null, bool prefijo = true)
    //    {
    //        var t = obj.GetType();

    //        PropertyInfo PpyNoRutaCompleta;
    //        string RutaCompleta = null;

    //        //if (t.GetTypeInfo().FullName == "System.String")
    //        if (rutaCompleta.NoNulo())
    //        {
    //            RutaCompleta = rutaCompleta;
    //        }
    //        else
    //        {
    //            PpyNoRutaCompleta = t.GetProperty("NoRutaCompleta");
    //            if (PpyNoRutaCompleta.GetValue(obj, null).NoNulo())
    //                RutaCompleta = PpyNoRutaCompleta.GetValue(obj, null).ToString();
    //        }

    //        if (RutaCompleta.NoNulo())
    //        {
    //            var NoArchivo = RutaCompleta.GetArchivoNombre();
    //            var NoExtension = RutaCompleta.GetArchivoExtension();

    //            var PpyNoExtension = t.GetProperty("NoExtension");
    //            PpyNoExtension.SetValue(obj, NoExtension, null);

    //            var PpyNoArchivo = t.GetProperty("NoArchivo");
    //            PpyNoArchivo.SetValue(obj, NoArchivo, null);

    //            if (prefijo)
    //            {
    //                var PpyNoPrefijoArchivo = t.GetProperty("NoPrefijoArchivo");
    //                if (PpyNoPrefijoArchivo != null) PpyNoPrefijoArchivo.SetValue(obj, Glo.RandomString(10), null);
    //            }
    //        }

    //        return obj;
    //    }


    public static string GetMimeTypeFile(this string fileName)
    {
        string contentType;
        new FileExtensionContentTypeProvider().TryGetContentType(fileName, out contentType);
        return contentType ?? "application/octet-stream";


        //string mimeType = "application/unknown";
        //string ext = Path.GetExtension(fileName).ToLower();
        //Microsoft.Win32.RegistryKey regKey = Microsoft.Win32.Registry.ClassesRoot.OpenSubKey(ext);
        //if (regKey != null && regKey.GetValue("Content Type") != null)
        //    mimeType = regKey.GetValue("Content Type").ToString();
        //return mimeType;
    }

    //GetUrlArchivoFile
    //public static string GetImagenPageWeb(this EmpresaModel Empresa, string key, string urlDefault = null)
    //{
    //    if (Empresa == null) return urlDefault;
    //    if (Empresa.ImagenPageWebLst == null) return urlDefault;
    //    if (Empresa.ImagenPageWebLst.Count == 0) return urlDefault;
    //    string Url = null;
    //    var Imagen = Empresa.ImagenPageWebLst.Find(x => x.NoImagenWeb == key);
    //    if (Imagen != null)
    //    {
    //        Url = EnuTipoGeneral.FormatoArchivo.Empresa.ImagenPageWeb.With(x =>
    //        {
    //            x.CoEmpresa = Imagen.CoEmpresa;
    //            x.NoArchivo = Imagen.NoArchivo;
    //            x.NoExtension = Imagen.NoExtension;
    //        }).GetUrlArchivoFile();

    //        if (Url == null) Url = urlDefault;
    //    }

    //    return Url;
    //}
}