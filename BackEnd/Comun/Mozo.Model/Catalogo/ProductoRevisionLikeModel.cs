///<summary>
///
///</summary>
///<remarks>
///</remarks>
///<history>
/// t[Jonatan Abregu]	12/08/2022	Created
///</history>
namespace Mozo.Model.Catalogo;

public record ProductoRevisionLikeFilter : BaseFilterDto { }

[Serializable()]
public class ProductoRevisionLikeModel : BaseModel //<ProductoRevisionLikeModel>
{
    public int? CoProductoRevisionLike { get; set; }
    public int? CoProductoRevision { get; set; }
    public int? NuYes { get; set; }
    public int? NuNot { get; set; }
}