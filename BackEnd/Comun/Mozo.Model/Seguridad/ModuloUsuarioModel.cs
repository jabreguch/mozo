namespace Mozo.Model.Seguridad;

public record ModuloUsuarioFilterDto : BaseFilterDto //ModuloUsuarioFilter>
{

    public int? CoEmpresa { get; set; }
    public int? CoModulo { get; set; }
    public int? CoPersona { get; set; }
    public int? CoModuloUsuario { get; set; }

    //       p_coempresa integer,
    //   p_comodulo integer,
    //p_copersona integer)
}
[Serializable]
public partial class ModuloUsuarioModel : BaseModel //<ModuloUsuarioModel>
{
    public int? CoEmpresa { get; set; }
    public int? CoModuloUsuario { get; set; }
    public int? CoModulo { get; set; }
    public int? CoPersona { get; set; }
    public int? CoPerfil { get; set; }
    public string? FeExpiracion { get; set; }
    public string? TxConfiguracion { get; set; }
}

public partial class ModuloUsuarioModel
{
    public string? NoModulo { get; set; }
    public string? NoPerfil { get; set; }
    public string? NoArea { get; set; }
    public int? NuOrden { get; set; }
    public string? NoCarpetaVirtual { get; set; }
    public List<MenuModel>? MenuLst { get; set; }
    public string? NoIcono { get; set; }

}