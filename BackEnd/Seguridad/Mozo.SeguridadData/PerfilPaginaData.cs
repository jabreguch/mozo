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
namespace Mozo.SeguridadData;

public interface IPerfilPaginaData
{
    Task<int> InsertAsync(PerfilPaginaModel c);
    Task DeleteByModuloAndPerfilAsync(PerfilPaginaFilterDto c);
    Task<IEnumerable<PerfilPaginaModel>> SelAllAsync(PerfilPaginaFilterDto c);
}
public partial class PerfilPaginaData : IPerfilPaginaData
{
    private readonly string _schema = EnuCommon.BdDefault.Schema.Seguridad;
    private readonly IDbConnection _connection;
    private readonly UserContext _user;
    public PerfilPaginaData(IDbConnection connection, UserContext user)
    {
        _connection = connection;
        _user = user;
    }

    public async Task<int> InsertAsync(PerfilPaginaModel c)
    {
        DynamicParameters pr = new();
        pr.Add2("CoModulo", c.CoModulo, DbType.Int32);
        pr.Add2("CoPerfil", c.CoPerfil, DbType.Int32);
        pr.Add2("CoPagina", c.CoPagina, DbType.Int32);      
        pr.Add2("CoMenu", c.CoMenu, DbType.Int32);
        pr.Add2("CoUsuCre", _user.CoPersona, DbType.Int32);

        string sql = $@"SELECT {_schema}.fn_perfilpagina_insert(
           @CoModulo,
           @CoPerfil,
           @CoPagina,         
           @CoMenu,
           @CoUsuCre
        )";
        return await _connection.ExecuteScalarAsync<int>(sql, pr);
    }
    public async Task DeleteByModuloAndPerfilAsync(PerfilPaginaFilterDto c)
    {
        DynamicParameters pr = new();
        pr.Add2("CoModulo", c.CoModulo, DbType.Int32);
        pr.Add2("CoPerfil", c.CoPerfil, DbType.Int32);
        pr.Add2("CoUsuEli", _user.CoPersona, DbType.Int32);
        string sql = $"CALL {_schema}.usp_perfilpagina_delete_by_modulo_and_perfil(@CoModulo,@CoPerfil,@CoUsuEli)";
        await _connection.ExecuteScalarAsync(sql, pr);
    }
    public async Task<IEnumerable<PerfilPaginaModel>> SelAllAsync(PerfilPaginaFilterDto c)
    {
        DynamicParameters pr = new();
        pr.Add2("CoModulo", c.CoModulo, DbType.Int32);
        pr.Add2("CoPerfil", c.CoPerfil, DbType.Int32);

        string sql = $"SELECT * FROM {_schema}.fn_perfilpagina_sel_all(@CoModulo,@CoPerfil)";
        return await _connection.QueryAsync<PerfilPaginaModel>(sql, pr);
    }

}