///<summary>
///
///</summary>
///<remarks>
///</remarks>
///<history>
/// t[Jonatan Abregu]	04-05-2020	Created
///</history>
namespace Mozo.Model.Empresa;

public record BlogMensajeFilter : BaseFilterDto { }

[Serializable]
public partial class BlogMensajeModel : BaseModel //<BlogMensajeModel>
{
    public int? CoBlog { get; set; }
    public int? CoBlogMensaje { get; set; }
    public string? NoMensaje { get; set; }
    public string? NoEmail { get; set; }
    public string? NoPersona { get; set; }

}
public partial class BlogMensajeModel
{
    public string? NoApellidoP { get; set; }
    public string? NoApellidoM { get; set; }
}