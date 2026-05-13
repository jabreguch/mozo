namespace Mozo.Model.Maestro;

public record TipoParticularDto : BaseFilterDto
{
    public int? CoGrupo { get; set; }
    public int? CoTipo { get; set; }
}
[Serializable]
public partial class TipoParticularModel : BaseModel //<TipoParticularModel>
{

    public TipoParticularModel() { }
    public int? CoGrupo { get; set; }
    public int? CoModulo { get; set; }
    public int? CoTipo { get; set; }
    public int? NuOrden { get; set; }
    public string? NoTipo { get; set; }
    public string? NoSigla { get; set; }
    public string? TxDescripcion { get; set; }
    public string? NoValor { get; set; }
    public int? CoGrupoPadre { get; set; }
    public int? CoTipoPadre { get; set; }
    public int? FlDefault { get; set; }
    public string? NoComando { get; set; }
}

public partial class TipoParticularModel
{

    public bool? FlDefault2 { get; set; }
    public string? NoGrupo { get; set; }
    public string? NoGrupoPadre { get; set; }
    public string? NoTipoPadre { get; set; }
    public string? NoModuloDescripcion { get; set; }
    public int? CoPersona { get; set; }
    public int? CoGrupoHijo { get; set; }
    public int? QtHijo { get; set; }

    public int? CoServicio { get; set; }
    public bool FlLoadChildren { get; set; }
    public bool FlSiglaView { get; set; }

    // public FilterModel Filter { get; set; }

    /*Sub TIpo*/
    public int? CoSubGrupo { get; set; }
    public int? CoSubTipo { get; set; }
    public string? NoSubValor { get; set; }
    public string? NoSubSigla { get; set; }
    public int? NuSubOrden { get; set; }
    public string? NoSubTipo { get; set; }

}

//