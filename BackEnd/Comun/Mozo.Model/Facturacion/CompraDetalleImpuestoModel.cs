///<summary>
///
///</summary>
///<remarks>
///</remarks>
///<history>
/// t[Jonatan Abregu]	21/09/2021	Created
///</history>
namespace Mozo.Model.Facturacion;

public record CompraDetalleImpuestoModelFilter : BaseFilterDto { }

[Serializable]
public partial class CompraDetalleImpuestoModel : BaseModel //<CompraDetalleImpuestoModel>
{


    public int? CoCompraDetalleImpuesto { get; set; }
    public int? CoCompraDetalle { get; set; }
    public int? CoImpuesto { get; set; }
    public int? CoImpuestoHijo { get; set; }

    public decimal? SsMonto { get; set; }
    public decimal? PqTaza { get; set; }
}

public partial class CompraDetalleImpuestoModel
{


}