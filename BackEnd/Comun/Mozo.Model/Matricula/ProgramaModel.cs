///<summary>
///
///</summary>
///<remarks>
///</remarks>
///<history>
/// t[-]	18/03/2021	Created
///</history>
namespace Mozo.Model.Matricula;

public record ProgramaFilter : BaseFilterDto { }

[Serializable]
public class ProgramaModel : BaseModel //<ProgramaModel>
{


    public int? CoPaquetePrograma { get; set; }



    public int? CoPaquete { get; set; }



    public int? CoMatricula { get; set; }



    public int? CoPrograma { get; set; }



    public int? CoProfesor { get; set; }



    public int? CoCurso { get; set; }



    public int? QtHora { get; set; }



    public int? CoSede { get; set; }



    public int? QtMaxAlumno { get; set; }

    public string? NuAula { get; set; }

    public string? TxDescripcion { get; set; }
    public string? NoProfesor { get; set; }

    public string? NoCurso { get; set; }


    public string? TxDescripcionCurso { get; set; }

    public string? NoSede { get; set; }

    public string? NoPrograma { get; set; }

    public List<ProgramaHorarioModel>? HorarioList { get; set; }


    public List<ProgramaNotaModel>? NotaList { get; set; }

    public List<ProgramaPensionModel>? PensionList { get; set; }
}