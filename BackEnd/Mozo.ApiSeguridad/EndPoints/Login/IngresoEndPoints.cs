using Mozo.ApiSeguridad.Helper;
using Mozo.LoginBusiness;
using Mozo.Model.Seguridad;

namespace Mozo.Api.Login;

///<summary>
/// Endpoints para gestión de Ingreso
///</summary>
///<history>
/// Create by Jonatan Abregu
///</history>
public static partial class IngresoEndPoints
{
    /// <summary>
    /// Mapea todas las rutas de Ingreso
    /// </summary>
    public static RouteGroupBuilder MapIngreso(this RouteGroupBuilder g)
    {
        g.WithSecurity();

        g.MapPatch("/close", CloseTokenAsync)
            .WithResponses(StatusCodes.Status204NoContent)
            .WithDescription("Cerrar sessión");

        return g;
    }
}

public static partial class IngresoEndPoints
{

    static async Task<IResult>
        CloseTokenAsync(
            IngresoModel m,
            IIngresoBusiness
            IIngreso
        )
    {
        await IIngreso.UpdateCloseTokenAsync(m);
        return Results.NoContent();
    }



}