///<summary>
///
///</summary>
///<remarks>
///</remarks>
///<history>
/// t[Jonatan Abregu]	15-04-2020	Created
///</history>
namespace Mozo.Model.Empresa;

public record HorarioFilter : BaseFilterDto { }
[Serializable]
public class HorarioModel : BaseModel //<HorarioModel>
{
    public int? CoHorario { get; set; }
    public int? CoDia { get; set; }
    public string? NoDia { get; set; }
    public string? HoInicio { get; set; }
    public string? HoFinal { get; set; }
}