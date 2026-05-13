namespace Mozo.Model.Maestro;

public record TipoGeneralFilterDto : BaseFilterDto
{
    public int? CoGrupo { get; set; }
    public int? CoModulo { get; set; }
    public int? CoTipo { get; set; }

}
[Serializable]
public partial class TipoGeneralModel : BaseModel //<TipoGeneralModel>
{

    public string? TxDescripcion { get; set; }
    public int? CoTipo { get; set; }
    public int? CoModulo { get; set; }
    public string? NoTipo { get; set; }
    public int? CoGrupo { get; set; }
    public string? NoSigla { get; set; }
    public int? FlDefault { get; set; }
    public int? NuOrden { get; set; }
    public string? NoComando { get; set; }
    public string? Valor { get; set; }

}
public partial class TipoGeneralModel
{



}