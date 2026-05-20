using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

using Mozo.Helper.Global;
using Mozo.Model.Seguridad.Auth;

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Mozo.HelperWeb.Token;

public static class UtilityJwt
{

    public static string GenerateRefreshToken()
    {
        Guid g = Guid.NewGuid();
        return g.ToString("N");
    }   

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

        long feExpiration = feExpiracion.Ticks;
        string noToken = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);

        return noToken;
    }



    public static bool TokenNoExpiro(DateTime? To, DateTime? From)
    {
        DateTime DateUtc = DateTime.UtcNow.AddMinutes(3);
        bool valid = false;
        if (To.HasValue && DateUtc < To && From.HasValue && DateUtc > From)
            valid = true;

        return valid;
    }

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


  


}
