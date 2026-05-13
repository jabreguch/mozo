namespace Mozo.Model.Maestro;

public record RedSocialFilterDto : BaseFilterDto
{
    public int? CoEmpresa { get; set; }
    public int? FlPersona { get; set; }
    public int? CoRedSocial { get; set; }
    public int? CoTipoRedSocial { get; set; }
    public int? CoPersona { get; set; }



}
public partial class RedSocialModel : BaseModel //<RedSocialModel>
{
    public int? CoEmpresa { get; set; }
    public int? CoRedSocial { get; set; }
    public int? CoEtiqueta { get; set; }
    public int? CoTipoUrl { get; set; }
    public int? CoTipoRedSocial { get; set; }
    public string? NoRedSocial { get; set; }
    public int? CoPersona { get; set; }
    public int? FlPersona { get; set; }
    public int? FlWhatsapp { get; set; }

}

public partial class RedSocialModel
{
    public bool? FlWhatsapp2 { get; set; }
    public string? NoEtiqueta { get; set; }
    public string? NoTipoRedSocial { get; set; }
    public string? NoTipoUrl { get; set; }

    public List<TipoGeneralModel>? EtiquetaLst { get; set; }
    public List<TipoGeneralModel>? TipoUrlLst { get; set; }



}