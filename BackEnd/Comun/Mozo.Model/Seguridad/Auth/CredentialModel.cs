namespace Mozo.Model.Seguridad.Auth;

public class CredencialModel
{
    public string? NoUsuario { get; set; }
    public string? NoNombreCompleto { get; set; }
    public int? CoEmpresa { get; set; }
    public int? CoPersona { get; set; }
    public int? CoPermiso { get; set; }
    public int? CoIngreso { get; set; }
    //public long? FeExpiration { get; set; }

    //public TokenModel? Token { get; set; }
    // public GlobalCredentialEmpresaModel? Empresa { get; set; }
    // public GlobalCredentialPermisoModel? Permiso { get; set; }
    // public GlobalCredentialIngresoModel? Ingreso { get; set; }
}

public class GlobalCredencialModel
{
    public CredencialModel? Credencial { get; set; }
    public string? NoToken { get; set; }
    public string? NoTokenRefresh { get; set; }
}

//[Serializable]
//public class GlobalCredentialIngresoModel
//{
//     public int? CoIngreso { get; set; }
//}

//[Serializable]
//public class GlobalCredentialEmpresaModel
//{
//     public string? NoEmpresa { get; set; }
//     public string? NoEmpresaCorto { get; set; }
//     public string? NoArchivo { get; set; }
//     public string? NoExtension { get; set; }
//}