using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Primitives;
using Microsoft.IdentityModel.Tokens;
using Mozo.Comun.Helper.Global;
using Mozo.Comun.Implement.Api;
using Mozo.Model.Maestro;
using Mozo.Model.Seguridad;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Mozo.Comun.Implement;

public class Authentication : JwtBearerEvents
{
    private bool GetPermiso(PersonaModel c, string NoControlador, string NoAccion)
    {
        return true;
        //List<ModuloUsuarioModel> ModuloUsuarioCol = _moduloUsuarioService.GetAllPersona(new ModuloUsuarioModel() { CoEmpresa = c.CoEmpresa, CoPersona = c.CoPersona }).ToList();
        //foreach (ModuloUsuarioModel ModuloUsuario in ModuloUsuarioCol)
        //{
        //    List<ServicioWebModel> ServicioWebCol = _paginaService.GetAllServicioWeb(new ServicioWebModel() { CoEmpresa = ModuloUsuario.CoEmpresa, CoModulo = ModuloUsuario.CoModulo, CoPersona = ModuloUsuario.CoPersona }).ToList();
        //    ServicioWebModel TmpServicioWeb = ServicioWebCol.Find(x => x.NoControlador.Minuscula() == NoControlador && x.NoAccion.Minuscula() == NoAccion);
        //    if (TmpServicioWeb != null)
        //    {
        //        return true;
        //    }
        //}    
        //return false;
    }


    public override Task TokenValidated(TokenValidatedContext context)
    {
        StringValues HeaderValues;

        if (!UtilityJwt.TokenNoExpiro(context.SecurityToken.ValidTo, context.SecurityToken.ValidFrom))
        {
            context.Fail("Token Expiro.");
            context.HttpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
            return Task.CompletedTask;
        }

        var Identidad = context.Principal;

        //context.HttpContext.User = context.HttpContext.User

        var Persona = new PersonaModel();

        var rv = context.Request.RouteValues;
        object NoArea, NoControlador, NoAccion;
        rv.TryGetValue("area", out NoArea);
        rv.TryGetValue("controller", out NoControlador);
        rv.TryGetValue("action", out NoAccion);


        if (GetPermiso(Persona, NoControlador.Minuscula(), NoAccion.Minuscula()) == false)
        {
            context.Fail("El usuario no tiene permiso a esta opción.");
            context.HttpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
            return Task.CompletedTask;
            //throw new SecurityTokenException("El usuario no tiene permiso a esta opción");
        }


        //context.HttpContext.Session.SesionSet(EnuCommon.GLOBAL_NAME_SESSION_CREDENTIAL, Usuario);
        //Glo.CacheSet("Usuario", StrUsuario);


        //context.HttpContext.Items.Add("Usuario", GlobalSession);
        return Task.CompletedTask;
    }

    public override Task Challenge(JwtBearerChallengeContext context)
    {
        //context.Error = "pepe";
        //context.ErrorDescription = context.AuthenticateFailure.Message;
        //context.ErrorUri = "pepeSDADADADuRIIIIII";
        return Task.CompletedTask;
    }


    //public virtual Task AuthenticationFailed(AuthenticationFailedContext context);        
    public override Task Forbidden(ForbiddenContext context)
    {
        return Task.CompletedTask;
    }

    public override Task MessageReceived(MessageReceivedContext context)
    {
        return Task.CompletedTask;
    }
    //public override Task TokenValidated(TokenValidatedContext context);

    public override Task AuthenticationFailed(AuthenticationFailedContext context)
    {
        return Task.CompletedTask;
    }
}

//public class JwtModel
//{
//    public string Key { get; set; }
//    public string Audience { get; set; }
//    public int Duracion { get; set; }

//}
public static class UtilityJwt
{
    private static IList<Claim> GetClaim(PersonaModel c)
    {
        return new List<Claim>
        {
            new("CoEmpresa", c.CoEmpresa.Text()),
            new("CoPermiso", c.Permiso.CoPermiso.Text()),
            new("CoPersona", c.CoPersona.Text())
        };
    }

    public static GlobalCredentialTokenModel GenerateToken(PersonaModel c)
    {
        //Authorization
        var SecretKey = Encoding.ASCII.GetBytes(Accessor.Configuration.GetSection("JwtBearerTokenSettings")
            .GetSection("SecretKey").Value);
        var securityKey = new SymmetricSecurityKey(SecretKey);
        var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claimsIdentity = new ClaimsIdentity(GetClaim(c));
        // create token to the user
        var tokenHandler = new JwtSecurityTokenHandler();
        var NuDuration = double.Parse(Accessor.Configuration.GetSection("JwtBearerTokenSettings")
            .GetSection("ExpiryTimeInMinute").Value);
        var jwtSecurityToken = tokenHandler.CreateJwtSecurityToken(
            //audience: "http://localhost",
            //issuer: "http://localhost",
            subject: claimsIdentity,
            issuedAt: DateTime.UtcNow,
            notBefore: DateTime.UtcNow,
            expires: DateTime.UtcNow.AddMinutes(NuDuration),
            signingCredentials: signingCredentials
        );

        var TokenModel = new GlobalCredentialTokenModel();
        var guid = Guid.NewGuid();


        TokenModel.NoRefresh = guid.ToString().Replace("-", "");
        TokenModel.FeExpiration =
            long.Parse(jwtSecurityToken.Claims.First(claim => claim.Type == "exp").Value); //Fecha de expedición
        TokenModel.FeIssued =
            long.Parse(jwtSecurityToken.Claims.First(claim => claim.Type == "iat").Value); //Fecha de emisión
        TokenModel.NoToken = tokenHandler.WriteToken(jwtSecurityToken);

        //JwtSecurityToken secToken = new JwtSecurityToken(header, payload);
        //var jwtToken = new JwtSecurityToken(token);


        //string FeIngreso = jwtSecurityToken.Claims.First(claim => claim.Type == "iat").Value;


        return TokenModel;
    }

    public static bool TokenNoExpiro(DateTime? To, DateTime? From)
    {
        //DateTime DateUtc = DateTime.UtcNow.AddMinutes(3);
        var DateUtcNow = DateTime.UtcNow;
        var valid = false;
        if (To.HasValue && DateUtcNow < To && From.HasValue && DateUtcNow > From) valid = true;

        //var hasExpired = To < DateTime.UtcNow;
        return valid;
    }

    public static TokenValidationParameters TokenParametro()
    {
        var SecretKey = Encoding.ASCII.GetBytes(Accessor.Configuration.GetSection("JwtBearerTokenSettings")
            .GetSection("SecretKey").Value);

        var securityKey = new SymmetricSecurityKey(SecretKey);
        var validationParameters = new TokenValidationParameters
        {
            ValidateAudience = false,
            ValidateIssuer = false,
            ValidateLifetime = false,
            ValidateActor = false,
            ValidateTokenReplay = false,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = securityKey,
            ClockSkew = TimeSpan.Zero
        };
        return validationParameters;
    }


    //public static PermisoModel GetUsuario(GlobalTokenModel Token)
    //{
    //    if (Token.NoAccessToken == null) { return null; }


    //    SecurityToken securityToken;
    //    JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
    //    TokenValidationParameters validationParameters = TokenParametro();

    //    ClaimsPrincipal Identidad = tokenHandler.ValidateToken(Token.NoAccessToken, validationParameters, out securityToken);

    //    JwtSecurityToken jwtSecurityToken = securityToken as JwtSecurityToken;
    //    if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256))
    //    {
    //        throw new SecurityTokenException("Token invalido.");
    //        //return StatusCode(400, new BeMensaje() { TxMensaje = "Token invalido", IdTipoMensaje = Enu.IdTipoMensaje.Advertencia });
    //        //return null;
    //    }

    //    PermisoModel Permiso = new PermisoModel();
    //    Permiso.CoEmpresa = Identidad.Claims.First(x => x.Type == "CoEmpresa").Value.Num();
    //    Permiso.CoPermiso = Identidad.Claims.First(x => x.Type == "CoPermiso").Value.Num();
    //    Permiso.CoPersona = Identidad.Claims.First(x => x.Type == "CoPersona").Value.Num();
    //   // Permiso.NoUsuario = Identidad.Claims.First(x => x.Type == "NoUsuario").Value;
    //    //Permiso.CoIngreso = Identidad.Claims.First(x => x.Type == "CoIngreso").Value.Num();

    //    return Permiso;
    //}
}