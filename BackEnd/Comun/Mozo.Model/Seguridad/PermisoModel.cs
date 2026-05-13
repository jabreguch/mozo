namespace Mozo.Model.Seguridad;

public record PermisoFilterDto : BaseFilterDto //PermisoFilter>
{
    public int? CoEmpresa { get; set; }
    public int? CoPermiso { get; set; }
    public string? NoIp { get; set; }
    public string? NoUsuario { get; set; }
    public string? NoClave { get; set; }
}

[Serializable]
public partial class PermisoModel : BaseModel
{
    public int? CoPermiso { get; set; }
    public int? CoPersona { get; set; }
    public string? NoUsuario { get; set; }
    public string? NoClave { get; set; }
    public string? CoLang { get; set; }
}


public partial class PermisoModel
{
    public int? CoEmpresa { get; set; }
    public string? NoNombreCompleto { get; set; }
    //public int? CoModulo { get; set; }
    //public int? CoLocal { get; set; }
    //public string? FeExpiracion { get; set; }
    //public string? NuDocumento { get; set; }
    //public ModuloUsuarioModel? ModuloUsuario { get; set; }
    //public List<ModuloUsuarioModel>? ModuloUsuarioLst { get; set; }
    //public int FlIngreso { get; set; } = 0;
    //public string? NoIp { get; set; }
}


