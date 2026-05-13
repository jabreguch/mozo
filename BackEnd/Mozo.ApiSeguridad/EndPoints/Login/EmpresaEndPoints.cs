using Mozo.ApiSeguridad.Helper;
using Mozo.LoginBusiness;
using Mozo.Model.Seguridad;

namespace Mozo.Api.Login;

///<summary>
/// Endpoints para gestión de Empresa
///</summary>
///<history>
/// Create by Jonatan Abregu
///</history>
public static partial class EmpresaEndPoints
{
    /// <summary>
    /// Mapea todas las rutas de Empresa
    /// </summary>
    public static RouteGroupBuilder MapEmpresa(this RouteGroupBuilder g)
    {
        g.WithSecurity();

        g.MapGet("/byid", SelByIdAsync)
           .WithResponses<ModuloModel>(StatusCodes.Status200OK)
           .Produces(StatusCodes.Status404NotFound)
           .WithDescription("Obtener una Empresa");

        g.MapGet("/", SelAllAsync)
           .WithResponses<IEnumerable<EmpresaModel>>(StatusCodes.Status200OK)
           .WithDescription("Obtener todas las empresas");

        return g;
    }

}

public partial class EmpresaEndPoints
{
    private static async Task<IResult>
         SelByIdAsync(
             [AsParameters] EmpresaFilterDto f,
             IEmpresaBusiness IEmpresa
         )
    {
        EmpresaModel? i = await IEmpresa.SelByIdAsync(f);
        if (i == null)
            return Results.NotFound();
        return Results.Ok(i);
    }

    private static async Task<IResult>
        SelAllAsync(IEmpresaBusiness IEmpresa)
    {
        IEnumerable<EmpresaModel> r = await IEmpresa.SelAllAsync();
        return Results.Ok(r);
    }

}