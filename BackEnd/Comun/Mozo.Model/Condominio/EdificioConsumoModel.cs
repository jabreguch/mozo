///<summary>
///
///</summary>
///<remarks>
///</remarks>
///<history>
/// t[Jonatan Abregu]	10-04-2020	Created
///</history>
namespace Mozo.Model.Condominio;

public record EdificioConsumoFilter : BaseFilterDto
{

}


[Serializable]
public class EdificioConsumoModel : BaseModel //<EdificioConsumoModel>
{
    public int? CoEdificioConsumo { get; set; }
    public int? CoCuenta { get; set; }
    public int? NuPiso { get; set; }

    public string? NuPredio { get; set; }
    public string? NuMedidorAgua { get; set; }
    public string? NuEdificio { get; set; }

    public string? NuMedidorElectrico { get; set; }
    public int? CoPeriodo { get; set; }
    public int? CoCondominio { get; set; }
    public string? NoCondominio { get; set; }
    public int? CoEdificio { get; set; }
    public int? CoPredio { get; set; }
    public int? CoTipoServicio { get; set; }
    public string? FeVencimiento { get; set; }
    public decimal? NuLecturaActual { get; set; }
    public decimal? NuLecturaAnterior { get; set; }
    public decimal? NuLecturaConsumo { get; set; }

    // public List<BeServicioCalculo> ServicioCalculoCol { get; set; }

    // public BeServicioCalculo ServicioCalculo { get; set; }

    public decimal? SsMontoConsumo { get; set; }

    public string? NoEdificio { get; set; }
    public string? NoTipoServicio { get; set; }
    public decimal? NuLecturaInicial { get; set; }
    public int? CoTipoServicioPadre { get; set; }
    public int? CoServicio { get; set; }
    public EdificioVistaModel? EdificioVista { get; set; }
    public PredioVistaModel? PredioVista { get; set; }
    public EdificioProcesoModel? EdificioProceso { get; set; }
    public PredioProcesoModel? PredioProceso { get; set; }
    // public string? NoTipoPredioIncluye { get; set; }

    public string? NoCalculo { get; set; }
    public int? FlComprobantePago { get; set; }
    public string? NuDocumento { get; set; }
    public string? FeDocumento { get; set; }
    public int? CoCalculo { get; set; }
    public int? CoTipoPredio { get; set; }
    public string? TxDescripcion { get; set; }
    public int? NuAnno { get; set; }
    public int? FlAcordadoInquilino { get; set; }
    public int? NuOrden { get; set; }
}