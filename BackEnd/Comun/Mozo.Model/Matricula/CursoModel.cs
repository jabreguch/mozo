///<summary>
///
///</summary>
///<remarks>
///</remarks>
///<history>
/// t[-]	18/03/2021	Created
///</history>
namespace Mozo.Model.Matricula;

public record CursoFilter : BaseFilterDto { }

[Serializable]
public class CursoModel : BaseModel //<CursoModel>
{


    public int? CoCurso { get; set; }



    public int? CoCategoria { get; set; }

    public string? NoCategoria { get; set; }
    public string? NoCurso { get; set; }



    public int? QtHora { get; set; }

    public string? TxDescripcion { get; set; }
}