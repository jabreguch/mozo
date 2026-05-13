using Microsoft.Extensions.DependencyInjection;

using Mozo.MaestroBusiness;
using Mozo.MaestroData;

using Npgsql;

using System.Data;
namespace Mozo.MaestroComposition;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection MaestroComposition(this IServiceCollection services, string connectionString)
    {
        services.AddScoped<IDbConnection>(_ => new NpgsqlConnection(connectionString));
        // Registrar DAL y BL
        services.AddScoped<IArchivoData, ArchivoData>();
        services.AddScoped<IArchivoBusiness, ArchivoBusiness>();

        services.AddScoped<IPersonaData, PersonaData>();
        services.AddScoped<IPersonaBusiness, PersonaBusiness>();

        services.AddScoped<IPersonaTipoData, PersonaTipoData>();
        services.AddScoped<IPersonaTipoBusiness, PersonaTipoBusiness>();

        services.AddScoped<IRedSocialData, RedSocialData>();
        services.AddScoped<IRedSocialBusiness, RedSocialBusiness>();

        services.AddScoped<ITipoParticularData, TipoParticularData>();
        services.AddScoped<ITipoParticularBusiness, TipoParticularBusiness>();

        services.AddScoped<ITipoGeneralData, TipoGeneralData>();
        services.AddScoped<ITipoGeneralBusiness, TipoGeneralBusiness>();

        services.AddScoped<IPaisData, PaisData>();
        services.AddScoped<IPaisBusiness, PaisBusiness>();

        services.AddScoped<IDocumentoIdentidadData, DocumentoIdentidadData>();
        services.AddScoped<IDocumentoIdentidadBusiness, DocumentoIdentidadBusiness>();

        //services.AddScoped<IUbigeoData, UbigeoData>();
        //services.AddScoped<IUbigeoBusiness, UbigeoBusiness>();

        return services;
    }
}

