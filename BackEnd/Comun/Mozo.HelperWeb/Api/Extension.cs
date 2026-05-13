using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Mozo.HelperWeb.Api
{
    public static class Extension
    {
        public static ValidationProblem CreateValidationProblem(string errorCode, string errorDescription) => TypedResults.ValidationProblem(new Dictionary<string, string[]> {
        { errorCode, [errorDescription] }
      });

    }
}
