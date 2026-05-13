using Microsoft.Extensions.Configuration;

using Mozo.Helper.Enu;

using System.Runtime.Caching;

using CacheItemPriority = System.Runtime.Caching.CacheItemPriority;
using MemoryCache = System.Runtime.Caching.MemoryCache;

namespace Mozo.HelperWeb.Api
{
    public static class Register
    {
        public static void Connection(IConfiguration configuration)
        {

            ObjectCache cacheName = MemoryCache.Default;
            CacheItemPolicy policy = new CacheItemPolicy();
            // Indicamos la prioridad de la politica.
            policy.Priority = CacheItemPriority.Default;
            policy.AbsoluteExpiration = DateTimeOffset.Now.AddHours(1);
            cacheName.Set(EnuCommon.BdDefault.Name, configuration.GetSection("ConnectionStrings").GetSection(EnuCommon.BdDefault.Name).Value, policy);
            //return services;
        }
    }
}
