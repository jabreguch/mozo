
using Microsoft.AspNetCore.StaticFiles;
using Mozo.Comun.Helper.Enu;
using System;
using System.IO;
using System.Reflection;


namespace Mozo.Comun.Helper.Global
{
    public static partial class Glo
    {
        //public static class WebConfig
        //{

        //    public static string CadenaConexion()
        //    {
        //        if (Glo.CacheGet("CnMozo") == null)
        //        {
        //            string NoVariable = Cripto.Encripta(GetWebConfig().GetSection("ConnectionStrings").GetSection("CnMozo").Value, Parametro.Key());
        //            Glo.CacheSet("CnMozo", NoVariable);
        //            return NoVariable;
        //        }
        //        else
        //        {
        //            return Glo.CacheGet("CnMozo");
        //        }
        //    }


        //    public static class Jwt
        //    {

        //        public static string Key()
        //        {
        //            if (Glo.CacheGet("JwtKey") == null)
        //            {
        //                string NoVariable = GetWebConfig().GetSection("Jwt").GetSection("Key").Value;
        //                Glo.CacheSet("JwtKey", NoVariable);
        //                return NoVariable;
        //            }
        //            else
        //            {
        //                return Glo.CacheGet("JwtKey");
        //            }
        //        }
        //        public static string Audience()
        //        {
        //            if (Glo.CacheGet("JwtAudience") == null)
        //            {
        //                string NoVariable = GetWebConfig().GetSection("Jwt").GetSection("Audience").Value;
        //                Glo.CacheSet("JwtAudience", NoVariable);
        //                return NoVariable;
        //            }
        //            else
        //            {
        //                return Glo.CacheGet("JwtAudience");
        //            }
        //        }
        //        public static int Duracion()
        //        {
        //            if (Glo.CacheGet("JwtDuracion") == null)
        //            {
        //                string NoVariable = GetWebConfig().GetSection("Jwt").GetSection("Duracion").Value;
        //                Glo.CacheSet("JwtDuracion", NoVariable);
        //                return NoVariable.Num();
        //            }
        //            else
        //            {
        //                return Glo.CacheGet("JwtDuracion").Num();
        //            }
        //        }


        //    }


        //    public static class Parametro
        //    {

        //        public static string Key()
        //        {
        //            if (Glo.CacheGet("ParametroKey") == null)
        //            {
        //                string NoVariable = GetWebConfig().GetSection("Parametro").GetSection("Key").Value;
        //                Glo.CacheSet("ParametroKey", NoVariable);
        //                return NoVariable;
        //            }
        //            else
        //            {
        //                return Glo.CacheGet("ParametroKey");
        //            }
        //        }



        //    }

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

        public static string GetArchivoExtension(this string NoRutaCompleta)
        {
            string NoArchivoExtension = null;
            if (NoRutaCompleta.NoNulo())
            {
                NoArchivoExtension = NoRutaCompleta.ToString().Substring(NoRutaCompleta.ToString().Length - 4, 4).Text();
                NoArchivoExtension = NoArchivoExtension.Replace(".", "").ToLower();
            }
            return NoArchivoExtension;
        }

        public static string GetArchivoNombre(this string NoRutaCompleta)
        {
            string NoArchivo = null;
            if (NoRutaCompleta.NoNulo())
            {
                int NuInicio = NoRutaCompleta.ToString().LastIndexOf("\\") + 1;
                int NuFinal = NoRutaCompleta.ToString().LastIndexOf(".");
                NoArchivo = NoRutaCompleta.Substring(NuInicio, NuFinal - NuInicio).Text();

                NoArchivo = NoArchivo.RemoveDiacritics();
                NoArchivo = NoArchivo.RestringeASCII(255);

            }

            return NoArchivo;
        }


        public static T SetArchivoBlob<T>(this T obj)
        {
            Type t = obj.GetType();

            string NoArchivo = null;
            string NoExtension = null;
            PropertyInfo PpyNoArchivo = t.GetProperty("NoArchivo");
            if (PpyNoArchivo.GetValue(obj, null).NoNulo())
            {
                NoArchivo = PpyNoArchivo.GetValue(obj, null).ToString();
            }

            PropertyInfo PpyNoExtension = t.GetProperty("NoExtension");
            if (PpyNoExtension.GetValue(obj, null).NoNulo())
            {
                NoExtension = PpyNoExtension.GetValue(obj, null).ToString();
            }

            //PropertyInfo PryENVIARA = t.GetProperty("ENVIARA");

            //IPathProvider pathProvider;

            //string RutaCompleta = pathProvider.MapPath("Sample.PNG");
                    
            string RutaCompleta = Path.Combine(EnuCommon.RutaCarpetaTemporal(), string.Concat(NoArchivo, ".", NoExtension));
            FileStream fs = new FileStream(RutaCompleta, FileMode.Open, FileAccess.Read);
            long filesize = fs.Length;
            byte[] BlArchivo = new byte[Convert.ToInt32(fs.Length - 1) + 1];
            fs.Read(BlArchivo, 0, Convert.ToInt32(filesize));
            fs.Close();
            fs.Close(); fs = null;

            PropertyInfo PpyBlArchivo = t.GetProperty("BlArchivo");
            PpyBlArchivo.SetValue(obj, BlArchivo, null);

            //File.Delete(TXARCHIVORUTACOMPLETA.GetPath());

            return obj;
        }


        //public static T SetArchivoNombreExtension<T>(this T obj, string NoRutaCompleta = null)
        //{
        //    Type t = obj.GetType();

        //    PropertyInfo PpyNoRutaCompleta;
        //    string _NoRutaCompleta = null;

        //    //if (t.GetTypeInfo().FullName == "System.String")
        //    if (NoRutaCompleta.NoNulo())
        //    {
        //        _NoRutaCompleta = NoRutaCompleta;
        //    }
        //    else
        //    {
        //        PpyNoRutaCompleta = t.GetProperty("NoRutaCompleta");
        //        if (PpyNoRutaCompleta.GetValue(obj, null).NoNulo())
        //        {
        //            _NoRutaCompleta = PpyNoRutaCompleta.GetValue(obj, null).ToString();
        //        }
        //    }

        //    if (_NoRutaCompleta.NoNulo())
        //    {
        //        string NoArchivo = _NoRutaCompleta.GetArchivoNombre();
        //        string NoExtension = _NoRutaCompleta.GetArchivoExtension();

        //        PropertyInfo PpyNoExtension = t.GetProperty("NoExtension");
        //        PpyNoExtension.SetValue(obj, NoExtension, null);

        //        PropertyInfo PpyNoArchivo = t.GetProperty("NoArchivo");
        //        PpyNoArchivo.SetValue(obj, NoArchivo, null);

        //    }

        //    return obj;
        //}


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


    }
}