///<summary>
///
///</summary>
///<remarks>
///</remarks>
///<history>
/// t[Jonatan Abregu]	15-04-2020	Created
///</history>
namespace Mozo.Model.Empresa;

public record BandejaEntradaFilter : BaseFilterDto { }


[Serializable]
public partial class BandejaEntradaModel : BaseModel //<BandejaEntradaModel>
{


    public int? CoEtiqueta { get; set; }
    public int? CoBandejaEntrada { get; set; }
    public string? NoPersona { get; set; }
    public string? NoEmail { get; set; }
    public string? NoTelefono { get; set; }
    public string? NoAsunto { get; set; }
    public string? NoMensaje { get; set; }
    public int? CoServicio { get; set; }

    public int? FlFavorito { get; set; }


}
public partial class BandejaEntradaModel
{
    public string? NoEtiqueta { get; set; }
    public string? NoServicio { get; set; }


    public string? NoApellidoP { get; set; }
    public string? NoApellidoM { get; set; }

    public string? NoServicioPadre { get; set; }
}