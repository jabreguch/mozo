///<summary>
///
///</summary>
///<remarks>
///</remarks>
///<history>
/// t[-]	19/03/2021	Created
///</history>
namespace Mozo.Model.Matricula;

public record PaqueteProgramaFilter : BaseFilterDto
{ }

[Serializable]
public class PaqueteProgramaModel : BaseModel //<PaqueteProgramaModel>
{


    public int? CoPaquetePrograma { get; set; }



    public int? CoPaquete { get; set; }



    public int? CoPrograma { get; set; }



    public int? CoCurso { get; set; }


    public string? NuAula { get; set; }



    public int? QtHora { get; set; }

    public int? QtMaxAlumno { get; set; }
    public int? QtPension { get; set; }


    public string? TxDescripcionPrograma { get; set; }

    public string? NoCurso { get; set; }


    public string? TxDescripcionCurso { get; set; }

    public string? NoProfesor { get; set; }


    public string? NoSede { get; set; }

    //   public List<ProgramaModel> ProgramaList { get; set; }
}