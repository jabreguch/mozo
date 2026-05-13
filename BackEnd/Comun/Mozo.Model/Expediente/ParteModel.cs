///<summary>
///
///</summary>
///<remarks>
///</remarks>
///<history>
/// t[Jonatan Abregu]	10-04-2020	Created
///</history>
namespace Mozo.Model.Expediente;

public record ParteFilter : BaseFilterDto { }

[Serializable]
public class ParteModel : BaseModel //<ParteModel>
{


    public int? CoExpediente { get; set; }



    public int? CoParte { get; set; }



    public int? CoTipoParte { get; set; }



    public int? NuOrden { get; set; }



    public int? CoPersona { get; set; }

    public string? NoTipoParte { get; set; }

    public string? NoParte { get; set; }
}