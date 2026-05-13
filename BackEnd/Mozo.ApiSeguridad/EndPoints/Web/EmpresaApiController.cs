using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Mozo.AppSeguridadWeb.Interface.Service;
using Mozo.Comun.Implement.Api;
using Mozo.Model.Seguridad;

namespace Mozo.ApiSeguridad.Controllers.Web;

[Route("Web/[controller]")]
public class EmpresaApiController : BaseApiController
{
    private readonly IEmpresaService _empresaService;
    private IConfiguration _configuration;

    public EmpresaApiController(IEmpresaService empresaService
        , IConfiguration configuration
    )
    {
        _empresaService = empresaService;
        _configuration = configuration;
    }


    [AllowAnonymous]
    [HttpGet]
    [Route("[action]")]
    public IActionResult GetById([FromQuery] EmpresaModel c)
    {
        c = _empresaService.GetById(c);
        if (c == null)
            return NotFound2();
        return Ok(c);
    }


    [AllowAnonymous]
    [HttpGet]
    [Route("[action]")]
    public IActionResult GetAll([FromQuery] EmpresaModel c)
    {
        var r = _empresaService.GetAll(c);
        return Ok(r);
    }
}