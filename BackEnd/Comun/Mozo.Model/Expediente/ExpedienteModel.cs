///<summary>
///
///</summary>
///<remarks>
///</remarks>
///<history>
/// t[Jonatan Abregu]	10-04-2020	Created
///</history>
namespace Mozo.Model.Expediente;

public record ExpedienteFilter : BaseFilterDto { }

[Serializable]
public class ExpedienteModel : BaseModel //<ExpedienteModel>
{


    public int? CoTabExpediente { get; set; }

    public string? NoResponsable { get; set; }
    public string? NoMateria { get; set; }
    public string? NoSubMateria { get; set; }
    public string? NoEstado { get; set; }



    public int? CoExpediente { get; set; }



    public int? CoMateria { get; set; }



    public int? CoSubMateria { get; set; }



    public int? NuExpediente { get; set; }



    public int? NuAnno { get; set; }

    public string? FeExpediente { get; set; }
    public string? TxDescripcion { get; set; }



    public int? CoEstado { get; set; }




    public int? CoArea { get; set; }



    public int? CoResponsable { get; set; }



    public int? FlVisualiza { get; set; }

    public string? NoArea { get; set; }
    public string? NoAreaSigla { get; set; }
}