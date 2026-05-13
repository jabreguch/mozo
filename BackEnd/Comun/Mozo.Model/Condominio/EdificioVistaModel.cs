namespace Mozo.Model.Condominio;

public record EdificioVistaFilter : BaseFilterDto //EdificioVistaFilter>
{

}


[Serializable]
public class EdificioVistaModel //: IVista
{
    public int? TieneMontoPorDefecto { get; set; }
    public int? TieneLecturaInicial { get; set; }
}