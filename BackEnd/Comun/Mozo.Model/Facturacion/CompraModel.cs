using Mozo.Model.Contabilidad;
using Mozo.Model.Maestro;

///<summary>
///
///</summary>
///<remarks>
///</remarks>
///<history>
/// t[Jonatan Abregu]	21/09/2021	Created
///</history>
namespace Mozo.Model.Facturacion;

public record CompraFilter : BaseFilterDto
{
}

[Serializable]
public partial class CompraModel : BaseModel //<CompraModel>
{
    public int? CoCompra { get; set; }

    public int? CoComprobantePago { get; set; }

    public string? NuSerie { get; set; }
    public string? NuDocumento { get; set; }
    public string? FeCompra { get; set; }
    public string? FeVencimiento { get; set; }
    public int? CoMoneda { get; set; }
    public int? CoLocal { get; set; }
    public int? CoTipoCambio { get; set; }

    public int? CoProveedor { get; set; }

    public decimal? SsPrecio { get; set; }

    public decimal? SsImpuesto { get; set; }



    public int? FlImpuestoIncluyePrecio { get; set; }

    public decimal? SsDescuento { get; set; }
    public int? NuNotaEntrada { get; set; }

    public int? CoModoPago { get; set; }
    public int? CoMedioPago { get; set; }

    public string? TxDescripcion { get; set; }

    public int? CoDocPago { get; set; }

}
public partial class CompraModel
{



    public string? NoMoneda { get; set; }
    public string? NoMonedaSigla { get; set; }
    public string? NoModoPago { get; set; }
    public string? NoMedioPago { get; set; }

    public TipoCambioModel? TipoCambio { get; set; }
    public PersonaModel? Proveedor { get; set; }


}