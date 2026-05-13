///<summary>
///
///</summary>
///<remarks>
///</remarks>
///<history>
/// t[-]	25/03/2021	Created
///</history>
namespace Mozo.Model.Matricula;

public record ProgramaNotaFilter : BaseFilterDto { }


[Serializable]
public class ProgramaNotaModel : BaseModel //<ProgramaNotaModel>
{
    //private string? BgColor = new[] { "a", "b", "c" };
    private string[] BgColorArray =
    {
        "bg-primary text-white",
        "bg-secondary",
        "bg-success text-white",
        "bg-danger text-white",
        "bg-warning text-white",
        "bg-info text-white",
        "bg-light text-dark",
        "bg-dark text-white",
        "bg-gray-100 text-dark-50",
        "bg-gray-200 text-dark-50",
        "bg-gray-300 text-dark-50"
    };



    public int? CoProgramaNota { get; set; }



    public int? CoTipoNota { get; set; }



    public int? CoPrograma { get; set; }

    public decimal? PqPeso { get; set; }
    public string? NoTipoNota { get; set; }

    public string? NoTipoNotaSigla { get; set; }
    public int? NuOrdenTipoNota { get; set; }


    public string? TxDescripcion { get; set; }
    public int? NuNota { get; set; }



    public int? CoMatriculaNota { get; set; }



    public int? CoMatricula { get; set; }

    public string? BgColor(int IndexBgColor)
    {
        return BgColorArray[IndexBgColor];
    }
}