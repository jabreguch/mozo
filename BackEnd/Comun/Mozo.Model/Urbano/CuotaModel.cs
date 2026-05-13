using Mozo.Model.Maestro;

///<summary>
///
///</summary>
///<remarks>
///</remarks>
///<history>
/// t[Jonatan Abregu]	16/11/2021	Created
///</history>
namespace Mozo.Model.Urbano;

public record CuotaFilter : BaseFilterDto { }
[Serializable]
public partial class CuotaModel : BaseModel //<CuotaModel>
{
    public int? CoProyecto { get; set; }
    public int? CoCredito { get; set; }

    public int? CoCuota { get; set; }
    public int? CoCuotaAnterior { get; set; }

    public int? NuPeriodo { get; set; }

    public decimal? SsSaldoCapital { get; set; }
    public decimal? SsAmortizacion { get; set; }
    public decimal? SsInteres { get; set; }
    public decimal? SsCuota { get; set; }
    public decimal? SsComision { get; set; }
    public decimal? SsCuotaTotal { get; set; }
    public decimal? SsAbono { get; set; }
    public string? FeVencimiento { get; set; }
    public string? TxDescripcion { get; set; }
    public decimal? SsCuotaPago { get; set; }

    public int? CoEtapa { get; set; } /*Campo que falta añadir - error de diseño*/
}

/*Reporte*/
public partial class CuotaModel
{
    public PersonaModel? Cliente { get; set; }
    public CreditoResumenModel? CreditoResumen { get; set; }
    public CuotaResumenModel? CuotaResumen { get; set; }

    public LoteModel? Lote { get; set; }


    public DocPagoModel? DocCuotaDetalle { get; set; }
    public CuotaDetalleModel? CuotaDetalle { get; set; }
    public List<CuotaDetalleModel> CuotaDetalleLst { get; set; } = new();




    // public FilterModel Filter { get; set; }
}


//public partial class FilterModel
//{
//    /*Campo de busqueda*/
//    #region Property Query Search Fields
//     public int? CoEtapa { get; set; }
//     public string? FeInicio { get; set; }
//     public string? FeFinal { get; set; }
//     public int? FlVencio { get; set; }

//     public string? NoManzana { get; set; }
//     public int? CoModoPago { get; set; }
//     public int? FlBono { get; set; }

//     public string? NoInputSearch { get; set; }

//    #endregion Property  Query Search Fields
//}