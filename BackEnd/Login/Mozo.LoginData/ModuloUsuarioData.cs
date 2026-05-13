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

public interface IModuloUsuarioData
{
    Task<ModuloUsuarioModel?> SelByIdAsync(ModuloUsuarioFilterDto c);
    Task<ModuloUsuarioModel?> SelByModuloAsync(ModuloUsuarioFilterDto c);
    Task<IEnumerable<ModuloUsuarioModel>> SelAllByPersonaAsync(ModuloUsuarioFilterDto c);

}
public partial class ModuloUsuarioData : IModuloUsuarioData
{
    private readonly string _schema = EnuCommon.BdDefault.Schema.Login;

    private readonly IDbConnection _connection;
    private readonly UserContext _user;
    public ModuloUsuarioData(IDbConnection connection, UserContext user)
    {
        _connection = connection;
        _user = user;
    }
    public async Task<ModuloUsuarioModel?> SelByIdAsync(ModuloUsuarioFilterDto c)
    {
        DynamicParameters pr = new();
        pr.Add2("CoModuloUsuario", c.CoModuloUsuario, DbType.Int32);
        string sql = $"SELECT * FROM {_schema}.fn_modulousuario_sel_by_id(@CoModuloUsuario)";
        return await _connection.QueryFirstOrDefaultAsync<ModuloUsuarioModel>(sql, c);
    }
    public async Task<ModuloUsuarioModel?> SelByModuloAsync(ModuloUsuarioFilterDto c)
    {
        DynamicParameters pr = new();
        pr.Add2("CoModuloUsuario", c.CoModuloUsuario, DbType.Int32);
        pr.Add2("CoModulo", c.CoModulo, DbType.Int32);
        string sql = $"SELECT * FROM {_schema}.fn_modulousuario_sel_by_modulo(@CoModuloUsuario,@CoModulo)";
        return await _connection.QueryFirstOrDefaultAsync<ModuloUsuarioModel>(sql, pr);
    }
    public async Task<IEnumerable<ModuloUsuarioModel>> SelAllByPersonaAsync(ModuloUsuarioFilterDto c)
    {
        DynamicParameters pr = new();
        pr.Add2("CoEmpresa", _user.CoEmpresa, DbType.Int32);
        pr.Add2("CoPersona", c.CoPersona, DbType.Int32);
        string sql = $"SELECT * FROM {_schema}.fn_modulousuario_sel_all_by_persona(@CoEmpresa,@CoPersona)";
        return await _connection.QueryAsync<ModuloUsuarioModel>(sql, pr);
    }



}