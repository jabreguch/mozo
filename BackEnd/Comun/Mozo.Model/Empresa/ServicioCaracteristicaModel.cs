///<summary>
///
///</summary>
///<remarks>
///</remarks>
///<history>
/// t[Jonatan Abregu]	26/09/2022	Created
///</history>
namespace Mozo.Model.Empresa;

public record ServicioCaracteristicaFilter : BaseFilterDto { }

[Serializable()]
public partial class ServicioCaracteristicaModel : BaseModel //<ServicioCaracteristicaModel>
{

    public int? CoServicioCaracteristica { get; set; }
    public int? CoServicio { get; set; }
    public int? NuOrden { get; set; }
    public string? NoTitulo { get; set; }
    public string? TxDescripcion { get; set; }

}

public partial class ServicioCaracteristicaModel
{


}