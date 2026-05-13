///<summary>
///
///</summary>
///<remarks>
///</remarks>
///<history>
/// t[Jonatan Abregu]	18/09/2022	Created
///</history>
namespace Mozo.Model.Catalogo;

public record ProductoImpuestoFilter : BaseFilterDto { }

[Serializable()]
public partial class ProductoImpuestoModel : BaseModel //<ProductoImpuestoModel>
{
    public int? CoProductoImpuesto { get; set; }
    public int? CoProducto { get; set; }
    public int? CoImpuesto { get; set; }
    public int? CoImpuestoHijo { get; set; }
    public decimal? PqTaza { get; set; }
}
public partial class ProductoImpuestoModel
{


}