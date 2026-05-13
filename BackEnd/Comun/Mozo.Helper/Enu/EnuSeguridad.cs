namespace Mozo.Helper.Enu;

public static class PaginaProperty
{
    //public static string NoTipoPagina(this int? CoTipoPagina)
    //{
    //    if (CoTipoPagina == 1)
    //        return "Menu";
    //    if (CoTipoPagina == 2)
    //        return "Sub menú";
    //    if (CoTipoPagina == 3)
    //        return "Página flotante";
    //    if (CoTipoPagina == 4)
    //        return "Servicio Web";
    //    return "";
    //}
    //public static string GetIcono(this int? CoTipoPagina)
    //{

    //    if (CoTipoPagina == 1 || CoTipoPagina == 2)
    //    {
    //        return "fl flaticon-menu-2";

    //    }
    //    else if (CoTipoPagina == 3)
    //    {
    //        return "fa fa-external-link-alt";
    //    }
    //    else if (CoTipoPagina == 4)
    //    {
    //        return "fa fa-cloud";
    //    }
    //    else
    //    {
    //        return "";
    //    }

    //}

    //public static string GetText(this int? CoTipoPagina, string NoOpcion, string NoArea, string NoControlador,
    //    string NoAccion)
    //{
    //    if (CoTipoPagina == 1 || CoTipoPagina == 2)
    //        return NoOpcion;
    //    if (CoTipoPagina == 3)
    //        return string.Concat(NoArea, " / ", NoControlador, "/", NoAccion);
    //    if (CoTipoPagina == 4)
    //        return string.Concat(NoArea, " / ", NoControlador, "/", NoAccion);
    //    return "";
    //}
}

public static class EnuSeguridad
{
    //public static class CoTabPermiso
    //{
    //    public const int Persona = 1;
    //    public const int Permiso = 2;
    //}

    //public static class TipoPagina
    //{
    //    public const int Menu = 0;
    //    public const int Pagina = 1;
    //    public const int SubPagina = 2;
    //    public const int PaginaFlotante = 3;
    //    public const int ServicioWeb = 4;
    //}

    //public interface IModulo
    //{
    //    int CoModulo { get; }
    //    string Api { get; }
    //    string Web { get; }
    //}

    //public static class ModuloModel
    //{
    //    public static int CoModulo { get; set; }
    //    public static string Api { get; set; }
    //    public static string Web { get; set; }
    //}

    public class ModuloModel2
    {
        public int CoModulo { get; set; }
        public string? Api { get; set; }
        public string? Web { get; set; }
    }

    public static class Modulo
    {
        private static readonly Func<int, string> GetApi = CoModulo => "api" + CoModulo;
        private static readonly Func<int, string> GetWeb = CoModulo => "web" + CoModulo;

        public static ModuloModel2 Maestro => GetModulo(-2);

        public static ModuloModel2 Seguridad => GetModulo(-1);

        public static ModuloModel2 Empresa => GetModulo(1);

        public static ModuloModel2 Condominio => GetModulo(2);

        public static ModuloModel2 FinanzaPersonal => GetModulo(3);

        public static ModuloModel2 Biblioteca => GetModulo(4);

        public static ModuloModel2 Soporte => GetModulo(5);

        public static ModuloModel2 MaestroUser => GetModulo(6);

        public static ModuloModel2 Contabilidad => GetModulo(7);

        public static ModuloModel2 Expediente => GetModulo(8);

        public static ModuloModel2 Facturacion => GetModulo(9);

        public static ModuloModel2 Matricula => GetModulo(11);

        public static ModuloModel2 Inventario => GetModulo(12);

        public static ModuloModel2 Credito => GetModulo(13);

        public static ModuloModel2 Urbano => GetModulo(15);

        public static ModuloModel2 Rrhh => GetModulo(16);
        public static ModuloModel2 Catalogo => GetModulo(17);
        private static ModuloModel2 GetModulo(int CoModulo)
        {
            ModuloModel2 Modulo = new()
            {
                CoModulo = CoModulo,
                Api = GetApi(CoModulo),
                Web = GetWeb(CoModulo)
            };
            return Modulo;
        }
    }
}