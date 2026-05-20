using Microsoft.AspNetCore.OutputCaching;

namespace Mozo.ApiSeguridad.Helper;

/// <summary>
/// Clase utilitaria de End Points
/// </summary>
public static class EndpointExtensions
{
    // Prefijos centralizados para fácil mantenimiento
    private static readonly string[] NamespacePrefixes =
    {
        "Mozo.ApiSeguridad.",
        "Mozo.ApiMaestro.",
        "Mozo.ApiLogin.",
        "Mozo.Api."
    };

    /// <summary>
    /// Para clases estáticas: recibe el <see cref="Type"/> explícito.
    /// </summary>
    public static RouteGroupBuilder MapWithAutoTag(
        this IEndpointRouteBuilder app,
        string prefix,
        Type endpointType,
        Action<RouteGroupBuilder> mapAction)
    {
        ArgumentNullException.ThrowIfNull(endpointType);

        string cleanNamespace = RemoveNamespacePrefixes(endpointType.Namespace ?? "General");
        string className = endpointType.Name.Replace("EndPoints", "", StringComparison.Ordinal);
        string tag = $"{cleanNamespace} - {className}";

        RouteGroupBuilder group = app.MapGroup(prefix).WithTags(tag);
        mapAction(group);
        return group;
    }

    /// <summary>
    /// Genera un tag a partir del namespace y nombre de clase del tipo especificado.
    /// </summary>
    public static string FromNamespaceAndClass(Type type)
    {
        ArgumentNullException.ThrowIfNull(type);

        string cleanNamespace = RemoveNamespacePrefixes(type.Namespace ?? "General");
        string formattedNamespace = cleanNamespace.Replace(".", " / ", StringComparison.Ordinal);

        return $"{formattedNamespace} - {type.Name}";
    }

    /// <summary>
    /// Configuración estándar de respuestas para tipos referencia (200/201/204, 400, 401)
    /// </summary>
    public static RouteHandlerBuilder WithResponses<T>(
        this RouteHandlerBuilder builder,
        int successCode) where T : class
    {
        return builder
            .Produces<T>(successCode)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status401Unauthorized);
    }

    /// <summary>
    /// Configuración estándar de respuestas para tipos valor (200/201, 400, 401)
    /// </summary>
    public static RouteHandlerBuilder WithResponsesValue<T>(
        this RouteHandlerBuilder builder,
        int successCode) where T : struct
    {
        return builder
            .Produces<T>(successCode)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status401Unauthorized);
    }

    /// <summary>
    /// Configuración estándar para respuestas sin contenido (204, 400, 401)
    /// </summary>
    public static RouteHandlerBuilder WithResponses(
        this RouteHandlerBuilder builder,
        int successCode)
    {
        return builder
            .Produces(successCode)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status401Unauthorized);
    }

    /// <summary>
    /// Configuración estándar para grupo de rutas con seguridad
    /// </summary>
    public static RouteGroupBuilder WithSecurity(this RouteGroupBuilder group)
    {
        return group
            .DisableAntiforgery()
            .RequireAuthorization();
    }

    /// <summary>
    /// Remueve los prefijos conocidos del namespace
    /// </summary>
    private static string RemoveNamespacePrefixes(string namespaceName)
    {
        foreach (string prefix in NamespacePrefixes)
        {
            if (namespaceName.StartsWith(prefix, StringComparison.Ordinal))
            {
                return namespaceName[prefix.Length..];
            }
        }
        return namespaceName;
    }

    /// <summary>
    /// Remueve el cache
    /// </summary>
    public static async Task InvalidateCacheAsync(this IOutputCacheStore cacheStore, string CacheTag)
    {
        await cacheStore.EvictByTagAsync(CacheTag, default);
    }
}