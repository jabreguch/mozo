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

public interface IModuloUsuarioData
{
    Task<int> InsertAsync(ModuloUsuarioModel c);
    Task UpdateAsync(ModuloUsuarioModel c);
    Task DeleteByIdAsync(ModuloUsuarioFilterDto c);
    Task UpdateConfiguracionAsync(ModuloUsuarioModel c);
    Task<ModuloUsuarioModel?> SelByIdAsync(ModuloUsuarioFilterDto c);
    Task<IEnumerable<ModuloUsuarioModel>> SelAllByModuloAsync(ModuloUsuarioFilterDto c);
    Task<IEnumerable<ModuloUsuarioModel>> SelAllByPersonaAsync(ModuloUsuarioFilterDto c);
    Task<ModuloUsuarioModel?> SelByModuloAndPersonaAsync(ModuloUsuarioFilterDto c);
}
public partial class ModuloUsuarioData : IModuloUsuarioData
{
    private readonly string _schema = EnuCommon.BdDefault.Schema.Seguridad;

    private readonly IDbConnection _connection;
    private readonly UserContext _user;
    public ModuloUsuarioData(IDbConnection connection, UserContext user)
    {
        _connection = connection;
        _user = user;
    }

    public async Task<int> InsertAsync(ModuloUsuarioModel c)
    {
        DynamicParameters pr = new();
        pr.Add2("CoEmpresa", c.CoEmpresa, DbType.Int32);
        pr.Add2("CoModulo", c.CoModulo, DbType.Int32);
        pr.Add2("CoPersona", c.CoPersona, DbType.Int32);
        pr.Add2("CoPerfil", c.CoPerfil, DbType.Int32);
        pr.Add2("FeExpiracion", c.FeExpiracion, DbType.String);

        string sql = $"SELECT {_schema}.fn_modulousuario_insert(@CoEmpresa,@CoModulo,@CoPersona,@CoPerfil,@FeExpiracion)";
        return await _connection.ExecuteScalarAsync<int>(sql, pr);
    }
    public async Task UpdateAsync(ModuloUsuarioModel c)
    {
        DynamicParameters pr = new();
        pr.Add2("CoEmpresa", c.CoEmpresa, DbType.Int32);
        pr.Add2("CoModuloUsuario", c.CoModuloUsuario, DbType.Int32);
        pr.Add2("CoModulo", c.CoModulo, DbType.Int32);
        pr.Add2("CoPersona", c.CoPersona, DbType.Int32);
        pr.Add2("CoPerfil", c.CoPerfil, DbType.Int32);
        pr.Add2("FeExpiracion", c.FeExpiracion, DbType.String);

        string sql = $"CALL {_schema}.usp_modulousuario_update(@CoEmpresa,@CoModuloUsuario,@CoModulo,@CoPersona,@CoPerfil,@FeExpiracion)";
        await _connection.ExecuteScalarAsync(sql, pr);
    }
    public async Task UpdateConfiguracionAsync(ModuloUsuarioModel c)
    {
        DynamicParameters pr = new();
        pr.Add2("CoEmpresa", c.CoEmpresa, DbType.Int32);
        pr.Add2("CoModuloUsuario", c.CoModuloUsuario, DbType.Int32);
        pr.Add2("TxConfiguracion", c.TxConfiguracion, DbType.String);
        string sql = $"CALL {_schema}.usp_modulousuario_update_configuracion(@CoEmpresa,@CoModuloUsuario,@TxConfiguracion)";
        await _connection.ExecuteScalarAsync(sql, pr);
    }
    public async Task DeleteByIdAsync(ModuloUsuarioFilterDto c)
    {
        DynamicParameters pr = new();
        pr.Add2("CoEmpresa", c.CoEmpresa, DbType.Int32);
        pr.Add2("CoModuloUsuario", c.CoModuloUsuario, DbType.Int32);
        string sql = $"CALL {_schema}.usp_modulousuario_delete_by_id(@CoEmpresa,@CoModuloUsuario)";
        await _connection.ExecuteScalarAsync(sql, pr);
    }
    public async Task<IEnumerable<ModuloUsuarioModel>> SelAllByModuloAsync(ModuloUsuarioFilterDto c)
    {
        DynamicParameters pr = new();
        pr.Add2("CoEmpresa", c.CoEmpresa, DbType.Int32);
        pr.Add2("CoModulo", c.CoModulo, DbType.Int32);
        string sql = $"SELECT * FROM {_schema}.fn_modulousuario_sel_all_by_modulo(@CoEmpresa@CoModulo)";
        return await _connection.QueryAsync<ModuloUsuarioModel>(sql, pr);
    }
    public async Task<IEnumerable<ModuloUsuarioModel>> SelAllByPersonaAsync(ModuloUsuarioFilterDto c)
    {
        DynamicParameters pr = new();
        pr.Add2("CoEmpresa", c.CoEmpresa, DbType.Int32);
        pr.Add2("CoPersona", c.CoPersona, DbType.Int32);
        string sql = $"SELECT * FROM {_schema}.fn_modulousuario_sel_all_by_persona(@CoEmpresa,@CoPersona)";
        return await _connection.QueryAsync<ModuloUsuarioModel>(sql, pr);
    }
    public async Task<ModuloUsuarioModel?> SelByIdAsync(ModuloUsuarioFilterDto c)
    {
        DynamicParameters pr = new();
        pr.Add2("CoEmpresa", c.CoEmpresa, DbType.Int32);
        pr.Add2("CoModuloUsuario", c.CoModuloUsuario, DbType.Int32);
        string sql = $"SELECT * FROM {_schema}.fn_modulousuario_sel_by_id(@CoEmpresa,@CoModuloUsuario)";
        return await _connection.QueryFirstOrDefaultAsync<ModuloUsuarioModel>(sql, pr);
    }
    public async Task<ModuloUsuarioModel?> SelByModuloAndPersonaAsync(ModuloUsuarioFilterDto c)
    {
        DynamicParameters pr = new();
        pr.Add2("CoEmpresa", c.CoEmpresa, DbType.Int32);
        pr.Add2("CoModulo", c.CoModulo, DbType.Int32);
        pr.Add2("CoPersona", c.CoPersona, DbType.Int32);
        string sql = $"SELECT * FROM {_schema}.fn_modulousuario_sel_by_modulo_and_persona(@CoEmpresa,@CoModulo,@CoPersona)";
        return await _connection.QueryFirstOrDefaultAsync<ModuloUsuarioModel>(sql, pr);
    }

}