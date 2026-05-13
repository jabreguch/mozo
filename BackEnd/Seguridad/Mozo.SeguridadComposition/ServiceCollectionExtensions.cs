using Microsoft.Extensions.DependencyInjection;

using Mozo.SeguridadBusiness;
using Mozo.SeguridadData;

using Npgsql;

using System.Data;

namespace Mozo.SeguridadComposition;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection SeguridadComposition(this IServiceCollection services, string connectionString)
    {
        services.AddScoped<IDbConnection>(_ => new NpgsqlConnection(connectionString));
        // Registrar DAL y BL
        services.AddScoped<IEmpresaData, EmpresaData>();
        services.AddScoped<IEmpresaBusiness, EmpresaBusiness>();

        services.AddScoped<IEmpresaModuloData, EmpresaModuloData>();
        services.AddScoped<IEmpresaModuloBusiness, EmpresaModuloBusiness>();

        services.AddScoped<IMenuData, MenuData>();
        services.AddScoped<IMenuBusiness, MenuBusiness>();

        services.AddScoped<IModuloData, ModuloData>();
        services.AddScoped<IModuloBusiness, ModuloBusiness>();

        services.AddScoped<IModuloUsuarioData, ModuloUsuarioData>();
        services.AddScoped<IModuloUsuarioBusiness, ModuloUsuarioBusiness>();

        services.AddScoped<IPaginaData, PaginaData>();
        services.AddScoped<IPaginaBusiness, PaginaBusiness>();

        services.AddScoped<IPerfilData, PerfilData>();
        services.AddScoped<IPerfilBusiness, PerfilBusiness>();

        services.AddScoped<IPerfilPaginaData, PerfilPaginaData>();
        services.AddScoped<IPerfilPaginaBusiness, PerfilPaginaBusiness>();

        services.AddScoped<IPermisoData, PermisoData>();
        services.AddScoped<IPermisoBusiness, PermisoBusiness>();

        return services;
    }
}

