///<summary>
///
///</summary>
///<remarks>
///</remarks>
///<history>
/// t[-]	18/03/2021	Created
///</history>
namespace Mozo.Model.Matricula;

public record MatriculaNotaFilter : BaseFilterDto
{ }

[Serializable]
public class MatriculaNotaModel : BaseModel //<MatriculaNotaModel>
{


    public int? CoMatriculaNota { get; set; }



    public int? CoProgramaNota { get; set; }



    public int? CoMatricula { get; set; }



    public int? CoAlumno { get; set; }



    public int? CoCurso { get; set; }



    public int? CoPrograma { get; set; }



    public int? CoTipoNota { get; set; }

    public decimal? NuNota { get; set; }
    public string? TxDescripcion { get; set; }



    public decimal? PqPeso { get; set; }

    public string? NoTipoNota { get; set; }

    public string? NoTipoNotaSigla { get; set; }

    public string? NoAlumno { get; set; }
    public string? NuDocumento { get; set; }

    public int? CoPaquete { get; set; }

    public string? NoPaquete { get; set; }

    public string? NoPrograma { get; set; }

    public int? QtHorario { get; set; }
    public int? QtFalta { get; set; }
    public int? NuOrdenTipoNota { get; set; }

    public string? NoCurso { get; set; }
    public string? NoGrado { get; set; }

    public decimal? PqFaltda()
    {
        if (QtFalta > 0)
            return (decimal)QtFalta / (decimal)QtHorario * 100;
        return 0;
    }
}