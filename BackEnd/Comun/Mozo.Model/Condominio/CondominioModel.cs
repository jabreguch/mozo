///<summary>
///
///</summary>
///<remarks>
///</remarks>
///<history>
/// t[Jonatan Abregu]	10-04-2020	Created
///</history>
namespace Mozo.Model.Condominio;

public record CondominioFilter : BaseFilterDto
{

}

[Serializable]
public class CondominioModel : BaseModel //<CondominioModel>
{
    public int? CoCondominio { get; set; }
    public string? NoCondominio { get; set; }
    public string? NoDireccion { get; set; }
    public int? CoTipoPredio { get; set; }
    public List<EdificioModel>? EdificioCol { get; set; }
}