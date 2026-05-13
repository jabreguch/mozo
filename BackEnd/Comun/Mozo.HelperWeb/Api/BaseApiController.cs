using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Mozo.Comun.Helper.Global;
using System.Net.Mime;

namespace Mozo.Comun.Implement.Api;
//100-199	100-101	Informational
//200–299	200–206	Successful
//300–399	300–305	Redirection
//400–499	400–415	Client error
//500–599	500–505	Server error

[Authorize]
[ApiController]
[Produces(MediaTypeNames.Application.Json)]
public class BaseApiController : ControllerBase
{
    //protected ObjectResult ClientError(string msg = "Incorrect information") => ObjectResult2(400, msg);
    protected ObjectResult BadRequest2(string msg = "Incorrect information")
    {
        return ObjectResult2(400, msg);
    }

    protected ObjectResult NotFound2(string msg = "Record not found")
    {
        return ObjectResult2(404, msg);
    }

    private ObjectResult ObjectResult2(int codeStatusHttp, string msg)
    {
        var http = Glo.StatusHttp(codeStatusHttp);
        return Problem(msg, Request.Path, int.Parse(http[0]), http[1], http[2]);
    }
}