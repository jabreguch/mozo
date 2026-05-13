///<summary>
///
///</summary>
///<remarks>
///</remarks>
///<history>
/// t[Jonatan Abregu]	10-04-2020	Created
///</history>
namespace Mozo.Model.Condominio;

public record EdificioFilter : BaseFilterDto
{

}


[Serializable]
public class EdificioModel : BaseModel //<EdificioModel>
{
    public int? CoEdificio { get; set; }
    public int? CoCondominio { get; set; }
    public string? NoCondominio { get; set; }
    public string? NoEdificio { get; set; }
    public string? NuEdificio { get; set; }
    public IEnumerable<PeriodoModel>? PeriodoCol { get; set; }
    public IEnumerable<ServicioModel>? ServicioCol { get; set; }
    public int? CoTipoPredio { get; set; }
    public int? CoTabEdificio { get; set; }
}