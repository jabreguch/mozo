using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

using Mozo.Helper.Global;
using Mozo.Model.Seguridad.Auth;

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Mozo.HelperWeb.Token;
/*
public static class UserContextClaims
{
    public static UserContext UserContext(this HttpContext context)
             => (UserContext)context.Items["UserContext"]!;
}
*/
public static class UtilityJwt
{

    public static string GenerateRefreshToken()
    {
        Guid g = Guid.NewGuid();
        return g.ToString("N");

        //byte[] randomNumber = new byte[64];
        //using RandomNumberGenerator rng = RandomNumberGenerator.Create();
        //rng.GetBytes(randomNumber);
        //return Convert.ToBase64String(randomNumber);
    }
    //[JsonPropertyName("NoUsuario")] public string? NoUsuario { get; set; }
    //[JsonPropertyName("NoPersona")] public string? NoPersona { get; set; }
    //[JsonPropertyName("CoEmpresa")] public int? CoEmpresa { get; set; }
    //[JsonPropertyName("CoPersona")] public int? CoPersona { get; set; }
    //[JsonPropertyName("CoPermiso")] public int? CoPermiso { get; set; }
    //[JsonPropertyName("CoIngreso")] public int? CoIngreso { get; set; }

    public static string GenerateToken(CredencialModel credential, IConfiguration configuration)
    {
        byte[] SecretKey = Encoding.ASCII.GetBytes(configuration.GetSection("JwtBearerTokenSettings").GetSection("SecretKey").Value!);
        //Convert.from
        SymmetricSecurityKey securityKey = new SymmetricSecurityKey(SecretKey);
        SigningCredentials signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        // Datos del usuario
        List<Claim> claimCollection = new();

        if (credential.CoEmpresa != null)
            claimCollection.Add(new Claim("CoEmpresa", credential.CoEmpresa!.Text()));

        if (credential.CoPersona != null)
            claimCollection.Add(new Claim("CoPersona", credential.CoPersona!.Text()));

        if (credential.CoPermiso != null)
            claimCollection.Add(new Claim("CoPermiso", credential.CoPermiso!.Text()));

        if (credential.CoIngreso != null)
            claimCollection.Add(new Claim("CoIngreso", credential.CoIngreso!.Text()));

        if (credential.NoUsuario != null)
            claimCollection.Add(new Claim("NoUsuario", credential.NoUsuario!));

        //if (credential.NoPersona != null)
        //    claimCollection.Add(new Claim(ClaimTypes.Name, credential.NoPersona!));


        //ClaimsIdentity claimsIdentity = new ClaimsIdentity(claimCollection);
        // create token to the user
        //JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
        DateTime feExpiracion = DateTime.UtcNow.AddMinutes(double.Parse(configuration.GetSection("JwtBearerTokenSettings").GetSection("ExpiryTimeInMinute").Value!));

        JwtSecurityToken jwtSecurityToken = new JwtSecurityToken(
                //audience: "http://localhost",
                //issuer: "http://localhost",                    
                issuer: null,
                audience: null,
                claims: claimCollection,
                expires: feExpiracion,
                //notBefore: DateTime.UtcNow,                   
                signingCredentials: signingCredentials
        );

        //TokenModel TokenModel = new()
        //{
        //    FeExpiration = feExpiracion.Ticks,
        //    NoToken = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken)
        //};


        long feExpiration = feExpiracion.Ticks;
        string noToken = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);


        return noToken;
    }



    public static bool TokenNoExpiro(DateTime? To, DateTime? From)
    {
        DateTime DateUtc = DateTime.UtcNow.AddMinutes(3);
        bool valid = false;
        if (To.HasValue && DateUtc < To && From.HasValue && DateUtc > From)
        {
            valid = true;
        }

        //var hasExpired = To < DateTime.UtcNow;
        return valid;
    }
    //Configuration.GetValue<JwtModel>("Jwt").Key
    public static TokenValidationParameters TokenParametro(IConfiguration configuration)
    {
        byte[] SecretKey = Encoding.ASCII.GetBytes(configuration.GetSection("JwtBearerTokenSettings").GetSection("SecretKey").Value!);

        SymmetricSecurityKey securityKey = new SymmetricSecurityKey(SecretKey);
        TokenValidationParameters validationParameters = new TokenValidationParameters()
        {
            ValidateAudience = false,
            ValidateIssuer = false,
            ValidateLifetime = true,
            //ValidateActor = false,
            //ValidateTokenReplay = false,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = securityKey,
            ClockSkew = TimeSpan.Zero
        };
        return validationParameters;
    }


    //public static Tuple<BeGlobalSession, string> GetUsuario(BeToken Token, IConfiguration configuration)
    //{          
    //    if (Token.NoAccessToken == null) { return null; }
    //    //c.TxToken = c.TxToken.StartsWith("Bearer ") ? c.TxToken.Substring(7) : c.TxToken;


    //    SecurityToken securityToken;
    //    JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
    //    TokenValidationParameters validationParameters = TokenParametro(configuration);

    //    ClaimsPrincipal Identidad = tokenHandler.ValidateToken(Token.NoAccessToken, validationParameters, out securityToken);

    //    JwtSecurityToken jwtSecurityToken = securityToken as JwtSecurityToken;
    //    if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256))
    //    {
    //        return    new Tuple<BeGlobalSession, string>(null, "Token invalido.");
    //        //throw new SecurityTokenValidationException("Token invalido.");
    //        //return StatusCode(400, new BeMensaje() { TxMensaje = "Token invalido", IdTipoMensaje = Enu.IdTipoMensaje.Advertencia });
    //        //return null;
    //    }

    //    if (TokenNoExpiro(jwtSecurityToken.ValidTo, jwtSecurityToken.ValidFrom))
    //    {
    //        string TxUsuario = Identidad.Claims.First(x => x.Type == "Usuario").Value.Text();
    //        //GlobalSessionModel Usuario = null;
    //        if (TxUsuario.NoNulo())
    //        {
    //            //Response.Headers.Add("TokenVencido", "0");
    //            return new Tuple<BeGlobalSession, string>(TxUsuario.Deserializa<BeGlobalSession>(), null);
    //           // return TxUsuario.Deserializa<BeGlobalSession>();
    //        }
    //        else
    //        {
    //            return new Tuple<BeGlobalSession, string>(null, "Fallo al validar token.");
    //            //throw new SecurityTokenValidationException("Fallo al validar token.");
    //        }
    //    }
    //    else
    //    {
    //        //Response.Headers.Add("TokenVencido", "1");
    //        //return new BeUsers() { FlTokenVencido = 1 };
    //        return new Tuple<BeGlobalSession, string>(null, "Token expiro.");
    //        //throw new  SecurityTokenExpiredException("Token expiro.");
    //    }

    //}


}
