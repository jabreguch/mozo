namespace Mozo.ApiSeguridad.Helper;

/// <summary>
/// Clase utilitaria de End Points
/// </summary>
public static class EndpointExtensions
{

    /// <summary>
    /// Para clases estáticas: recibe el <see cref="Type"/> explícito.
    /// </summary>
    public static RouteGroupBuilder MapWithAutoTag(
        this IEndpointRouteBuilder app,
        string prefix,
        Type endpointType,
        Action<RouteGroupBuilder> mapAction)
    {
        if (endpointType == null) throw new ArgumentNullException(nameof(endpointType));

        string ns = endpointType.Namespace!;
        // Quitar prefijos del namespace
        if (ns.StartsWith("Mozo.Api."))
            ns = ns["Mozo.Api.".Length..];

        string tag = $"{ns} - {endpointType.Name.Replace("EndPoints", "")}";
        RouteGroupBuilder g = app.MapGroup(prefix).WithTags(tag);
        mapAction(g);
        return g;
    }

    /// <summary>
    /// Para clases estáticas: recibe el <see cref="Type"/> explícito.
    /// </summary>
    public static string FromNamespaceAndClass(Type t)
    {
        string ns = t.Namespace ?? "General";

        // Quitar prefijos del namespace
        if (ns.StartsWith("Mozo.ApiSeguridad."))
            ns = ns["Mozo.ApiSeguridad.".Length..];
        else if (ns.StartsWith("Mozo.ApiMaestro."))
            ns = ns["Mozo.ApiMaestro.".Length..];
        else if (ns.StartsWith("Mozo.ApiLogin."))
            ns = ns["Mozo.ApiLogin.".Length..];

        //

        // Tomar también el nombre de la clase
        string className = t.Name;//.Replace("Endpoints", "");

        return $"{ns.Replace(".", " / ")} - {className}";
    }

    /// <summary>
    /// Configuración estándar de respuestas para tipos referencia (400, 401)
    /// </summary>
    public static RouteHandlerBuilder WithResponses<T>(
        this RouteHandlerBuilder builder,
        int successCode) where T : class
    {
        builder.Produces<T>(successCode)
            .Produces(StatusCodes.Status400BadRequest);
        // .Produces(StatusCodes.Status401Unauthorized);
        return builder;
    }

    /// <summary>
    /// Configuración estándar de respuestas para tipos valor como int (400, 401)
    /// </summary>
    public static RouteHandlerBuilder WithResponsesValue<T>(
        this RouteHandlerBuilder builder,
        int successCode) where T : struct
    {
        builder.Produces<T>(successCode)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status401Unauthorized);
        return builder;
    }

    /// <summary>
    /// Configuración estándar para respuestas sin contenido (204, 400, 401)
    /// </summary>
    public static RouteHandlerBuilder WithResponses(
        this RouteHandlerBuilder builder,
        int successCode)
    {
        builder.Produces(successCode)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status401Unauthorized);
        return builder;
    }

    /// <summary>
    /// Configuración estándar para grupo de rutas
    /// </summary>
    public static RouteGroupBuilder WithSecurity(
        this RouteGroupBuilder group)
    {
        return group.DisableAntiforgery().RequireAuthorization();
    }


}
