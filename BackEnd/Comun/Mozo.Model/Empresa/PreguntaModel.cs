///<summary>
///
///</summary>
///<remarks>
///</remarks>
///<history>
/// t[Jonatan Abregu]	15-04-2020	Created
///</history>
namespace Mozo.Model.Empresa;

public record PreguntaFilter : BaseFilterDto { }

[Serializable]
public class PreguntaModel : BaseModel //<PreguntaModel>
{
    public int? NuOrden { get; set; }
    public int? CoPregunta { get; set; }
    public string? NoPregunta { get; set; }
    public string? NoRespuesta { get; set; }
}