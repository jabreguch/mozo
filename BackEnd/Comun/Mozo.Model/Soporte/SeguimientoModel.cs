using Mozo.Model.Maestro;

namespace Mozo.Model.Soporte;

public record SeguimientoFilter : BaseFilterDto { }
[Serializable]
public class SeguimientoModel : BaseModel //<SeguimientoModel>
{


    public int? CoSeguimiento { get; set; }



    public int? CoSoporte { get; set; }

    public string? TxSeguimiento { get; set; }
    public int? NuDuracion { get; set; }
    public int? CoTipoTiempo { get; set; }
    public string? TxSoporte { get; set; }
    public string? FeIngreso { get; set; }
    public string? HoIngreso { get; set; }
    public string? NoDuracion { get; set; }
    public ArchivoModel? Archivo { get; set; }
}