using Mozo.ApiSeguridad.Helper;
using Mozo.LoginBusiness;
using Mozo.Model.Seguridad;

namespace Mozo.Api.Login;

///<summary>
/// Endpoints para gestión de Menu
///</summary>
///<history>
/// Create by Jonatan Abregu
///</history>
public static partial class MenuEndPoints
{
    /// <summary>
    /// Mapea todas las rutas de Menu
    /// </summary>
    public static RouteGroupBuilder MapMenu(this RouteGroupBuilder g)
    {
        g.WithSecurity();

        g.MapGet("/modulos", SelAllModuloAsync)
            .WithResponses<List<ModuloUsuarioModel>>(StatusCodes.Status200OK)
            .WithDescription("Obtener las opciones del sistema");
        return g;
    }
}

public partial class MenuEndPoints
{
    private static async Task<IResult>
        SelAllModuloAsync(
            IModuloUsuarioBusiness IModuloUsuario,
            IPerfilPaginaBusiness IPerfilPagina,
            UserClaims user
        )
    {
        return Results.Ok(
            await MenuLogin.SelAllModuloAsync(
                new() { CoPersona = user.CoPersona },
                IModuloUsuario, IPerfilPagina
         ));
    }
}