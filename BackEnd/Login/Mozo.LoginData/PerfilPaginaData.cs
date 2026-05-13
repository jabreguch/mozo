using Dapper;

using Mozo.Helper.Enu;
using Mozo.Helper.Global;
using Mozo.Model.Seguridad;

using System.Data;

///<summary>
///
///</summary>
///<remarks>
///</remarks>
///<history>
/// t[Jonatan Abregu]	16/11/2018	Created
///</history>
namespace Mozo.LoginData;

public interface IPerfilPaginaData
{
    Task<IEnumerable<PerfilPaginaModel>> SelAllByModuloAndPerfilAsync(PerfilPaginaFilterDto c);
}
public partial class PerfilPaginaData : IPerfilPaginaData
{
    private readonly string _schema = EnuCommon.BdDefault.Schema.Login;

    private readonly IDbConnection _connection;
    private readonly UserContext _user;
    public PerfilPaginaData(IDbConnection connection, UserContext user)
    {
        _connection = connection;
        _user = user;
    }


    public async Task<IEnumerable<PerfilPaginaModel>> SelAllByModuloAndPerfilAsync(PerfilPaginaFilterDto c)
    {
        DynamicParameters pr = new();
        pr.Add2("CoModulo", c.CoModulo, DbType.Int32);
        pr.Add2("CoPerfil", c.CoPerfil, DbType.Int32);
        string sql = $"SELECT * FROM {_schema}.fn_perfilpagina_sel_all_by_modulo_and_perfil(@CoModulo,@CoPerfil)";
        return await _connection.QueryAsync<PerfilPaginaModel>(sql, pr);
    }

}