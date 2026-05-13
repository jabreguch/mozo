///<summary>
///
///</summary>
///<remarks>
///</remarks>
///<history>
/// t[Jonatan Abregu]	10-04-2020	Created
///</history>
namespace Mozo.Model.Expediente;

public record MovimientoEstadoFilter : BaseFilterDto { }

[Serializable]
public class MovimientoEstadoModel : BaseModel //<MovimientoEstadoModel>
{


    public int? CoExpediente { get; set; }



    public int? CoMovimientoEstado { get; set; }



    public int? CoEstadoAnterior { get; set; }



    public int? CoEstadoActual { get; set; }


    public string? FeMovimientoEstado { get; set; }

    public string? NoEstadoAnterior { get; set; }

    public string? NoEstadoActual { get; set; }
}