///<summary>
///
///</summary>
///<remarks>
///</remarks>
///<history>
/// t[Jonatan Abregu]	19/07/2022	Created
///</history>
namespace Mozo.Model.Catalogo;

public record ProductoAtributoFilter : BaseFilterDto //ProductoAtributoFilter>
{

}

[Serializable]
public partial class ProductoAtributoModel : BaseModel //<ProductoAtributoModel>
{
    public int? CoProductoAtributo { get; set; }
    public int? CoProducto { get; set; }
    public int? CoAtributo { get; set; }
    public string? NoValor { get; set; }
}

public partial class ProductoAtributoModel
{

}