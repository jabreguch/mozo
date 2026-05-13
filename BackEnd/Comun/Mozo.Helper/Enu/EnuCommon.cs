namespace Mozo.Helper.Enu;

public static class EnuCommon
{
    public static class MsgValiadation
    {
        public const string? Required = "Ingresar el campo.";

    }

    public static class BdDefault
    {
        public static string Name = "Logistica";
        public static class Schema
        {
            #region Seguridad           
            public static string Maestro = "maestro";
            public static string MaestroRpt = "maestrosrpt";
            public static string Seguridad = "seguridad";
            public static string Login = "login";
            #endregion

            public static string Catalogo = "catalogo";

            public static string Urbano = "urbano";


            public static string Empresa = "empresa";

            public static string Contabilidad = "contabilidad";
            public static string Facturacion = "facturacion";
            public static string FacturacionRpt = "facturacionrpt";

            public static string Inventario = "inventario";
            public static string Rrhh = "rrhh";
        }
    }
    //public static class UserSystem
    //{
    //    public const int CoEmpresa = -1;
    //    public const int CoPersona = -1;
    //    public const string NoUsuario = "system";
    //    public const string NoClave = "9d29a69a-b2ab-499c-bd2c-2ac075d92a5c";
    //}




    ////"dd/MM/yyyy HH:mm:tt";
    ////"dd/MM/yyyy hh:mm:ss tt" site.js
    //public static readonly string formatoFecha = "dd/MM/yyyy HH:mm:ss";

    ////public static readonly string formatoFechaBD = "dd/MM/yyyy";
    //public static readonly string formatoFechaHTML5 = "yyyy-MM-dd";
    //// EXITO = 1, INFORMACION = 2, ADVERTENCIA = 3, PELIGRO = 4

    //public static readonly string menRegistroModificacion = "Se registró las modificaciones";

    //public static readonly string menExito = "Se realizo los cambios correctamente";
    //public static readonly string menInformacion = "Se esta procesando la información";
    //public static readonly string menAdvertencia = "Tenga precaución al registrar";
    //public static readonly string menPeligro = "Existe errores";


    //public static string msgExito(string msg = null)
    //{
    //    return new MensajeModel() { NoMensaje = msg, CoTipoMensaje = CoTipoMensaje.Exito }.Serializa();
    //}

    //public static string msgInformacion(string msg = null)
    //{
    //    return new MensajeModel() { NoMensaje = msg, CoTipoMensaje = CoTipoMensaje.Informacion }.Serializa();
    //}
    //public static string msgAdvertencia(string msg = null)
    //{
    //    return new MensajeModel() { NoMensaje = msg, CoTipoMensaje = CoTipoMensaje.Advertencia }.Serializa();
    //}

    //public static string msgPeligro(string msg = null)
    //{
    //    return new MensajeModel() { NoMensaje = msg, CoTipoMensaje = CoTipoMensaje.Peligro }.Serializa();
    //}

    //public static readonly string menMuestraRegistro = "Se visualiza el registro";
    //public static readonly string menNoMuestraRegistro = "No se encontro el registro";
    //public static readonly string menMuestranRegistros = "Se vizualizan los registros";
    //public static readonly string menRegistroElimino = "Se eliminó el registro";

    //public static readonly string menSeleccionHtmlSelect = "Seleccionar..";

    //  public static readonly string RutaVistaMensaje = "~/_Mensaje";//Ruta de la vista del mensaje al usuario


    //private static readonly string _RutaCarpetaTemporal = "Temporal";


    //public static string RutaCarpetaTemporal2(bool FlWeb = false)
    //{
    //    if (FlWeb == true)
    //    {
    //        return string.Concat("~/", _RutaCarpetaTemporal);
    //    }
    //    else
    //    {
    //        string NoRuta = Directory.GetCurrentDirectory();
    //        NoRuta = string.Concat(NoRuta, "\\", _RutaCarpetaTemporal, "\\");
    //        //NoRuta = NoRuta.Replace("\\", "/");
    //        return NoRuta;
    //    }
    //}

    // public const int PageSize = 10;

    public static List<int> RandomListUnica = new();



    public static class Modal
    {
        public static class Size
        {
            public const int Small = 1;
            public const int Default = 2;
            public const int Large = 3;
            public const int ExtraLarge = 4;
            public const int Fullscreen = 5;


        }

    }

    public static class Toast
    {
        public static class Type
        {
            public const int Info = 1;
            public const int Success = 2;
            public const int Danger = 3;
            public const int Warning = 4;
            public const int Save = 5;
            public const int UpdateState = 6;
            public const int Delete = 7;
        }

        public static class Action
        {
            public const int Save = 1;
            public const int UpdateState = 2;
            public const int Delete = 3;
        }
    }


    public class Button
    {
        public static class Size
        {
            public const int Lg = 1;
            public const int Standart = 2;
            public const int Small = 3;

        }
        public static class Tipo
        {
            public const int Button = 1;
            public const int Submit = 2;
            public const int Reset = 3;
        }
        public static class Theme
        {
            public const int New = 1;
            public const int Save = 2;
            public const int Delete = 3;
            public const int Search = 4;
            public const int Report = 5;
            public const int GoOut = 6;
            public const int Refresh = 7;
            public const int Execute = 8;
            public const int Edit = 9;
            public const int Attach = 10;

            public const int More = 11;
            public const int Less = 12;

            public const int Export = 13; //Sin Icono

            public const int Transfer = 14;

            public const int Select = 15;

            public const int Check = 16;
            //public const int Activo = 17;
            //public const int Inactivo = 18;

            public const int Separator = 19;

            public const int Link = 20;
            public static class _Link
            {
                public const int None = 1;
                public const int Blank = 2;

            }

            //public const int LinkSelf = 20;
            //public const int LinkBlank = 21;

            public const int Question = 22;

            public const int ToggleOff = 23;
            public const int ToggleOn = 24;

            public const int Filter = 25;
            public const int Reset = 26;

            public const int Back = 27;
            public const int Accept = 28;


        }
    }


    public static class FormatoFecha
    {
        public const int SoloFecha = 1;
        public const int SoloHora = 2;
        public const int FechaYHora = 3;
        public const int FechaNombre = 4;
        public const int FechaYHoraNombre = 5;
        public const int MesNombre = 6;
        public const int DiaNombre = 7;
        public const int DiaNumero = 8;
        public const int AditionHora = 9;

        public const int MesAnio = 10;
        //public const int FechaYHora24 = 9;
    }


    //public static class EstadoPagina
    //{
    //    public const int None = 0;
    //    public const int Nuevo = 1;
    //    public const int Edicion = 2;
    //    public const int Preliminar = 3;
    //    public const int Insert = 4;
    //    public const int FirstNew = 5;

    //    public const int Delete = 6;
    //    //public const int Delete = 7;
    //}
    public static class RecordStatus
    {
        public const int Load = 1;
        public const int New = 2;
        public const int Edit = 3;
        public const int Delete = 4;
    }


    public static class CoCarpetaGuarda
    {
        public const int FolderImage = 1;
        public const int FolderDocument = 2;
        public const int FolderTemporary = 3;

        //       "FolderImage": "G:\\Desarrollo\\Comercio\\WwwRoot\\Image",
        //"FolderDocument": "G:\\Desarrollo\\Comercio\\WwwRoot\\Document",
        //"FolderResource": "G:\\Desarrollo\\Comercio\\WwwRoot\\Resource", 
        //"FolderTemporary": "G:\\Desarrollo\\Comercio\\WwwRoot\\Temporary",
    }
}