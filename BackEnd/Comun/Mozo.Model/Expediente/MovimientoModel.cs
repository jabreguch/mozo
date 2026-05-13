///<summary>
///
///</summary>
///<remarks>
///</remarks>
///<history>
/// t[Jonatan Abregu]	10-04-2020	Created
///</history>
namespace Mozo.Model.Expediente;

public record MovimientoFilter : BaseFilterDto
{ }

[Serializable]
public class MovimientoModel : BaseModel //<MovimientoModel>
{


    public int? CoMovimiento { get; set; }



    public int? CoCuenta { get; set; }



    public int? CoExpediente { get; set; }



    public int? CoCategoria { get; set; }



    public int? CoSubCategoria { get; set; }



    public int? CoAreaAnterior { get; set; }



    public int? CoResponsableAnterior { get; set; }



    public int? CoProveedor { get; set; }



    public int? CoAreaActual { get; set; }



    public int? CoTipoMovimiento { get; set; }



    public int? CoResponsableActual { get; set; }

    public string? FeMovimiento { get; set; }
    public string? TxDescripcion { get; set; }
    public double? SsMonto { get; set; }
    public int? FlPrestamo { get; set; }
    public string? FeDevolucion { get; set; }
    public int? FlTraspaso { get; set; }



    public int? CoBeneficiario { get; set; }


    public double? SsMontoDevolucion { get; set; }
}