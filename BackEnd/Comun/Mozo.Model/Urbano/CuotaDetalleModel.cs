///<summary>
///
///</summary>
///<remarks>
///</remarks>
///<history>
/// t[Jonatan Abregu]	05/02/2022	Created
///</history>
namespace Mozo.Model.Urbano;

public record CuotaDetalleFilter : BaseFilterDto { }
[Serializable]
public partial class CuotaDetalleModel : BaseModel //<CuotaDetalleModel>
{
    public int? CoProyecto { get; set; }

    public int? CoCredito { get; set; }

    public int? CoCuota { get; set; }

    public int? CoCuotaDetalle { get; set; }

    public int? CoTipoCuotaDetalle { get; set; }

    public int? CoDocPagoCuotaDetalle { get; set; }

    public decimal? SsCuotaDetallePago { get; set; }

    public decimal? SsMora { get; set; }
    public decimal? SsSaldoCapital { get; set; }
}

public partial class CuotaDetalleModel
{
    #region Property Query Fields

    public DocPagoModel? DocCuotaDetalle { get; set; }

    #endregion
    #region Property Query Fields
    public string? NoTipoCuotaDetalle { get; set; }
    public int? QtCuotaDetalle { get; set; }
    public int? QtAbonoDetalle { get; set; }

    #endregion






}