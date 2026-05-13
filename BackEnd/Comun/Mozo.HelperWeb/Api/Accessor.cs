using Microsoft.Extensions.Configuration;

namespace Mozo.Comun.Implement.Api;

public static class Accessor
{
    public static IConfiguration Configuration;
    public static int? CoEmpresa { set; get; }
    public static int? CoPermiso { set; get; }

    public static int? CoPersona { set; get; }
    //public static string NoUsuario { set; get; }
    //public static int? CoIngreso { set; get; }
    //public static long? FeUtcIssued { set; get; }
    //public static long? FeUtcExpiration { set; get; }
}