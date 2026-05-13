using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Mozo.Comun.Helper.Global;
using Mozo.Comun.Implement.Log;
using System;
using System.Diagnostics;
using System.Text.Json;
using System.Threading.Tasks;

namespace Mozo.Comun.Implement;

public class ProblemException : Exception
{
    //private static readonly string DefaultMessage = "Account balance is insufficient for the transaction.";

    public int? Status { get; set; }
    public string Title { get; set; }
    public string Instance { get; set; }
    public string Type { get; set; }
    public string Detail { get; set; }

    //public ProblemException() : base(DefaultMessage) { }
    //public ProblemException(string message) : base(message) { }
    //public ProblemException(string message, System.Exception innerException)
    //: base(message, innerException) { }

    //public ProblemException(string accountName, int accountBalance, int transactionAmount)
    //: base(DefaultMessage)
    //{
    //    AccountName = accountName;
    //    AccountBalance = accountBalance;
    //    TransactionAmount = transactionAmount;
    //}

    //public ProblemException(string accountName, int accountBalance, int transactionAmount, System.Exception innerException)
    //: base(DefaultMessage, innerException)
    //{
    //    AccountName = accountName;
    //    AccountBalance = accountBalance;
    //    TransactionAmount = transactionAmount;
    //}

    //protected ProblemException(
    //    System.Runtime.Serialization.SerializationInfo info,
    //    System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
}

public static class CustomErrorHandlerHelper
{
    public static void UseCustomErrors(this IApplicationBuilder app, IHostEnvironment environment)
    {
        if (environment.IsDevelopment())
            app.Use(WriteDevelopmentResponse);
        else
            app.Use(WriteProductionResponse);
    }

    private static Task WriteDevelopmentResponse(HttpContext httpContext, Func<Task> next)
    {
        return WriteResponse(httpContext, true);
    }

    private static Task WriteProductionResponse(HttpContext httpContext, Func<Task> next)
    {
        return WriteResponse(httpContext, false);
    }

    private static async Task WriteResponse(HttpContext httpContext, bool includeDetails)
    {
        // Try and retrieve the error from the ExceptionHandler middleware
        var exceptionDetails = httpContext.Features.Get<IExceptionHandlerFeature>();
        httpContext.Response.ContentType = "application/problem+json";

        var problem = new ProblemDetails();
        if (exceptionDetails.Error is ProblemException)
        {
            var ex = (ProblemException)exceptionDetails?.Error;
            problem.Status = ex.Status;
            problem.Title = ex.Title;
            problem.Instance = ex.Instance;
            problem.Type = ex.Type;
            problem.Detail = ex.Detail;
            ELog.Save(problem);
        }
        else
        {
            var ex = exceptionDetails?.Error;
            if (ex != null)
            {
                var traceId = Activity.Current?.Id ?? httpContext?.TraceIdentifier;
                var http = Glo.StatusHttp(httpContext.Response.StatusCode);
                problem.Status = int.Parse(http[0]);
                problem.Title = ex.Message.NoNulo() ? ex.Message : http[1];
                problem.Instance = httpContext.Request.Path;
                problem.Type = ex.HelpLink.NoNulo() ? ex.HelpLink : http[2];
                problem.Detail = string.Concat(ex.ToString(), traceId.NoNulo() ? "...traceId: " + traceId : "");
                ELog.Save(problem);
                problem.Detail = ex.Message;
                //Serialize the problem details object to the Response as JSON (using System.Text.Json)
            }
        }

        var stream = httpContext.Response.Body;
        await JsonSerializer.SerializeAsync(stream, problem);


        // Should always exist, but best to be safe!
    }
}