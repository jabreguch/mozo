using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.OutputCaching;

using Mozo.ApiSeguridad.Helper;
using Mozo.HelperWeb.Api;
using Mozo.MaestroBusiness;
using Mozo.Model.Maestro;


namespace Mozo.Api.Maestro;

public static partial class PersonaEndPoints
{
    public static RouteGroupBuilder MapPersona(this RouteGroupBuilder g)
    {
        g.DisableAntiforgery().RequireAuthorization();
        g.MapPost("/", InsertAsync);
        g.MapPut("/", UpdateAsync);
        g.MapDelete("/{copersona:int}", DeleteByIdAsync);
        g.MapPatch("/state", UpdateStateAsync);
        g.MapGet("/{copersona:int}", SelByIdAsync);
        g.MapGet("/", SelAllAsync);
        g.MapGet("/active", SelAllActiveAsync).CacheOutput(x => x.Expire(TimeSpan.FromHours(24)).Tag("Persona_SelAllActivo"));
        return g;
    }

}
public static partial class PersonaEndPoints
{
    static async Task<Results<Created<int?>, ValidationProblem>> InsertAsync(PersonaModel m, IOutputCacheStore outputCacheStore, IConfiguration configuration, IPersonaBusiness IPersona, IPersonaTipoBusiness IPersonaTipo, IRedSocialBusiness IRedSocial, UserClaims user)
    {        
        if (m.NoPersona == null) { return Extension.CreateValidationProblem("500", "Ingrese nombre de la persona"); ; }        

        //m.CoUbigeo = string.Concat(c.CoDepartamento, c.CoProvincia, c.CoDistrito);

        PersonaModel? personaFind = await IPersona.SelRepeatAsync(m);
        if (personaFind != null)
            return Extension.CreateValidationProblem("500", "Número de documento de Identidad ya existe.");

        m.CoPersona = await IPersona.InsertAsync(m);

        return TypedResults.Created($"/{m.CoPersona}", m.CoPersona);
    }


    static async Task<Results<Ok<int?>, ValidationProblem>> UpdateAsync(PersonaModel m, IOutputCacheStore outputCacheStore, IConfiguration configuration, IPersonaBusiness IPersona, UserClaims user)
    {
        
        if (m.NoPersona == null) { return Extension.CreateValidationProblem("500", "Ingrese nombre de la persona"); ; }     
        //c.CoUbigeo = string.Concat(c.CoDepartamento, c.CoProvincia, c.CoDistrito);

        PersonaModel? personaFind = await IPersona.SelRepeatAsync(m);
        if (personaFind != null)
            return Extension.CreateValidationProblem("500", "Número de documento de Identidad ya existe.");

        await IPersona.UpdateAsync(m);

        return TypedResults.Ok(m.CoPersona);
    }


    static async Task<Results<NoContent, ValidationProblem>> DeleteByIdAsync(int copersona, IOutputCacheStore outputCacheStore, IPersonaBusiness IPersona, UserClaims user)
    {
        PersonaModel m = new() { CoPersona = copersona };
        
        await IPersona.DeleteByIdAsync(m);
        await outputCacheStore.EvictByTagAsync("Persona_SelAllActivo", default);
        return TypedResults.NoContent();
    }

    static async Task<Results<Ok<int?>, ValidationProblem>> UpdateStateAsync(PersonaModel m, IOutputCacheStore outputCacheStore, IPersonaBusiness IPersona, UserClaims user)
    {
        
        await IPersona.UpdateStateAsync(m);
        await outputCacheStore.EvictByTagAsync("Persona_SelAllActivo", default);
        return TypedResults.Ok(m.CoPersona);
    }

    static async Task<Results<Ok<IEnumerable<PersonaModel>>, ValidationProblem>> SelAllActiveAsync([AsParameters] PersonaFilter f, IPersonaBusiness IPersona, UserClaims user)
    {       
        IEnumerable<PersonaModel> r = await IPersona.SelAllActiveAsync(new());
        return TypedResults.Ok(r);
    }

    static async Task<Results<Ok<PersonaModel>, NotFound, ValidationProblem>> SelByIdAsync(int copersona, IPersonaBusiness IPersona, UserClaims user)
    {
        PersonaModel m = new() { CoPersona = copersona };
        
        PersonaModel? i = await IPersona.SelByIdAsync(m);
        if (i == null)
            return TypedResults.NotFound();
        return TypedResults.Ok(i);
    }

    static async Task<Results<Ok<IEnumerable<PersonaModel>>, ValidationProblem>> SelAllAsync([AsParameters] PersonaFilter f, IPersonaBusiness IPersona, UserClaims user)
    {       
        IEnumerable<PersonaModel> r = await IPersona.SelAllAsync(new());
        return TypedResults.Ok(r);
    }







}


