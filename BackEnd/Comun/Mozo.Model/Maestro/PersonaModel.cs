using Mozo.Model.Seguridad;

namespace Mozo.Model.Maestro;

public record PersonaFilter : BaseFilterDto { }


[Serializable]
public partial class PersonaModel : BaseModel //<PersonaModel>
{
    public PersonaModel()
    {
    }

    public PersonaModel(int? coModulo, int? coPersonaTipo)
    {
        CoModulo = coModulo;
        CoPersonaTipo = coPersonaTipo;

    }
    public int? CoPersona { get; set; }
    public int? CoDocumentoIdentidad { get; set; }
    public string? NuDocumento { get; set; } = null!;
    public string? NoPersona { get; set; } = null!;
    public string? NoApellidoP { get; set; }
    public string? NoApellidoM { get; set; }
    public string? NoDireccion { get; set; }
    public string? NoCodigoPostal { get; set; }
    public int? CoEstadoCivil { get; set; }
    public string? NoReferencia { get; set; }
    public string? TxDescripcion { get; set; }
    public int? CoProfesion { get; set; }
    public int? CoSexo { get; set; }
    public int? CoRubro { get; set; }
    public int? CoPais { get; set; }
    public string? FeNacimiento { get; set; }

}

public partial class PersonaModel
{


    #region Field join table primary
    public string? NoNombreCompleto => NoPersona != null ? string.Concat(NoPersona, " ", NoApellidoP, " ", NoApellidoM) : null;

    public string? NoProfesion { get; set; }
    public string? NoEstadoCivil { get; set; }
    public string? NoRubro { get; set; }
    public string? NoEmpresa { get; set; }
    public string? NoEmpresaCorto { get; set; }
    public int? CoPersonaTipo { get; set; }
    public string? NoPersonaTipo { get; set; }

    #endregion
    #region Field execption


    #endregion

    public PermisoModel? Permiso { get; set; }
    public IngresoModel? Ingreso { get; set; }
    public int? CoModulo { get; set; }
    public IEnumerable<RedSocialModel>? RedSocialLst { get; set; }
    public List<PersonaTipoModel>? PersonaTipoLst { get; set; }
    //*Begin: Seguridad*//
    public int? CoIngreso { get; set; }
    public bool FlSimpleView { get; set; }
    public bool FlCreateItem { get; set; }

    //*End: Seguridad*//



    public string? NoTelefonoMovil { get; set; }
    public string? NoTelefonoFijo { get; set; }
    public string? NoCorreoElectronico { get; set; }

}