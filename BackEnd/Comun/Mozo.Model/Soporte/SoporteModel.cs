using Mozo.Model.Maestro;

namespace Mozo.Model.Soporte;

public record SoporteFilter : BaseFilterDto { }
[Serializable]
public partial class SoporteModel : BaseModel //<SoporteModel>
{


    public int? CoSoporte { get; set; }



    public int? CoGrupo { get; set; }



    public int? CoTipoSoporte { get; set; }



    public int? CoClasificacion { get; set; }


    public int? CoSubClasificacion { get; set; }

    public int? CoUbicacion { get; set; }
    public int? CoArea { get; set; }



    public int? CoCliente { get; set; }



    public int? CoEspecialista { get; set; }



    public int? CoUsuarioRegistro { get; set; }

    public string? FeInicioTarea { get; set; }
    public string? FeLimite { get; set; }
    public string? FeCierre { get; set; }
    public string? FeCorreoEnviado { get; set; }



    public int? CoTipoTiempo { get; set; }

    public int? NuDuracion { get; set; }


    public string? TxProblema { get; set; }
    public string? TxSoporte { get; set; }
    public string? NoCcp { get; set; }


    public string? NoDocumentoReferencia { get; set; }

    public string? NoUrlCloud { get; set; }
}

public partial class SoporteModel
{
    #region Soporte

    public string? FeCreIni { get; set; }
    public string? FeCreFin { get; set; }
    public List<SeguimientoModel>? SeguimientoLst { get; set; }
    public string? NoGrupo { get; set; }
    public string? NoDuracion { get; set; }
    public string? NoTipoSoporte { get; set; }
    public string? NoClasificacion { get; set; }


    public string? NoSubClasificacion { get; set; }

    public string? NoEstado { get; set; }

    #endregion Soporte

    #region UsuarioRegistro


    public string? NoUsuarioAsignado { get; set; }

    public string? NoCorreoAsignado { get; set; }

    #endregion UsuarioRegistro

    #region UsuarioRegistro


    public string? NoUsuarioRegistro { get; set; }

    public string? NoCorreoRegistro { get; set; }

    #endregion UsuarioRegistro

    #region UsuarioAtendido

    public PersonaModel? UsuAtendido { get; set; }

    #endregion UsuarioAtendido



    #region Estadistica

    public int? QtSoporte { get; set; }



    public int? QtDia { get; set; }



    public int? QtGrupoSoporte { get; set; }



    public int? QtGrupoDesarrollo { get; set; }



    public int? QtGrupoEstadistica { get; set; }



    public int? CoGrupoDesarrollo { get; set; }



    public int? CoGrupoSoporte { get; set; }



    public int? CoGrupoEstadistica { get; set; }

    #endregion Estadistica
}