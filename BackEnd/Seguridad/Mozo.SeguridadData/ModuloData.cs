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

public interface IModuloData
{
    Task<int> InsertAsync(ModuloModel c);
    Task UpdateAsync(ModuloModel c);
    Task UpdateStateAsync(ModuloModel c);
    Task DeleteByIdAsync(ModuloFilterDto c);
    Task<IEnumerable<ModuloModel>> SelAllAsync(ModuloFilterDto c);
    Task<IEnumerable<ModuloModel>> SelAllActiveAsync();
    Task<IEnumerable<ModuloModel>> SelAllActiveAreaAsync();

    Task<IEnumerable<ModuloModel>> SelAllActiveForEmpresaAsync();
    Task<ModuloModel?> SelByIdAsync(ModuloFilterDto c);
}
public partial class ModuloData : IModuloData
{
    private readonly string _schema = EnuCommon.BdDefault.Schema.Seguridad;
    private readonly IDbConnection _connection;
    private readonly UserContext _user;
    public ModuloData(IDbConnection connection, UserContext user)
    {
        _connection = connection;
        _user = user;
    }
    public async Task<int> InsertAsync(ModuloModel c)
    {
        DynamicParameters pr = new();
        pr.Add2("NoModulo", c.NoModulo, DbType.String);
        pr.Add2("NoArea", c.NoArea, DbType.String);
        pr.Add2("NoModuloDescripcion", c.NoModuloDescripcion, DbType.String);
        pr.Add2("NuOrden", c.NuOrden, DbType.Int32);
        pr.Add2("NoIcono", c.NoIcono, DbType.String);
        pr.Add2("FlArea", c.FlArea, DbType.Int32);
        pr.Add2("CoUsuCre", _user.CoPersona, DbType.Int32);

        string sql = $"SELECT {_schema}.fn_modulo_insert(@NoModulo,@NoArea,@NoModuloDescripcion,@NuOrden,@NoIcono,@FlArea,@CoUsuCre)";
        return await _connection.ExecuteScalarAsync<int>(sql, pr);
    }
    public async Task UpdateAsync(ModuloModel c)
    {
        DynamicParameters pr = new();
        pr.Add2("CoModulo", c.CoModulo, DbType.Int32);
        pr.Add2("NoModulo", c.NoModulo, DbType.String);
        pr.Add2("NoArea", c.NoArea, DbType.String);
        pr.Add2("NoModuloDescripcion", c.NoModuloDescripcion, DbType.String);
        pr.Add2("NuOrden", c.NuOrden, DbType.Int32);
        pr.Add2("NoIcono", c.NoIcono, DbType.String);
        pr.Add2("FlArea", c.FlArea, DbType.Int32);
        pr.Add2("CoUsuMod", _user.CoPersona, DbType.Int32);

        string sql = $"CALL {_schema}.usp_modulo_update(@CoModulo,@NoModulo,@NoArea,@NoModuloDescripcion,@NuOrden,@NoIcono,@FlArea,@CoUsuMod)";
        await _connection.ExecuteScalarAsync(sql, pr);
    }

    public async Task UpdateStateAsync(ModuloModel c)
    {
        DynamicParameters pr = new();
        pr.Add2("CoModulo", c.CoModulo, DbType.Int32);
        pr.Add2("FlEstReg", c.FlEstReg, DbType.Int32);
        pr.Add2("CoUsuMod", _user.CoPersona, DbType.Int32);

        string sql = $"CALL {_schema}.usp_modulo_update_state(@CoModulo,@FlEstReg,@CoUsuMod)";
        await _connection.ExecuteScalarAsync(sql, pr);
    }
    public async Task DeleteByIdAsync(ModuloFilterDto c)
    {
        DynamicParameters pr = new();
        pr.Add2("CoModulo", c.CoModulo, DbType.Int32);
        pr.Add2("CoUsuEli", _user.CoPersona, DbType.Int32);

        string sql = $"CALL {_schema}.usp_modulo_delete_by_id(@CoModulo,@CoUsuEli)";
        await _connection.ExecuteScalarAsync(sql, pr);
    }
    public async Task<IEnumerable<ModuloModel>> SelAllAsync(ModuloFilterDto c)
    {
        DynamicParameters pr = new();
        pr.Add2("CoModulo", c.CoModulo, DbType.Int32);
        string sql = $"SELECT * FROM {_schema}.fn_modulo_sel_all(@CoModulo)";
        return await _connection.QueryAsync<ModuloModel>(sql, pr);
    }
    public async Task<IEnumerable<ModuloModel>> SelAllActiveAsync()
    {
        string sql = $"SELECT * FROM {_schema}.fn_modulo_sel_all_active()";
        return await _connection.QueryAsync<ModuloModel>(sql);
    }
    public async Task<IEnumerable<ModuloModel>> SelAllActiveAreaAsync()
    {
        string sql = $"SELECT * FROM {_schema}.fn_modulo_sel_all_active_area()";
        return await _connection.QueryAsync<ModuloModel>(sql);
    }

    public async Task<IEnumerable<ModuloModel>> SelAllActiveForEmpresaAsync()
    {
        string sql = $"SELECT * FROM {_schema}.fn_modulo_sel_all_active_for_empresa()";
        return await _connection.QueryAsync<ModuloModel>(sql);
    }
    public async Task<ModuloModel?> SelByIdAsync(ModuloFilterDto c)
    {
        DynamicParameters pr = new();
        pr.Add2("CoModulo", c.CoModulo, DbType.Int32);
        string sql = $"SELECT * FROM {_schema}.fn_modulo_sel_by_id(@CoModulo)";
        return await _connection.QueryFirstOrDefaultAsync<ModuloModel>(sql, pr);
    }

}