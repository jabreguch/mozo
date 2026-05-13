///<summary>
///
///</summary>
///<remarks>
///</remarks>
///<history>
/// t[Jonatan Abregu]	12/08/2022	Created
///</history>
namespace Mozo.Model.Catalogo;

public record ProductoRevisionFilter : BaseFilterDto { }

[Serializable()]
public class ProductoRevisionModel : BaseModel //<ProductoRevisionModel>
{
    public int? CoProducto { get; set; }
    public int? CoProductoRevision { get; set; }
    public int? CoCliente { get; set; }
    public string? TxRevision { get; set; }
    public string? NoPros { get; set; }
    public string? NoCons { get; set; }
    public int? NuEstrella { get; set; }
}