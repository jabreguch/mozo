namespace Mozo.Model.Seguridad;

public record MenuFilterDto : BaseFilterDto //MenuFilter>
{
    public int? CoModulo { get; set; }
    public int? CoPersona { get; set; }
    public int? CoMenu { get; set; }



}
[Serializable]
public partial class MenuModel : BaseModel //<MenuModel>
{
    public int? NuOrden { get; set; }
    public int? CoMenu { get; set; }
    public string? NoMenu { get; set; }
    public int? CoPersona { get; set; }
    public int? CoModulo { get; set; }
    public string? NoModuloDescripcion { get; set; }
    public int? CoPerfil { get; set; }

}
public partial class MenuModel
{
    public List<PaginaModel>? PaginaLst { get; set; }

}