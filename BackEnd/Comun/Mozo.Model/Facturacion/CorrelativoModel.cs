///<summary>
///
///</summary>
///<remarks>
///</remarks>
///<history>
/// t[Jonatan Abregu]	21/09/2021	Created
///</history>
namespace Mozo.Model.Facturacion;

public record CorrelativoFilter : BaseFilterDto { }
[Serializable]
public partial class CorrelativoModel : BaseModel //<CorrelativoModel>
{
    public int? CoCorrelativo { get; set; }
    public int? CoLocal { get; set; }
    public int? CoComprobantePago { get; set; }

    public string? NuSerie { get; set; }
    public int? NuDocumento { get; set; }
    public int? FlDefecto { get; set; }
}

public partial class CorrelativoModel
{
    public string? NoLocal { get; set; }
    public string? NoComprobantePago { get; set; }
    public string? NoComprobantePagoSigla { get; set; }
}