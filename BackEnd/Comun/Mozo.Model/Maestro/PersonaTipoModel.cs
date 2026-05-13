///<summary>
///
///</summary>
///<remarks>
///</remarks>
///<history>
/// t[Jonatan Abregu]	18-04-2020	Created
///</history>
namespace Mozo.Model.Maestro;

public record PersonaTipoFilter : BaseFilterDto //PersonaTipoFilter>
{
    public int? CoModulo { get; set; }
    public int? CoPersona { get; set; }
}
[Serializable]
public partial class PersonaTipoModel : BaseModel //<PersonaTipoModel>
{
    public int? CoPersonaTipo { get; set; }
    public int? FlDesabilitado { get; set; }
    public int? CoModulo { get; set; }
    public int? CoPersona { get; set; }
    public int? CoTipo { get; set; }
    public string? NoTipo { get; set; }
    public string? NoModuloDescripcion { get; set; }
}
public partial class PersonaTipoModel
{

}