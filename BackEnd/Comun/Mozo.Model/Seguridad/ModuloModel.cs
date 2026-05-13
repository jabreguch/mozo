namespace Mozo.Model.Seguridad;

public record ModuloFilterDto : BaseFilterDto //ModuloFilter>
{
    public int? CoModulo { get; set; }
}

[Serializable]
public partial class ModuloModel : BaseModel //<ModuloModel>
{
    public string? NoIcono { get; set; }
    public int? NuOrden { get; set; }
    public int? FlArea { get; set; }
    public int? CoModulo { get; set; }
    public string? NoArea { get; set; }
    public string? NoModulo { get; set; }
    public string? NoModuloDescripcion { get; set; }
}
public partial class ModuloModel
{
    public int? CoEmpresa { get; set; }
    public IEnumerable<PerfilModel>? PerfilCol { get; set; }

}