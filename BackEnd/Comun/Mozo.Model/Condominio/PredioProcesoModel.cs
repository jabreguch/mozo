namespace Mozo.Model.Condominio;

public record PredioProcesoFilter : BaseFilterDto //PredioProcesoFilter>
{

}

[Serializable]
public class PredioProcesoModel
{
    public int? IngresaLecturaActual { get; set; }
    public int? EditaMonto { get; set; }
    public int? RecalculoMontoPorCambioLecturaActual { get; set; }
}