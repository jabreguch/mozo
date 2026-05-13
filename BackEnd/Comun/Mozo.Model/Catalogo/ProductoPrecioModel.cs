///<summary>
///
///</summary>
///<remarks>
///</remarks>
///<history>
/// t[Jonatan Abregu]	18/09/2022	Created
///</history>
namespace Mozo.Model.Catalogo;

public record ProductoPrecioFilter : BaseFilterDto { }

[Serializable()]
public partial class ProductoPrecioModel : BaseModel //<ProductoPrecioModel>
{
    public int? CoProductoPrecio { get; set; }
    public int? CoTipoPrecio { get; set; }

    public int? CoProducto { get; set; }
    public decimal? SsPrecio { get; set; }
    public int? CoMoneda { get; set; }

}
public partial class ProductoPrecioModel
{
    public string? NoTipoPrecio { get; set; }
    public int? FlDefault { get; set; }

}