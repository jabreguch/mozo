///<summary>
///
///</summary>
///<remarks>
///</remarks>
///<history>
/// t[Jonatan Abregu]	16/11/2021	Created
///</history>
namespace Mozo.Model.Urbano;

public record CotizacionCuotaFilter : BaseFilterDto { }
[Serializable]
public partial class CotizacionCuotaModel : BaseModel //<CotizacionCuotaModel>
{


    public int? CoProyecto { get; set; }



    public int? CoCotizacion { get; set; }



    public int? CoCotizacionCuota { get; set; }



    public int? NuPeriodo { get; set; }

    public decimal? SsSaldoCapital { get; set; }
    public decimal? SsAmortizacion { get; set; }
    public decimal? SsInteres { get; set; }
    public decimal? SsCuota { get; set; }
    public decimal? SsComision { get; set; }
    public decimal? SsCuotaTotal { get; set; }
    public decimal? SsAbono { get; set; }

    public string? FeVencimiento { get; set; }
    public string? FePago { get; set; }
    public int? FlPago { get; set; }
    public int? CoModoPago { get; set; }
    public int? CoBanco { get; set; }
    public string? NuDocumento { get; set; }
    public string? NuOperacion { get; set; }
    public string? TxDescripcion { get; set; }
}

/*Reporte*/
public partial class CotizacionCuotaModel
{
    public string? NoArchivoEmpresa { get; set; }


    public string? NoExtensionEmpresa { get; set; }

    public string? NoDireccion { get; set; }


    public string? NoTelefonoEmpresa { get; set; }

    public string? NoCorreoEmpresa { get; set; }


    public string? NuDocumentoCliente { get; set; }


    public string? NoTipoDocumentoCliente { get; set; }


    public string? NoTipoDocumentoSiglaCliente { get; set; }


    public decimal? SsTotalFinanciado { get; set; }

    public int? QtCuotaPago { get; set; }
    public decimal? SsTotalPago { get; set; }

    public string? NoCliente { get; set; }

    public int? QtPeriodo { get; set; }

    public int? CoEtapa { get; set; }
    public string? FePagoFin { get; set; }
    public string? FePagoIni { get; set; }

    public int? NuLote { get; set; }
    public string? NoManzana { get; set; }
    public int? FlVencio { get; set; }

    public int? FlTerminarDeuda { get; set; }


    public int? CoCuotaEditableSiguiente { get; set; }


    public int? CoCuotaEditableActual { get; set; }

    public string? NoBanco { get; set; }
    public string? NoModoPago { get; set; }

    public int? QtDiaVencimiento { get; set; }
}