using Microsoft.AspNetCore.Mvc;

using Mozo.ApiSeguridad.Helper;
using Mozo.ApiSeguridad.HelperWeb.Exceptions;
using Mozo.HelperWeb.Token;
using Mozo.LoginBusiness;
using Mozo.Model.Seguridad;
using Mozo.Model.Seguridad.Auth;

using System.Security.Claims;
namespace Mozo.Api.Login;

///<summary>
/// Endpoints para gestión de Permiso
///</summary>
///<history>
/// Create by Jonatan Abregu
///</history>
public static partial class PermisoEndPoints
{
    /// <summary>
    /// Mapea todas las rutas de Permiso
    /// </summary>
    public static RouteGroupBuilder MapPermiso(this RouteGroupBuilder g)
    {
        g.WithSecurity();

        g.MapPost("/login", LoginAsync)
            .AllowAnonymous()
            .WithResponses<GlobalCredencialModel>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound)
            .WithDescription("Realiza el login del usuario");

        g.MapGet("/login-check", LoginCheck)
            .WithResponses<GlobalCredencialModel>(StatusCodes.Status200OK)
            .WithDescription("Verifica si el login es válido");

        g.MapPost("/login-renew", LoginRenew)
            .WithResponses<GlobalCredencialModel>(StatusCodes.Status200OK)
            .WithDescription("Renueva el token");

        g.MapPatch("/language", UpdateLanguageAsync)
            .WithResponses<GlobalCredencialModel>(StatusCodes.Status200OK)
            .WithDescription("Actualiza el idioma del app");

        return g;
    }
}
public static partial class PermisoEndPoints
{
    private static async Task<IResult>
        LoginAsync(
            [FromBody] PermisoFilterDto f,
            IConfiguration configuration,
            IPermisoBusiness IPermiso,
            IIngresoBusiness IIngreso
        )
    {

        if (f == null)
            throw new ArgumentNullException(nameof(f));

        if (string.IsNullOrWhiteSpace(f.NoUsuario))
            throw new ValidationException("NoUsuario", "El usuario es requerido");

        if (string.IsNullOrWhiteSpace(f.NoClave))
            throw new ValidationException("NoClave", "La contraseña es requerida");


        GlobalCredencialModel globalCredencial = new();

        string? noTokenRefresh;

        PermisoModel? permiso = await IPermiso.SelByUserAsync(f);
        if (permiso == null)
            throw new NotFoundException("Usuario o contraseña incorrectos");


        noTokenRefresh = UtilityJwt.GenerateRefreshToken();

        IngresoModel? ingreso = new()
        {
            CoEmpresa = permiso.CoEmpresa,
            CoPersona = permiso.CoPersona,
            CoPermiso = permiso.CoPermiso,
            NoRefreshToken = noTokenRefresh,
            NoIp = f.NoIp,
        };
        ingreso.CoIngreso = await IIngreso.InsertAsync(ingreso);
        noTokenRefresh = string.Concat(ingreso.CoEmpresa, "-", noTokenRefresh, "-", ingreso.CoIngreso);

        CredencialModel credential = new()
        {
            CoEmpresa = permiso.CoEmpresa,
            CoPersona = permiso.CoPersona,
            CoPermiso = permiso.CoPermiso,
            NoUsuario = permiso.NoUsuario,
            NoNombreCompleto = permiso.NoNombreCompleto,
            CoIngreso = ingreso.CoIngreso
        };

        string token = UtilityJwt.GenerateToken(credential, configuration);
        //credential.FeExpiration = r.Item2;

        globalCredencial.Credencial = credential;
        globalCredencial.NoToken = token;
        globalCredencial.NoTokenRefresh = noTokenRefresh;

        return Results.Ok(globalCredencial);
    }

    private static async Task<IResult>
        LoginRenew(
            [FromBody] IngresoFilterDto f,
            IConfiguration configuration,
            IPermisoBusiness IPermiso,
            IIngresoBusiness IIngreso
        )
    {
        if (f == null || f.NoRefreshToken == null)
            throw new ArgumentNullException(nameof(f.NoRefreshToken), "Falta el token refresh");

        string[] tokenRefresh = f.NoRefreshToken.Split("-");

        if (tokenRefresh.Length != 3)
            throw new ValidationException("NoRefreshToken", "Formato de token refresh inválido");

        int? coEmpresa = int.Parse(tokenRefresh[0]);
        int? coIngreso = int.Parse(tokenRefresh[2]);

        IngresoModel? ingreso = await IIngreso.SelByIdAsync(new() { CoIngreso = coIngreso });
        if (ingreso == null)
            throw new UnauthorizedException("Token refresh no válido");

        await IIngreso.UpdateCloseTokenAsync(new() { CoEmpresa = ingreso.CoEmpresa, CoPermiso = ingreso.CoPermiso, NoRefreshTokenPrevious = f.NoRefreshToken });

        GlobalCredencialModel globalCredencial = new();

        string? noTokenRefresh;

        PermisoModel? permiso = await IPermiso.SelByIdAsync(new() { CoEmpresa = ingreso.CoEmpresa, CoPermiso = ingreso.CoPermiso });

        if (permiso == null)
            throw new NotFoundException("Usuario no encontrado");


        noTokenRefresh = UtilityJwt.GenerateRefreshToken();
        noTokenRefresh = string.Concat(ingreso.CoEmpresa, "-", noTokenRefresh, "-", ingreso.CoIngreso);

        ingreso = new()
        {
            CoEmpresa = permiso.CoEmpresa,
            CoPersona = permiso.CoPersona,
            CoPermiso = permiso.CoPermiso,
            NoRefreshToken = noTokenRefresh,
            NoIp = ingreso.NoIp,
        };
        ingreso.CoIngreso = await IIngreso.InsertAsync(ingreso);


        CredencialModel credential = new()
        {
            CoEmpresa = permiso.CoEmpresa,
            CoPersona = permiso.CoPersona,
            CoPermiso = permiso.CoPermiso,
            NoUsuario = permiso.NoUsuario,
            NoNombreCompleto = permiso.NoNombreCompleto,
            CoIngreso = ingreso.CoIngreso
        };

        string token = UtilityJwt.GenerateToken(credential, configuration);

        globalCredencial.Credencial = credential;
        globalCredencial.NoToken = token;
        globalCredencial.NoTokenRefresh = noTokenRefresh;

        return Results.Ok(globalCredencial);
    }


    private static async Task<IResult> LoginCheck(
        ClaimsPrincipal user, HttpContext httpContext
       )
    {
        GlobalCredencialModel globalCredencial = new();
        //ClaimsPrincipal user = httpContext.User;

        if (!user.Identity?.IsAuthenticated ?? true)
            throw new UnauthorizedException("No autorizado");

        var coEmpresa = user.Claims.FirstOrDefault(c => c.Type == "CoEmpresa")?.Value;
        var coPersona = user.Claims.FirstOrDefault(c => c.Type == "CoPersona")?.Value;
        var coPermiso = user.Claims.FirstOrDefault(c => c.Type == "CoPermiso")?.Value;
        var coIngreso = user.Claims.FirstOrDefault(c => c.Type == "CoIngreso")?.Value;
        var noUsuario = user.Claims.FirstOrDefault(c => c.Type == "NoUsuario")?.Value;

        if (string.IsNullOrEmpty(coEmpresa) || string.IsNullOrEmpty(coPersona) ||
          string.IsNullOrEmpty(coPermiso))
            throw new UnauthorizedException("Claims inválidos");

        string token = httpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");



        CredencialModel credential = new()
        {
            CoEmpresa = int.Parse(coEmpresa!),
            CoPersona = int.Parse(coPersona!),
            CoPermiso = int.Parse(coPermiso!),
            NoUsuario = noUsuario,
            CoIngreso = int.Parse(coIngreso!)
        };

        globalCredencial.Credencial = credential;
        globalCredencial.NoToken = token;


        return Results.Ok(globalCredencial);
    }

    static async Task<IResult>
        UpdateLanguageAsync(
        PermisoModel m,
        IPermisoBusiness IPermiso,
        UserClaims user
        )
    {
        if (m is null)
            throw new ArgumentNullException(nameof(m));

        await IPermiso.UpdateLanguageAsync(m);
        return Results.Ok(m);
    }


}


