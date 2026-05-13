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

public record PredioAlquilerFilter : BaseFilterDto //PredioAlquilerFilter>
{

}

[Serializable]
public class PredioAlquilerModel : BaseModel //<PredioAlquilerModel>
{
    public int? CoPredioAlquiler { get; set; }
    public int? CoEdificio { get; set; }
    public int? CoPeriodo { get; set; }
    public int? CoCondominio { get; set; }
    public string? NoCondominio { get; set; }
    public string? NoEdificio { get; set; }
    public string? NuPredio { get; set; }
    public int? CoPredio { get; set; }
    public int? CoInquilino { get; set; }
    public string? FeIniContrato { get; set; }
    public string? FeFinContrato { get; set; }
    public string? FeContrato { get; set; }
    public string? NoCompletoInquilino { get; set; }
    public string? NoTipoDocumentoSiglaInquilino { get; set; }
    public string? NuDocumentoInquilino { get; set; }
    public ArchivoModel? Archivo { get; set; }
    public List<DeudaModel>? DeudaCol { get; set; }
}