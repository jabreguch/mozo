///<summary>
///
///</summary>
///<remarks>
///</remarks>
///<history>
/// t[Jonatan Abregu]	21/09/2021	Created
///</history>
namespace Mozo.Model.Inventario;

public record StockFilter : BaseFilterDto { }
[Serializable]
public partial class StockModel : BaseModel //<StockModel>
{

    public int? CoStock { get; set; }
    public int? CoAlmacen { get; set; }
    public int? CoProducto { get; set; }
    public int? QtUnidad { get; set; }
    public int? CoLocal { get; set; }
}

public partial class StockModel
{


}