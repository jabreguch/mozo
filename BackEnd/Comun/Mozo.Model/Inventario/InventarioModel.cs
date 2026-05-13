///<summary>
///
///</summary>
///<remarks>
///</remarks>
///<history>
/// t[Jonatan Abregu]	21/09/2021	Created
///</history>
namespace Mozo.Model.Inventario;

public record InventarioFilter : BaseFilterDto { }

[Serializable]
public partial class InventarioModel : BaseModel //<InventarioModel>
{
    public int? CoInventario { get; set; }

    public int? CoCompra { get; set; }

    public int? CoVenta { get; set; }

    public int? CoTipoMovimiento { get; set; }

}
public partial class InventarioModel
{


}