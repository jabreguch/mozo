///<summary>
///
///</summary>
///<remarks>
///</remarks>
///<history>
/// t[Jonatan Abregu]	16/11/2021	Created
///</history>
namespace Mozo.Model.Urbano;

public record CreditoResumenFilter : BaseFilterDto { }
[Serializable]
public partial class CreditoResumenModel : BaseModel //<CreditoResumenModel>
{
    public decimal? SsCuotaPago { get; set; }
    public decimal? SsInteresPago { get; set; }
    public decimal? SsAmortizacionPago { get; set; }
    public decimal? SsAbonoPago { get; set; }
    public decimal? SsMoraPago { get; set; }
    public int? QtCuotaPago { get; set; }
    public int? QtCuotaVencido { get; set; }

}
