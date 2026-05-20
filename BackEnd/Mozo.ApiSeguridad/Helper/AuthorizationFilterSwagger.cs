using Microsoft.AspNetCore.Authorization;
using Microsoft.OpenApi;

using Swashbuckle.AspNetCore.SwaggerGen;

namespace Mozo.ApiSeguridad.Helper;

public class AuthorizationFilterSwagger : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        if (!context.ApiDescription.ActionDescriptor.EndpointMetadata
                .OfType<AuthorizeAttribute>().Any())
           return;
      
        var securityRequirement = new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecuritySchemeReference("Bearer"),
                new List<string>()
            }
        };

        operation.Security = new List<OpenApiSecurityRequirement> { securityRequirement };
    }
}