///<summary>
///
///</summary>
///<remarks>
///</remarks>
///<history>
/// t[-]	18/03/2021	Created
///</history>
namespace Mozo.Model.Matricula;

public record MatriculaFilter : BaseFilterDto { }

[Serializable]
public class MatriculaModel : BaseModel //<MatriculaModel>
{


    public int? CoMatricula { get; set; }



    public int? CoAlumno { get; set; }

    public string? NoAlumno { get; set; }


    public string? FeMatricula { get; set; }



    public int? FlCertificado { get; set; }


    public string? NuDocumento { get; set; }



    public int? CoPaquete { get; set; }



    public int? CoPrograma { get; set; }


    public string? NoPaquete { get; set; }
    public string? FeInicio { get; set; }
    public string? FeFin { get; set; }
    public decimal? SsMatricula { get; set; }


    public string? TxDescripcionCiclo { get; set; }


    public string? NoGrado { get; set; }



    public int? CoGrado { get; set; }

    public List<ProgramaModel>? ProgramaList { get; set; }
}