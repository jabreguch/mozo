namespace Mozo.Model.Condominio;

public record EdificioProcesoFilter : BaseFilterDto //EdificioProcesoFilter>
{

}

[Serializable]
public class EdificioProcesoModel
{
    public int? EditaMonto { get; set; }
    public int? IngresaLecturaActual { get; set; }
}