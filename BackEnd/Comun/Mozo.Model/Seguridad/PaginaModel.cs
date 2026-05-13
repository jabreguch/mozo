using System.Text.Json.Serialization;

namespace Mozo.Model.Seguridad;

public record PaginaFilterDto : BaseFilterDto
{
    public int? CoModulo { get; set; }
    public int? CoTipoPagina { get; set; }
    public int? CoPersona { get; set; }
    public int? CoPagina { get; set; }


}

[Serializable]
public partial class PaginaModel : BaseModel //<PaginaModel>
{
    public int? CoModulo { get; set; }
    public int? CoTipoPagina { get; set; }
    public int? CoPagina { get; set; }
    public int? CoMenu { get; set; }    
    public int? CoArea { get; set; }
    public string? NoControlador { get; set; }
    public string? NoAccion { get; set; }
    public string? NoOpcion { get; set; }
    public int? NuOrden { get; set; }    
}
public partial class PaginaModel
{
    [JsonIgnore]
    public string? NoPathPage
    {
        get
        {
            if (NoControlador != null)
                return string.Concat(NoArea, "/", NoControlador, "/", NoAccion);
            else return string.Empty;
        }
    }

 

    public int? CoPersona { get; set; }
    public string? NoArea { get; set; }
    public int? CoPerfil { get; set; }
    public string? NoModuloDescripcion { get; set; }
    public string? NoMenu { get; set; }
}