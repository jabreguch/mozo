///<summary>
///
///</summary>
///<remarks>
///</remarks>
///<history>
/// t[Jonatan Abregu]	16/11/2018	Created
///</history>
namespace Mozo.Model.Seguridad;

public record RedirectFilterDto : BaseFilterDto
{
    public int? CoRedirect { get; set; }

}
[Serializable]
public partial class RedirectModel : BaseModel //<RedirectModel>
{
    public int? CoRedirect { get; set; }
    public int? CoPermiso { get; set; }
    public int? CoPersona { get; set; }
    public int? CoModulo { get; set; }
    public string? FeRedirect { get; set; }

    public string? NoUsuario { get; set; }
    public string? NoClave { get; set; }
}
public partial class RedirectModel
{

}