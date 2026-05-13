namespace Mozo.Model.Maestro;

public class TipoSearchModel : ControlSearchBaseModel
{
    public TipoSearchModel(int? coEmpresa, int? coModulo, int? coGrupo)
    {
        CoEmpresa = coEmpresa;
        CoModulo = coModulo;
        CoGrupo = coGrupo;
    }

    public TipoSearchModel()
    {
    }

    public bool FlLoadChildren { get; set; } = false;
    public bool FlSiglaView { get; set; } = false;


    public string? Title { get; set; }
    public int? CoModulo { get; set; }
    public int? CoGrupo { get; set; }
}