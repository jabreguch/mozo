using Mozo.Model.Seguridad;

///<summary>
///
///</summary>
///<remarks>
///</remarks>
///<history>
/// t[Jonatan Abregu]	16/11/2021	Created
///</history>
namespace Mozo.Model.Urbano;

public record EtapaFilter : BaseFilterDto { }
[Serializable]
public partial class EtapaModel : BaseModel //<EtapaModel>
{
    public int? CoProyecto { get; set; }

    public int? CoEtapa { get; set; }

    public string? NoEtapa { get; set; }

    public decimal? NuArea { get; set; }
    public decimal? NuTEA { get; set; }

    public decimal? NuTEM { get; set; }
    public string? NoLimiteNorte { get; set; }
    public string? NoLimiteEste { get; set; }
    public string? NoLimiteSur { get; set; }
    public string? NoLimiteOeste { get; set; }

    public decimal? SsBono { get; set; }
}

public partial class EtapaModel
{
    public ProyectoModel? Proyecto { get; set; }
    public EmpresaModel? Empresa { get; set; }
}