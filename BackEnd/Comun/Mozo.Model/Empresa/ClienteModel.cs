///<summary>
///
///</summary>
///<remarks>
///</remarks>
///<history>
/// t[Jonatan Abregu]	07-05-2020	Created
///</history>
namespace Mozo.Model.Empresa;

public record ClienteFilter : BaseFilterDto { }
[Serializable]
public class ClienteModel : BaseModel //<ClienteModel>
{
    public string? NoCargo { get; set; }
    public int? CoCliente { get; set; }
    public string? NoCliente { get; set; }
    public string? TxOpinion { get; set; }
    public int? FlOpinion { get; set; }
}