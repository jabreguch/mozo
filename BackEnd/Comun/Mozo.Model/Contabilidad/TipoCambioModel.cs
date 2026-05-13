///<summary>
///
///</summary>
///<remarks>
///</remarks>
///<history>
/// t[Jonatan Abregu]	15/10/2021	Created
///</history>
namespace Mozo.Model.Contabilidad;

public record TipoCambioFilter : BaseFilterDto { }

[Serializable]
public partial class TipoCambioModel : BaseModel //<TipoCambioModel>
{
    public int? CoTipoCambio { get; set; }
    public int? CoFuente { get; set; }
    public int? CoMonedaOrigen { get; set; }
    public int? CoMonedaDestino { get; set; }
    public string? FeTipoCambio { get; set; }
    public int? FlAutomatico { get; set; }
    public decimal? SsCompra { get; set; }
    public decimal? SsVenta { get; set; }
}
public partial class TipoCambioModel
{
    public string? FeTipoCambioMes { get; set; }
    public string? NoMoneda { get; set; }
    public string? NoMonedaSigla { get; set; }
}