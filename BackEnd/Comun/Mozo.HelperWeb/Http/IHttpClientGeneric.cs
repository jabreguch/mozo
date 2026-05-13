using System.Threading.Tasks;

namespace Mozo.Comun.Implement.Http;

public interface IHttpClientGeneric
{
    Task<T> All<T>(string NoUrl, object Model, int security = 1);

    Task<T> One<T>(string url, object model, int security = 1);

    Task<T> Post<T>(string NoUrl, object Model, int security = 1);
}

public static class Security
{
    public static int Token = 1;

    public static int Anonymous = 2;
    //public static int Security = 2; Anonymous
}