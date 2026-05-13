///<summary>
///
///</summary>
///<remarks>
///</remarks>
///<history>
/// t[Jonatan Abregu]	10-04-2020	Created
///</history>
namespace Mozo.Model.Condominio;

public record PeriodoFilter : BaseFilterDto //PeriodoFilter>
{

}

[Serializable]
public class PeriodoModel : BaseModel //<PeriodoModel>
{
    public int? CoPeriodo { get; set; }
    public int? CoCondominio { get; set; }
    public string? NoCondominio { get; set; }
    public int? CoPredio { get; set; }
    public int? QtPredio { get; set; }
    public int? CoEdificio { get; set; }
    public int? CoTipoServicio { get; set; }
    public int? NuOrden { get; set; }
    public int? NuAnno { get; set; }
    public string? FeInicial { get; set; }
    public string? FeFinal { get; set; }
    public string? NoEdificio { get; set; }
    public string? NoTipoPredio { get; set; }
    public List<ConsumoModel>? ConsumoCol { get; set; }
    public List<PredioModel>? PredioCol { get; set; }
    public int? QtConsumo { get; set; }
    public int? CoTipoPredio { get; set; }
    public string? NoPeriodo { get; set; }
    public string? TxDescripcion { get; set; }
}