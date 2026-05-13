///<summary>
///
///</summary>
///<remarks>
///</remarks>
///<history>
/// t[Jonatan Abregu]	25-04-2020	Created
///</history>
namespace Mozo.Model.Empresa;

public record BlogFilter : BaseFilterDto
{

}


[Serializable]
public partial class BlogModel : BaseModel //<BlogModel>
{
    public int? CoAutor { get; set; }
    public string? FeBlog { get; set; }
    public string? NoServicio { get; set; }
    public string? NoSubServicio { get; set; }
    public int? CoServicio { get; set; }
    public int? CoSubServicio { get; set; }
    public int? CoBlog { get; set; }
    public string? NoTitulo { get; set; }
    public string? TxBlog { get; set; }
    public string? NoIdeaCentral { get; set; }
    public string? NoAutor { get; set; }

    public string? NoPersona { get; set; }
    public string? NoApellidoP { get; set; }

    public string? NoApellidoM { get; set; }

    //
    public string? NoNombreCompleto => string.Concat(NoPersona, " ", NoApellidoP, " ", NoApellidoM);
}

public partial class BlogModel
{
    public int? QtMensaje { get; set; }
    public int? QtSubServicio { get; set; }
    public int? QtServicio { get; set; }
    public int? QtTop { get; set; }
}