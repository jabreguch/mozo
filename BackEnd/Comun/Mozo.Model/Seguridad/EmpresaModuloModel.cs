namespace Mozo.Model.Seguridad;

public record EmpresaModuloFilterDto : BaseFilterDto //EmpresaModuloFilter>
{
    public int? CoEmpresa { get; set; }
    public int? CoEmpresaModulo { get; set; }
    public int? CoModulo { get; set; }

}
[Serializable]
public partial class EmpresaModuloModel : BaseModel //<EmpresaModuloModel>
{
    public int? CoEmpresaModulo { get; set; }
    public int? CoModulo { get; set; }
    public int? CoEmpresa { get; set; }
}
public partial class EmpresaModuloModel
{
    public int? NuOrden { get; set; }
    public string? NoModulo { get; set; }
    public string? NoModuloDescripcion { get; set; }
}