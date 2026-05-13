using System.ComponentModel;

namespace Mozo.Helper.Enu;

[Description("Seleccion grupos de soporte")]
public class GrupoSeleccionModel
{
    [Description("Grupo")] public int CoGrupo;

    [Description("Titulo del Grupo")] public string? TxTitulo;
}

public static class EnuSoporte
{
    public static class Grupo
    {
        public static GrupoSeleccionModel TIEscritorio = new() { CoGrupo = 1, TxTitulo = "Escritorio TI" };
        public static GrupoSeleccionModel TIDesarrollo = new() { CoGrupo = 2, TxTitulo = "Desarrollo de sistemas TI" };
        public static GrupoSeleccionModel Abogado = new() { CoGrupo = 4, TxTitulo = "Abogado" };
        public static GrupoSeleccionModel Estadistico = new() { CoGrupo = 3, TxTitulo = "Estadisticas" };
    }
}