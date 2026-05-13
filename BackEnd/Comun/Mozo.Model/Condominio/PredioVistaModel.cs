namespace Mozo.Model.Condominio;

public record PredioVistaFilter : BaseFilterDto //PredioVistaFilter>
{

}

[Serializable]
public class PredioVistaModel //: IVista
{

    public int? TieneServicioPadre { get; set; }
    public int? TieneLecturaInicial { get; set; }
    public int? TieneMontoPorDefecto { get; set; }
}