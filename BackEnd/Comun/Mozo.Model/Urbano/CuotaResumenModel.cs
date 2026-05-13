///<summary>
///
///</summary>
///<remarks>
///</remarks>
///<history>
/// t[Jonatan Abregu]	16/11/2021	Created
///</history>
namespace Mozo.Model.Urbano;

public record CuotaResumenFilter : BaseFilterDto { }
[Serializable]
public class CuotaResumenModel : BaseModel //<CuotaResumenModel>
{
    public decimal? SsCuotaPago { get; set; }
    public decimal? SsInteresPago { get; set; }
    public decimal? SsAmortizacionPago { get; set; }
    public decimal? SsAbonoPago { get; set; }
    public decimal? SsMoraPago { get; set; }
    public int? FlPago { get; set; }
    public int? FlVencio { get; set; }
    public string? FePago { get; set; }
    public int? QtDiaVencimiento { get; set; }
}
