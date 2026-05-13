using Microsoft.AspNetCore.Http.HttpResults;

namespace Mozo.Api.Maestro;
public static partial class UbigeoEndPoints
{
    public static RouteGroupBuilder MapUbigeo(this RouteGroupBuilder group)
    {
        group.MapGet("/SelAllDepartamento", SelAllDepartamento)
            .RequireAuthorization()
             .CacheOutput(x => x.Expire(TimeSpan.FromHours(24)).Tag("Ubigeo_GetAllDepartamento")); ;

        group.MapGet("/SelAllProvincia", SelAllProvincia)
             .RequireAuthorization();

        group.MapGet("/SelAllDistrito", SelAllDistrito)
      .RequireAuthorization();

        return group;
    }

}
public static partial class UbigeoEndPoints
{
    static async Task<Results<Ok<IEnumerable<UbigeoModel>>, ValidationProblem>> SelAllDepartamento(UbigeoModel c, IUbigeoBusiness IUbigeo)
    {
        IEnumerable<UbigeoModel> r = await IUbigeo.SelAllDepartamento(c);
        return TypedResults.Ok(r);
    }

    static async Task<Results<Ok<IEnumerable<UbigeoModel>>, ValidationProblem>> SelAllProvincia(UbigeoModel c, IUbigeoBusiness IUbigeo)
    {
        IEnumerable<UbigeoModel> r = await IUbigeo.SelAllProvincia(c);
        return TypedResults.Ok(r);
    }

    static async Task<Results<Ok<IEnumerable<UbigeoModel>>, ValidationProblem>> SelAllDistrito(UbigeoModel c, IUbigeoBusiness IUbigeo)
    {
        IEnumerable<UbigeoModel> r = await IUbigeo.SelAllDistrito(c);
        return TypedResults.Ok(r);
    }







}

