///<summary>
///
///</summary>
///<remarks>
///</remarks>
///<history>
/// t[Jonatan Abregu]	16/11/2021	Created
///</history>
namespace Mozo.Model.Urbano;

public record ProyectoFilter : BaseFilterDto { }
[Serializable]
public class ProyectoModel : BaseModel //<ProyectoModel>
{


    public int? CoProyecto { get; set; }

    public string? NoProyecto { get; set; }
    public string? NoDireccion { get; set; }
    public string? NuPartida { get; set; }
    public List<EtapaModel>? EtapaLst { get; set; }
}