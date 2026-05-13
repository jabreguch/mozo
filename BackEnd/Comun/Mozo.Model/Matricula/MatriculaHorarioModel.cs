///<summary>
///
///</summary>
///<remarks>
///</remarks>
///<history>
/// t[-]	18/03/2021	Created
///</history>
namespace Mozo.Model.Matricula;

public record MatriculaHorarioFilter : BaseFilterDto { }

[Serializable]
public partial class MatriculaHorarioModel : BaseModel //<MatriculaHorarioModel>
{


    public int? CoProgramaHorario { get; set; }



    public int? CoMatriculaHorario { get; set; }



    public int? CoMatricula { get; set; }



    public int? CoPrograma { get; set; }




    public int? CoPaquete { get; set; }


    public int? FlAsistio { get; set; }

    public string? NoAlumno { get; set; }

    public string? NoTipoHorario { get; set; }


    public int? NuOrdenTipoHorario { get; set; }

    public int? CoTipoHorario { get; set; }
}

public partial class MatriculaHorarioModel
{
    public string? NoPaquete { get; set; }
    public string? NoPrograma { get; set; }
}