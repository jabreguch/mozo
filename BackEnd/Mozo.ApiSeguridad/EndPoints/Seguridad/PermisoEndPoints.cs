using Mozo.ApiSeguridad.Helper;
using Mozo.ApiSeguridad.HelperWeb.Exceptions;
using Mozo.Model.Seguridad;
using Mozo.SeguridadBusiness;

namespace Mozo.Api.Seguridad;

///<summary>
/// Endpoints para gestión de Permiso
///</summary>
///<history>
/// Create by Jonatan Abregu
///</history>
public static partial class PermisoEndPoints
{
    /// <summary>
    /// Mapea todas las rutas de Permiso
    /// </summary>
    public static RouteGroupBuilder MapPermiso(this RouteGroupBuilder g)
    {
        g.WithSecurity();

        g.MapPost("/", InsertAsync)
            .WithResponsesValue<int>(StatusCodes.Status200OK)
             .WithDescription("Insertar un Permiso");

        g.MapPut("/", UpdateAsync)
            .WithResponsesValue<int>(StatusCodes.Status200OK)
            .WithDescription("Actualizar un Permiso");

        g.MapPatch("/state", UpdateStateAsync)
            .WithResponsesValue<int>(StatusCodes.Status200OK)
             .WithDescription("Activar o desactivar un Permiso");

        g.MapGet("/byid", SelByIdAsync)
            .WithResponses<PermisoModel>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound)
            .WithDescription("Obtener un Permiso");
        return g;
    }

}
public partial class PermisoEndPoints
{

    private static async Task<IResult>
        InsertAsync(
                PermisoModel m,
                IPermisoBusiness IPermiso
        )
    {
        if (m == null)
            throw new ArgumentNullException(nameof(m));

        if (string.IsNullOrWhiteSpace(m.NoUsuario))
            throw new ValidationException("NoUsuario", "El usuario es requerido");

        if (string.IsNullOrWhiteSpace(m.NoClave))
            throw new ValidationException("NoClave", "La contraseña es requerida");


        m.CoPermiso = await IPermiso.InsertAsync(m);

        return Results.Created($"/{m.CoPermiso}", m.CoPermiso);
    }

    private static async Task<IResult>
        UpdateAsync(
            PermisoModel m,
            IPermisoBusiness IPermiso
       )
    {
        if (m == null)
            throw new ArgumentNullException(nameof(m));

        if (string.IsNullOrWhiteSpace(m.NoUsuario))
            throw new ValidationException("NoUsuario", "El usuario es requerido");

        if (string.IsNullOrWhiteSpace(m.NoClave))
            throw new ValidationException("NoClave", "La contraseña es requerida");

        await IPermiso.UpdateAsync(m);
        return Results.Ok(m.CoPermiso);
    }
    private static async Task<IResult>
        UpdateStateAsync(
            PermisoModel m,
            IPermisoBusiness IPermiso
     )
    {
        await IPermiso.UpdateStateAsync(m);
        return Results.Ok(m.CoPermiso);
    }




    private static async Task<IResult>
        SelByIdAsync(
            [AsParameters] PermisoFilterDto f,
            IPermisoBusiness IPermiso
    )
    {
        PermisoModel? i = await IPermiso.SelByIdAsync(f);
        if (i is null)
            return Results.NotFound();
        return Results.Ok(i);
    }


}