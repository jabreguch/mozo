using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace Mozo.HelperWeb.Token;

public class Authentication : JwtBearerEvents
{

    public override Task TokenValidated(TokenValidatedContext context)
    {
       

        if (!UtilityJwt.TokenNoExpiro(context.SecurityToken.ValidTo, context.SecurityToken.ValidFrom))
        {
            context.Fail("Token Expiro.");           
            return Task.CompletedTask;           
        }
       
        return Task.CompletedTask;
    }

    public override Task Challenge(JwtBearerChallengeContext context)
    {     
        return Task.CompletedTask;
    }

    public override Task Forbidden(ForbiddenContext context)
    {
        return Task.CompletedTask;
    }
    public override Task MessageReceived(MessageReceivedContext context)
    {

        return Task.CompletedTask;
    }
 
    public override Task AuthenticationFailed(AuthenticationFailedContext context)
    {
        return Task.CompletedTask;
    }

}
