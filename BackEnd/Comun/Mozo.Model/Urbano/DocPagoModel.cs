using static Mozo.Helper.Global.Glo;

///<summary>
///
///</summary>
///<remarks>
///</remarks>
///<history>
/// t[Jonatan Abregu]	02/03/2022	Created
///</history>
namespace Mozo.Model.Urbano;

public record DocPagoFilter : BaseFilterDto { }
[Serializable]
public partial class DocPagoModel : BaseModel //<DocPagoModel>
{


    public int? CoProyecto { get; set; }



    public int? CoDocPago { get; set; }



    public int? CoTipoDocumento { get; set; }

    public int? Co { get; set; }
    public string? FePago { get; set; }
    public int? CoMedioPago { get; set; }



    public int? CoBanco { get; set; }

    public string? NuOperacion { get; set; }



    public int? CoDocumentoTributario { get; set; }

    public string? FeDocumento { get; set; }
    public string? NuDocumento { get; set; }
    public string? TxDescripcion { get; set; }
}

public partial class DocPagoModel
{
    public string? NoMedioPago { get; set; }
    public string? NoBanco { get; set; }


    public string? NoTipoDocumentoTributario { get; set; }

    public string? NoMensajePago
    {
        get
        {
            string? tx = null;
            if (NuOperacion.NoNulo())
                tx = string.Concat(NoMedioPago, NoBanco.NoNulo() ? " del banco " + NoBanco + " operación " + NuOperacion : "", " de fecha ", FePago.FechaHora(), ". ");

            if (NuDocumento.NoNulo())
                tx = string.Concat(tx, NoTipoDocumentoTributario, " ", NuDocumento, " de fecha ", FeDocumento.FechaHora(), ". ");

            if (TxDescripcion.NoNulo()) tx = string.Concat(tx, TxDescripcion);
            return tx;
        }
    }
}