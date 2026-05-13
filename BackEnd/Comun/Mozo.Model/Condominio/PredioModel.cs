using Mozo.Model.Maestro;

///<summary>
///
///</summary>
///<remarks>
///</remarks>
///<history>
/// t[Jonatan Abregu]	10-04-2020	Created
///</history>
namespace Mozo.Model.Condominio;

public record PredioFilter : BaseFilterDto //PredioFilter>
{

}

[Serializable]
public class PredioModel : BaseModel //<PredioModel>
{
    public int? CoVistaPredio { get; set; }
    public int? CoPredio { get; set; }
    public int? CoCondominio { get; set; }
    public int? CoDeuda { get; set; }
    public int? CoDeudaDetalle { get; set; }
    public decimal? SsMontoConsumoDeuda { get; set; }
    public decimal? SsMontoConsumoPagoDeuda { get; set; }
    public string? NoCondominio { get; set; }
    public int? CoEdificio { get; set; }
    public int? CoDueno { get; set; }
    public int? NuPiso { get; set; }
    public string? NuPredio { get; set; }
    public decimal? NuArea { get; set; }
    public string? NuPartida { get; set; }
    public string? NuMedidorElectrico { get; set; }
    public string? NuMedidorAgua { get; set; }
    public ArchivoModel? Archivo { get; set; }
    public ArchivoModel? ArchivoPredioAlquiler { get; set; }
    public string? NoEdificio { get; set; }
    public int? CoPeriodo { get; set; }

    // public List<BeConsumoInicial> ConsumoInicialCol { get; set; }

    public List<ConsumoModel>? ConsumoCol { get; set; }
    public DeudaModel? Deuda { get; set; }
    public int? CoCuenta { get; set; }
    public List<ServicioModel>? ServicioCol { get; set; }
    public List<ServicioPredioModel>? ServicioPredioList { get; set; }
    public List<PredioAlquilerModel>? PredioAlquilerCol { get; set; }
    public int? CoTipoServicio { get; set; }
    public int? CoPredioAlquiler { get; set; }
    public int? CoTipoPredio { get; set; }
    public string? NuDocumentoInquilino { get; set; }
    public string? NoCompletoInquilino { get; set; }
    public string? NoTipoDocumentoSiglaInquilino { get; set; }
    public string? NuDocumentoDueno { get; set; }
    public string? NoCompletoDueno { get; set; }
    public string? NoTipoDocumentoSiglaDueno { get; set; }
    public int? FlAlquilado { get; set; }
    public string? TxDescripcion { get; set; }
    public string? NoTipoPredio { get; set; }
    public int? CoTabPredio { get; set; }
    public string? NoExtensionPredioAlquiler { get; set; }

    //        TblPredio.NoArchivo,
    //TblPredio.NoExtension,
    //TblPredioAlquiler.NoArchivo As NoArchivoAlquiler,
    //TblPredioAlquiler.NoExtension As NoExtensionAlquiler,
}