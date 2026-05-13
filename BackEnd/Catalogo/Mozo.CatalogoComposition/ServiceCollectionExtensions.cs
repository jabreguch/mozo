using Microsoft.Extensions.DependencyInjection;

using Mozo.CatalogoBusiness;
using Mozo.CatalogoData;

using Npgsql;

using System.Data;
namespace Mozo.CatalogoComposition;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection CatalogoComposition(this IServiceCollection services, string connectionString)
    {
        services.AddScoped<IDbConnection>(_ => new NpgsqlConnection(connectionString));
        // Registrar DAL y BL
        services.AddScoped<IAtributoData, AtributoData>();
        services.AddScoped<IAtributoBusiness, AtributoBusiness>();

        services.AddScoped<IProductoAtributoData, ProductoAtributoData>();
        services.AddScoped<IProductoAtributoBusiness, ProductoAtributoBusiness>();

        services.AddScoped<IProductoData, ProductoData>();
        services.AddScoped<IProductoBusiness, ProductoBusiness>();

        services.AddScoped<IProductoImpuestoData, ProductoImpuestoData>();
        services.AddScoped<IProductoImpuestoBusiness, ProductoImpuestoBusiness>();

        services.AddScoped<IProductoPrecioData, ProductoPrecioData>();
        services.AddScoped<IProductoPrecioBusiness, ProductoPrecioBusiness>();

        services.AddScoped<IProductoStockData, ProductoStockData>();
        services.AddScoped<IProductoStockBusiness, ProductoStockBusiness>();

        return services;
    }
}

