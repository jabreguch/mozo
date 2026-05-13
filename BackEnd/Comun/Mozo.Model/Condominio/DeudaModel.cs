///<summary>
///
///</summary>
///<remarks>
///</remarks>
///<history>
/// t[Jonatan Abregu]	10-04-2020	Created
///</history>
namespace Mozo.Model.Condominio;

public record DeudaFilter : BaseFilterDto
{

}

[Serializable]
public class DeudaModel : BaseModel //<DeudaModel>
{
    public int? NuAnno { get; set; }
    public int? CoTipoServicio { get; set; }

    public int? CoDeuda { get; set; }
    public int? CoPeriodo { get; set; }
    public int? CoCondominio { get; set; }
    public string? NoCondominio { get; set; }
    public int? CoEdificio { get; set; }
    public int? CoPredio { get; set; }
    public decimal? SsMontoConsumoAcordado { get; set; }
    public decimal? SsMontoConsumoSinAcordar { get; set; }
    public decimal? SsMontoConsumoPagoAcordado { get; set; }
    public decimal? SsMontoConsumoPagoSinAcordar { get; set; }
    public List<DeudaDetalleModel>? DetalleDeudaList { get; set; }


    public decimal? NuArea { get; set; }

    public int? NuPiso { get; set; }
    public string? NuPredio { get; set; }
    public string? NuMedidorAgua { get; set; }
    public string? NuMedidorElectrico { get; set; }

    public string? NoEdificio { get; set; }

    public string? FeInicial { get; set; }
    public string? FeFinal { get; set; }

    public string? FeContrato { get; set; }
    public string? FeIniContrato { get; set; }
    public string? FeFinContrato { get; set; }
    public string? NoPeriodo { get; set; }
    public string? NoCompletoInquilino { get; set; }
    public string? NoTipoDocumentoSiglaInquilino { get; set; }
    public string? NuDocumentoInquilino { get; set; }
    public int? CoTipoPredio { get; set; }
    public int? CoPredioAlquiler { get; set; }
    public int? FlAlquilado { get; set; }
    public int? CoInquilino { get; set; }
}