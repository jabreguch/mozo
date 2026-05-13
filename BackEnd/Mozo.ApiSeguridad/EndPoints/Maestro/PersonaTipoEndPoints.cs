using Microsoft.AspNetCore.Http.HttpResults;

using Mozo.ApiSeguridad.Helper;
using Mozo.MaestroBusiness;
using Mozo.Model.Maestro;

namespace Mozo.Api.Maestro;

public static partial class PersonaTipoEndPoints
{
    public static RouteGroupBuilder MapPersonaTipo(this RouteGroupBuilder g)
    {
        g.DisableAntiforgery().RequireAuthorization();
        g.MapPost("/", InsertAsync);
        g.MapPut("/", UpdateAsync).RequireAuthorization();
        g.MapGet("", SelAllActiveAsync);
        return g;
    }

}
public static partial class PersonaTipoEndPoints
{
    static async Task<Results<Created<int?>, ValidationProblem>> InsertAsync(PersonaTipoModel m, IPersonaTipoBusiness IPersonaTipo, UserClaims user)
    {
        m.CoPersonaTipo = await IPersonaTipo.InsertAsync(m);
        return TypedResults.Created($"/{m.CoPersonaTipo}", m.CoPersonaTipo);
    }

    static async Task<Results<Ok<int?>, ValidationProblem>> UpdateAsync(PersonaTipoModel m, IPersonaTipoBusiness IPersonaTipo, UserClaims user)
    {

        await IPersonaTipo.UpdateAsync(m);
        return TypedResults.Ok(m.CoPersona);
    }

    static async Task<Results<Ok<IEnumerable<PersonaTipoModel>>, ValidationProblem>> SelAllActiveAsync([AsParameters] PersonaTipoFilter f, IPersonaTipoBusiness IPersonaTipo, UserClaims user)
    {
        IEnumerable<PersonaTipoModel> r = Enumerable.Empty<PersonaTipoModel>();
        if (f.CoModulo != null && f.CoPersona != null)
            r = await IPersonaTipo.SelAllActiveByModuloAndPersonaAsync(new());
        else if (f.CoModulo == null && f.CoPersona != null)
            r = await IPersonaTipo.SelAllActiveByPersonaAsync(new());
        else if (f.CoModulo == null && f.CoPersona == null)
            r = await IPersonaTipo.SelAllActiveAsync(new());
        return TypedResults.Ok(r);
    }



}


