///<summary>
///
///</summary>
///<remarks>
///</remarks>
///<history>
/// t[Jonatan Abregu]	10-04-2020	Created
///</history>
namespace Mozo.Model.Condominio;

public record ConsumoFilter : BaseFilterDto
{

}

[Serializable]
public class ConsumoModel : BaseModel //<ConsumoModel>
{
    public int? CoConsumo { get; set; }
    public int? CoCondominio { get; set; }
    public string? NoCondominio { get; set; }
    public int? CoEdificio { get; set; }
    public int? CoPredio { get; set; }
    public int? CoTipoServicio { get; set; }
    public int? CoDeuda { get; set; }
    public string? NoTipoServicio { get; set; }
    public string? NoEdificio { get; set; }
    public int? CoPeriodo { get; set; }
    public int? FlAcordadoInquilino { get; set; }
    public string? FeLecturaAnterior { get; set; }
    public decimal? NuLecturaAnterior { get; set; }
    public string? FeVencimiento { get; set; }
    public decimal? NuLecturaConsumo { get; set; }
    public decimal? SsMontoConsumo { get; set; }
    public decimal? SsMontoConsumoPago { get; set; }
    public decimal? SsMontoConsumoAcordado { get; set; }
    public decimal? SsMontoConsumoSinAcordar { get; set; }
    public decimal? SsMontoConsumoPagoAcordado { get; set; }
    public decimal? SsMontoConsumoPagoSinAcordar { get; set; }
    //End: Campos de la deuda
    public decimal? NuLecturaActual { get; set; }


    public int? CoCuenta { get; set; }


    public int? NuPiso { get; set; }
    public string? NuPredio { get; set; }
    public string? NuMedidorAgua { get; set; }
    public string? NuMedidorElectrico { get; set; }
    public decimal? NuArea { get; set; }
    public int? CoDeudaDetalle { get; set; }
    public decimal? NuLecturaInicial { get; set; }
    public int? CoTipoServicioPadre { get; set; }
    public int? CoEdificioConsumo { get; set; }

    public EdificioVistaModel? EdificioVista { get; set; }
    public PredioVistaModel? PredioVista { get; set; }
    public EdificioProcesoModel? EdificioProceso { get; set; }
    public PredioProcesoModel? PredioProceso { get; set; }


    public string? FeInicial { get; set; }
    public string? FeFinal { get; set; }

    public string? FeContrato { get; set; }
    public string? FeIniContrato { get; set; }
    public string? FeFinContrato { get; set; }

    // Sistema de gastos
    public int? CoMovimiento { get; set; }

    public int? CoTipoPredio { get; set; }
    public int? CoPredioAlquiler { get; set; }
    public string? NoTipoPredio { get; set; }
    public string? FeDocumento { get; set; }
    public string? NuDocumento { get; set; }
    public int? CoCalculo { get; set; }
    public int? FlAlquilado { get; set; }
    public int? FlComprobantePago { get; set; }
    public int? NuOrden { get; set; }
    public string? NuDocumentoInquilino { get; set; }
    public string? NoCompletoInquilino { get; set; }
    public string? NoTipoDocumentoSiglaInquilino { get; set; }
    public string? TxDescripcion { get; set; }
}