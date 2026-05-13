namespace Mozo.Model.Seguridad;

public record PerfilFilterDto : BaseFilterDto
{
    public int? CoModulo { get; set; }
    public int? CoPerfil { get; set; }

}

[Serializable]
public partial class PerfilModel : BaseModel //<PerfilModel>
{
    public int? CoPerfil { get; set; }
    public string? NoPerfil { get; set; }
    public int? CoModulo { get; set; }

    private int? flDefault;

    public int? FlDefault
    {
        get => flDefault ?? 0;
        set
        {
            flDefault = value;
        }
    }
    public bool? FlDefault2 { get; set; }

}

public partial class PerfilModel
{
    public string? NoModuloDescripcion { get; set; }
    public List<PerfilPaginaModel>? PerfilPaginaLst { get; set; }
    public List<MenuModel>? MenuLst { get; set; }
}