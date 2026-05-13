///<summary>
///
///</summary>
///<remarks>
///</remarks>
///<history>
/// t[Jonatan Abregu]	16/11/2021	Created
///</history>
namespace Mozo.Model.Urbano;

public record CotizacionFilter : BaseFilterDto { }
[Serializable]
public partial class CotizacionModel : BaseModel //<CotizacionModel>
{


    public int? CoProyecto { get; set; }



    public int? CoCotizacion { get; set; }



    public int? CoCliente { get; set; }



    public int? CoEtapa { get; set; }



    public int? CoLote { get; set; }

    public string? NoCliente { get; set; }

    public string? NoProyecto { get; set; }

    public decimal? SsCostoTotal { get; set; }
    public int? QtPeriodo { get; set; }

    public decimal? SsInicial { get; set; }
    public decimal? SsFinanciado { get; set; }
    public decimal? PqInteresAnual { get; set; }
    public decimal? PqInteresMensual { get; set; }
    public string? NoEtapa { get; set; }
    public decimal? SsInteres { get; set; }
    public int? CoBono { get; set; }


    public decimal? SsTotalFinanciado { get; set; }

    public decimal? SsCuota { get; set; }


    public string? NuDocumentoCliente { get; set; }


    public string? NoTipoDocumentoCliente { get; set; }


    public string? NoTipoDocumentoSiglaCliente { get; set; }


    public string? NoManzana { get; set; }
    public int? NuLote { get; set; }
    public decimal? NuArea { get; set; }
    public decimal? NuAreaLibre { get; set; }
    public decimal? NuAreaTechada { get; set; }


    public string? FeCotizacion { get; set; }
    public string? FePrimeraCuota { get; set; }
    public int? QtCuotaVencida { get; set; }

    public int? QtCuotaPago { get; set; }

    public decimal? SsTotalPago { get; set; }

    public int? CoVendedor { get; set; }
    public string? NoVendedor { get; set; }

    public int? CoModoPago { get; set; }


    public int? CoDocumentoTributario { get; set; }

    public string? NuDocumento { get; set; }
}

/*Reporte*/
public partial class CotizacionModel
{
    public string? NoDireccion { get; set; }
    public string? NoTelefono { get; set; }
    public string? NoCorreo { get; set; }
    public string? NoArchivoEmpresa { get; set; }


    public string? NoExtensionEmpresa { get; set; }


    public string? NoArchivoProyecto { get; set; }


    public string? NoExtensionProyecto { get; set; }


    public string? NoTipoDocumentoTributario { get; set; }

    public string? NoModoPago { get; set; }


    public string? FeCotizacionIni { get; set; }
    public string? FeCotizacionFin { get; set; }
}