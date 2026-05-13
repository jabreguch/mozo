///<summary>
///
///</summary>
///<remarks>
///</remarks>
///<history>
/// t[Jonatan Abregu]	21/09/2021	Created
///</history>
namespace Mozo.Model.Inventario;

public record AlmacenFilter : BaseFilterDto { }
[Serializable]
public partial class AlmacenModel : BaseModel //<AlmacenModel>
{
    public int? CoAlmacen { get; set; }
    public string? NoAlmacen { get; set; }
    public int? CoLocal { get; set; }
    public int? FlPrincipal { get; set; }
}
public partial class AlmacenModel
{
    public string? NoLocal { get; set; }
}