using Mozo.Model.Maestro;

///<summary>
///
///</summary>
///<remarks>
///</remarks>
///<history>
/// t[Jonatan Abregu]	16/03/2022	Created
///</history>
namespace Mozo.Model.Empresa;

public record EquipoFilter : BaseFilterDto { }
[Serializable]
public partial class EquipoModel : BaseModel //<EquipoModel>
{
    public int? CoEquipo { get; set; }
    public int? CoPersona { get; set; }
    public string? NoExtracto { get; set; }
}

public partial class EquipoModel
{
    public PersonaModel? Persona { get; set; }

}