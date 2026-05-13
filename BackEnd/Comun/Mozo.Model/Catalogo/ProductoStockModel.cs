///<summary>
///
///</summary>
///<remarks>
///</remarks>
///<history>
/// t[Jonatan Abregu]	04/10/2021	Created
///</history>
namespace Mozo.Model.Catalogo;

public record ProductoStockFilter : BaseFilterDto { }

[Serializable]
public partial class ProductoStockModel : BaseModel //<ProductoStockModel>
{
    public int? CoProductoStock { get; set; }
    public int? CoProducto { get; set; }
    public int? CoAlmacen { get; set; }
    public int? CoLocal { get; set; }
    public int? QtInicial { get; set; }
    public int? QtMaxima { get; set; }
    public int? QtMinima { get; set; }
    public string? NoLocal { get; set; }
    public string? NoAlmacen { get; set; }
    public int? FlDefault { get; set; }
}

public partial class ProductoStockModel
{

    public string? NoLocalAndAlmacen => NoLocal + " - " + NoAlmacen;
}