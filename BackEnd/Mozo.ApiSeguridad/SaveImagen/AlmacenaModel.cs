namespace Mozo.ApiSeguridad.SaveImagen
{
    public class AlmacenaModel
    {

        public int? CoEmpresa { get; set; }
        public int? CoModulo { get; set; }
        public int? CarpetaGuarda { get; set; }

        //[Description("Tipo entidad")] public int? CoTipoEntidad;

        //[Description("Formato archivo")] public int? CoTipoFormato;

        //[Description("Tipo de persona")] public int? CoTipoPersona;

        //[Description("Con vista preliminar de la imagen")] public int FlMultiple = 0;

        //[Description("Multiples archivos")] public int? FlPreview = 0;

        public int? Id { get; set; }

        public string? NoExtension { get; set; }

        public string? Prefijo { get; set; }

        //[Description("Titulo del formulario")] public string? TxTitulo;

        public string GetRuta(string contenedor)
        {
            return Path.Combine(contenedor, "Modulo" + CoModulo.ToString()!, "Empresa" + CoEmpresa.ToString()!);
        }


    }
}
