///<summary>
///
///</summary>
///<remarks>
///</remarks>
///<history>
/// t[-]	18/03/2021	Created
///</history>
namespace Mozo.Model.Matricula;

public record MatriculaPensionFilter : BaseFilterDto
{ }

[Serializable]
public class MatriculaPensionModel : BaseModel //<MatriculaPensionModel>
{


    public int? CoMatriculaPension { get; set; }



    public int? CoProgramaPension { get; set; }



    public int? CoMatricula { get; set; }



    public int? CoPrograma { get; set; }


    public decimal? SsMonto { get; set; }



    public int? CoBanco { get; set; }

    public string? NuDocumento { get; set; }

    public string? NoBanco { get; set; }

    public int? FlPago { get; set; }

    public int? NuOrden { get; set; }

    public string? FeVencimiento { get; set; }


    public int? CoTipoPension { get; set; }
    public string? NoTipoPension { get; set; }


    public string? NoTipoPensionSigla { get; set; }


    public int? NuOrdenTipoPension { get; set; }

    public int? CoPaquete { get; set; }
    public string? NoPaquete { get; set; }
    public string? FeInicio { get; set; }
    public string? FeFin { get; set; }
    public decimal? SsMatricula { get; set; }
    public int? CoGrado { get; set; }

    public string? NoCurso { get; set; }


    public string? TxDescripcionCurso { get; set; }

    public string? NoAlumno { get; set; }
}