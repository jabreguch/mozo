///<summary>
///
///</summary>
///<remarks>
///</remarks>
///<history>
/// t[Jonatan Abregu]	10-04-2020	Created
///</history>
namespace Mozo.Model.Condominio;

public record ServicioFilter : BaseFilterDto //ServicioFilter>
{

}

[Serializable]
public class ServicioModel : BaseModel //<ServicioModel>
{
    public int? CoCondominio { get; set; }
    public int? CoServicio { get; set; }
    public int? FlAcordadoInquilino { get; set; }
    public string? NoCondominio { get; set; }
    public string? CoCategoriaSubCategoria { get; set; }
    public int? CoEdificio { get; set; }
    public int? CoPredio { get; set; }
    public int? CoTipoServicio { get; set; }
    public decimal? NuLecturaInicial { get; set; }
    public string? FeLecturaInicial { get; set; }
    public int? NuOrden { get; set; }
    public string? NoCalculo { get; set; }
    // public string? NoTipoPredioIncluye { get; set; }

    public int? CoCuenta { get; set; }
    public string? NoCuenta { get; set; }
    public string? NoTipoServicio { get; set; }
    public string? NoTipoServicioPadre { get; set; }
    public string? NoEdificio { get; set; }

    // public List<BeServicioCalculo> ServicioCalculoCol { get; set; }


    public string? NuMedidorElectrico { get; set; }
    public decimal? NuArea { get; set; }
    public string? NuMedidorAgua { get; set; }
    public string? NuEdificio { get; set; }
    public int? NuPiso { get; set; }
    public string? NuPredio { get; set; }
    public decimal? NuLecturaAnterior { get; set; }
    public decimal? SsMontoConsumo { get; set; }
    public decimal? NuLecturaConsumo { get; set; }
    public int? CoPeriodo { get; set; }
    public int? CoTipoServicioPadre { get; set; }

    public decimal? NuLecturaConsumoEdificio { get; set; }
    public EdificioVistaModel? EdificioVista { get; set; }
    public PredioVistaModel? PredioVista { get; set; }
    public EdificioProcesoModel? EdificioProceso { get; set; }
    public PredioProcesoModel? PredioProceso { get; set; }




    public int? CoTipoPredio { get; set; }
    public int? CoCalculo { get; set; }
    public string? TxDescripcion { get; set; }
    public int? QtArea { get; set; }
    public int? FlAlquilado { get; set; }
    public int? FlComprobantePago { get; set; }
    public int? CoPredioAlquiler { get; set; }
    public string? NuDocumentoInquilino { get; set; }
    public string? NoCompletoInquilino { get; set; }
    public string? NoTipoDocumentoSiglaInquilino { get; set; }
    public string? NoTipoPredio { get; set; }
    public int? FlOpciones { get; set; } // VErficar despues borrar
}
