///<summary>
///
///</summary>
///<remarks>
///</remarks>
///<history>
/// t[Jonatan Abregu]	19/07/2022	Created
///</history>
namespace Mozo.Model.Catalogo;

public record ProductoIndexFilter : BaseFilterDto
{
}

[Serializable]
public partial class ProductoIndexModel : BaseModel //<ProductoIndexModel>
{
    public int? CoProductoIndex { get; set; }
    public int? CoProducto { get; set; }
    public string? NoProducto { get; set; }
    public string? TxDescripcionCorta { get; set; }
    public string? TxBusqueda { get; set; }
    public int? CoCategoria { get; set; }
    public string? JsonAtributo { get; set; }
}

public partial class ProductoAtributoModel
{

}