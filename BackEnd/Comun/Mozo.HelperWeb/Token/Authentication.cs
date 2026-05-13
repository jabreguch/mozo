using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace Mozo.HelperWeb.Token;

public class Authentication : JwtBearerEvents
{

    //public _IPaginaService _paginaService;
    //public _IModuloUsuarioService _moduloUsuarioService;
    //public Authentication(
    //    _IPaginaService paginaService
    //    , _IModuloUsuarioService moduloUsuarioService
    //    )
    //{
    //    _moduloUsuarioService = moduloUsuarioService;
    //    _paginaService = paginaService;
    //}


    //private bool GetPermiso(GlobalSessionModel c, string NoControlador, string NoAccion)
    //{
    //    return true;
    //    List<ModuloUsuarioModel> ModuloUsuarioCol = _moduloUsuarioService._GetAllPersona(new ModuloUsuarioModel() {CoEmpresa = c.CoEmpresa, CoPersona = c.CoUsuario } ).ToList();
    //    foreach (ModuloUsuarioModel ModuloUsuario in ModuloUsuarioCol)
    //    {
    //        List<ServicioWebModel> ServicioWebCol = _paginaService._GetAllServicioWeb(new ServicioWebModel() { CoEmpresa = ModuloUsuario.CoEmpresa, CoModulo = ModuloUsuario.CoModulo, CoPersona = ModuloUsuario.CoPersona }).ToList();
    //        ServicioWebModel TmpServicioWeb = ServicioWebCol.Find(x => x.NoControlador.Minuscula() == NoControlador && x.NoAccion.Minuscula() == NoAccion);
    //        if (TmpServicioWeb != null)
    //        {
    //            return true;
    //        }                
    //    }
    //    return false;
    //}

    public override Task TokenValidated(TokenValidatedContext context)
    {
        //StringValues HeaderValues;
        //List<string> AudienciaCol = Glo.WebConfig.Jwt.Audience().Split(",").ToList();
        //if (context.Request.Headers.TryGetValue("origin", out HeaderValues))
        //{
        //    if (!AudienciaCol.Contains(HeaderValues.First()))
        //    {                   
        //        context.Fail("Origen no valido.");
        //        context.HttpContext.Response.StatusCode = 401;                   
        //        return Task.CompletedTask;
        //    }
        //}

        if (!UtilityJwt.TokenNoExpiro(context.SecurityToken.ValidTo, context.SecurityToken.ValidFrom))
        {
            context.Fail("Token Expiro.");
            //context.HttpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
            return Task.CompletedTask;
            //throw new SecurityTokenExpiredException("Token Expiro.");// SecurityTokenValidationException()

            //context.Fail("Token Expiro.");
            //context.HttpContext.Response.StatusCode = 401;
            //return Task.CompletedTask;
        }

        //new Claim("TxUser", c.TxUser!),
        //        new Claim("IdUsersRop", c.IdUsersRop.ToString()!),
        //        new Claim("TxUserSgu", c.TxUserSgu!)

        //ClaimsPrincipal identity = (ClaimsPrincipal)Thread.CurrentPrincipal;
        //ClaimsPrincipal identity = context.Principal;
        //string TxUser = identity.Claims.First(x => x.Type == "TxUser").Value;
        //BeToken Usuario = StrUsuario.Deserializa<BeGlobalSession>();
        //**Verficar si tiene Captcha**//
        //if (Usuario.NoCaptchaSistema.NoNulo())
        //{
        //    if (context.Request.Headers.TryGetValue("NoCaptcha", out HeaderValues))
        //    {
        //        if (Usuario.NoCaptchaSistema != HeaderValues.First().Text())
        //        {
        //            context.Fail("Codigo de la imagen incorrecto.");
        //            context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
        //            return Task.CompletedTask;
        //            //throw new SecurityTokenException("Codigo de la imagen incorrecto.");// SecurityTokenValidationException()
        //            //context.Fail("Codigo de la imagen incorrecto");
        //            //context.HttpContext.Response.StatusCode = 401;
        //            //return Task.CompletedTask;
        //        }
        //    }
        //    else
        //    {
        //        context.Fail("Codigo de la imagen no ingresado.");
        //        context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
        //        return Task.CompletedTask;
        //        //throw new SecurityTokenException("Codigo de la imagen no ingresado.");
        //        //context.Fail("Codigo de la imagen no ingresado.");
        //        //context.HttpContext.Response.StatusCode = 401;
        //        //return Task.CompletedTask;
        //    }
        //}
        //**Fin Verficar si tiene Captcha**//

        //RouteValueDictionary rv = context.Request.RouteValues;
        //object NoArea, NoControlador, NoAccion;
        //rv.TryGetValue("area", out NoArea);
        //rv.TryGetValue("controller", out NoControlador);
        //rv.TryGetValue("action", out NoAccion);


        //if (GetPermiso(Usuario, NoControlador.Minuscula(), NoAccion.Minuscula()) == false) {
        //    throw new SecurityTokenException("El usuario no tiene permiso a esta opción");
        //}




        //context.HttpContext.Session.SesionSet(EnuCommon.GLOBAL_NAME_SESSION_CREDENTIAL, Usuario);
        //Glo.CacheSet("Usuario", StrUsuario);


        //context.HttpContext.Items.Add("Usuario", StrUsuario);
        //context.Result.Failure.InnerException
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
