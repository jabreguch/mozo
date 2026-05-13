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

public record CreditoFilter : BaseFilterDto { }


[Serializable]
public partial class CreditoModel : BaseModel //<CreditoModel>
{


    public int? CoProyecto { get; set; }



    public int? CoCredito { get; set; }



    public int? CoCliente { get; set; }



    public int? CoEtapa { get; set; }



    public int? CoLote { get; set; }

    public decimal? SsCostoTotal { get; set; }
    public decimal? SsDescuento { get; set; }
    public int? QtPeriodo { get; set; }

    public decimal? SsSeparacion { get; set; }
    public decimal? SsInicial { get; set; }
    public decimal? SsFinanciado { get; set; }
    public decimal? PqInteresAnual { get; set; }
    public decimal? PqInteresMensual { get; set; }

    public decimal? SsInteres { get; set; }
    // public decimal? SsBono { get; set; }
    public int? CoDocBono { get; set; }
    public int? CoBono { get; set; }

    public decimal? SsTotalFinanciadoInicio { get; set; }

    public decimal? SsCuota { get; set; }
    public string? FeCredito { get; set; }
    public string? FePrimeraCuota { get; set; }
    public int? CoVendedor { get; set; }


    public int? CoModoPago { get; set; }


    public int? CoDocPagoInicial { get; set; }


    public int? CoDocPagoSeparacion { get; set; }


    public string? NoManzana { get; set; }
}

public partial class CreditoModel
{
    // public EmpresaModel Empresa { get; set; }
    // public ProyectoModel Proyecto { get; set; }
    // public EtapaModel Etapa { get; set; }
    public BonoModel? Bono { get; set; }
    public DocPagoModel? DocSeparacion { get; set; }
    public DocPagoModel? DocInicial { get; set; }
    public DocPagoModel? DocBono { get; set; }
    public LoteModel? Lote { get; set; }
    public PersonaModel? Cliente { get; set; }
    public CreditoResumenModel? CreditoResumen { get; set; }

    public PersonaModel? Vendedor { get; set; }



    // public CuotaResumenModel CuotaResumen { get; set; }
    // public CuotaModel Cuota { get; set; }
    // public CuotaDetalleModel CuotaDetalle { get; set; }
    // public DocPagoModel DocCuotaDetalle { get; set; }

    #region Property Query Fields Join

    public string? NoModoPago { get; set; }

    public decimal? SsTotalFinanciado => SsFinanciado + SsInteres;
    public List<CuotaModel>? CuotaLst { get; set; }


    #endregion


    #region Property Query Search Fields
    public int? FlBono { get; set; }
    public int? FlVencio { get; set; }


    #endregion
    #region Property Fields Title
    public string? NoProyecto { get; set; }
    public string? NoEtapa { get; set; }

    #endregion

}
