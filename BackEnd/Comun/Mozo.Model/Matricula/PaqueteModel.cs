///<summary>
///
///</summary>
///<remarks>
///</remarks>
///<history>
/// t[-]	19/03/2021	Created
///</history>
namespace Mozo.Model.Matricula;

public record PaqueteFilter : BaseFilterDto { }

[Serializable]
public partial class PaqueteModel : BaseModel //<PaqueteModel>
{


    public int? CoPaquete { get; set; }

    public string? NoPaquete { get; set; }
    public string? FeInicio { get; set; }
    public string? FeFin { get; set; }
    public decimal? SsMatricula { get; set; }

    public int? CoCategoria { get; set; }
    public string? NoCategoria { get; set; }
    public string? TxDescripcion { get; set; }



    public int? CoGrado { get; set; }

    public string? NoGrado { get; set; }

    public List<ProgramaModel>? ProgramaList { get; set; }


    public List<PaqueteProgramaModel>? PaqueteProgramaList { get; set; }

    public int? FlPublicoGeneral { get; set; }
}

public partial class PaqueteModel
{
    public decimal? SsMonto { get; set; }
}