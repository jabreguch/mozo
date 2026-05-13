///<summary>
///
///</summary>
///<remarks>
///</remarks>
///<history>
/// t[Jonatan Abregu]	21/09/2021	Created
///</history>
namespace Mozo.Model.Facturacion;

public record VentaDetalleImpuestoModelFilter : BaseFilterDto
{
}

[Serializable]
public partial class VentaDetalleImpuestoModel : BaseModel //<VentaDetalleImpuestoModel>
{


    public int? CoVentaDetalleImpuesto { get; set; }
    public int? CoVentaDetalle { get; set; }
    public int? CoImpuesto { get; set; }
    public int? CoImpuestoHijo { get; set; }

    public decimal? SsMonto { get; set; }
    public decimal? PqTaza { get; set; }
}

public partial class VentaDetalleImpuestoModel
{


}