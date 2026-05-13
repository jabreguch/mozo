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

public interface IPerfilData
{
    Task<int> InsertAsync(PerfilModel c);
    Task UpdateAsync(PerfilModel c);
    Task UpdateStateAsync(PerfilModel c);
    Task DeleteByIdAsync(PerfilFilterDto c);
    Task<PerfilModel?> SelByIdAsync(PerfilFilterDto c);
    Task<IEnumerable<PerfilModel>> SelAllAsync(PerfilFilterDto c);
    Task<IEnumerable<PerfilModel>> SelAllActiveAsync(PerfilFilterDto c);
    Task<PerfilModel?> SelDefaultAsync(PerfilFilterDto c);
}

public partial class PerfilData : IPerfilData
{
    private readonly string _schema = EnuCommon.BdDefault.Schema.Seguridad;

    private readonly IDbConnection _connection;
    private readonly UserContext _user;
    public PerfilData(IDbConnection connection, UserContext user)
    {
        _connection = connection;
        _user = user;
    }

    public async Task<int> InsertAsync(PerfilModel c)
    {
        DynamicParameters pr = new();
        pr.Add2("CoModulo", c.CoModulo, DbType.Int32);
        pr.Add2("NoPerfil", c.NoPerfil, DbType.String);
        pr.Add2("FlDefault", c.FlDefault, DbType.Int32);
        pr.Add2("CoUsuCre", _user.CoPersona, DbType.Int32);

        string sql = $@"SELECT {_schema}.fn_perfil_insert(
            @CoModulo,
            @NoPerfil,
            @FlDefault,
            @CoUsuCre
        )";
        return await _connection.ExecuteScalarAsync<int>(sql, pr);
    }
    public async Task UpdateAsync(PerfilModel c)
    {
        DynamicParameters pr = new();
        pr.Add2("CoModulo", c.CoModulo, DbType.Int32);
        pr.Add2("CoPerfil", c.CoPerfil, DbType.Int32);
        pr.Add2("NoPerfil", c.NoPerfil, DbType.String);
        pr.Add2("FlDefault", c.FlDefault, DbType.Int32);
        pr.Add2("CoUsuMod", _user.CoPersona, DbType.Int32);

        string sql = $@"CALL {_schema}.usp_perfil_update(
            @CoModulo,
            @CoPerfil,
            @NoPerfil,
            @FlDefault,
            @CoUsuMod
        )";
        await _connection.ExecuteScalarAsync(sql, pr);
    }
    public async Task UpdateStateAsync(PerfilModel c)
    {
        DynamicParameters pr = new();
        pr.Add2("CoModulo", c.CoModulo, DbType.Int32);
        pr.Add2("CoPerfil", c.CoPerfil, DbType.Int32);
        pr.Add2("FlEstReg", c.FlEstReg, DbType.Int32);
        pr.Add2("CoUsuMod", _user.CoPersona, DbType.Int32);

        string sql = $@"CALL {_schema}.usp_perfil_update_state(
            @CoModulo,
            @CoPerfil,
            @FlEstReg,
            @CoUsuMod
        )";
        await _connection.ExecuteScalarAsync(sql, pr);
    }
    public async Task DeleteByIdAsync(PerfilFilterDto c)
    {
        DynamicParameters pr = new();
        pr.Add2("CoPerfil", c.CoPerfil, DbType.Int32);
        pr.Add2("CoUsuEli", _user.CoPersona, DbType.Int32);

        string sql = $"CALL {_schema}.usp_perfil_delete_by_id(@CoPerfil,@CoUsuEli)";
        await _connection.ExecuteScalarAsync(sql, pr);
    }
    public async Task<IEnumerable<PerfilModel>> SelAllAsync(PerfilFilterDto c)
    {
        DynamicParameters pr = new();
        pr.Add2("CoModulo", c.CoModulo, DbType.Int32);
        pr.Add2("CoPerfil", c.CoPerfil, DbType.Int32);

        string sql = $"SELECT * FROM {_schema}.fn_perfil_sel_all(@CoModulo,@CoPerfil)";
        return await _connection.QueryAsync<PerfilModel>(sql, pr);
    }
    public async Task<IEnumerable<PerfilModel>> SelAllActiveAsync(PerfilFilterDto c)
    {
        DynamicParameters pr = new();
        pr.Add2("CoModulo", c.CoModulo, DbType.Int32);

        string sql = $"SELECT * FROM {_schema}.fn_perfil_sel_all_active(@CoModulo)";
        return await _connection.QueryAsync<PerfilModel>(sql, pr);
    }
    public async Task<PerfilModel?> SelByIdAsync(PerfilFilterDto c)
    {
        DynamicParameters pr = new();
        pr.Add2("CoModulo", c.CoModulo, DbType.Int32);
        pr.Add2("CoPerfil", c.CoPerfil, DbType.Int32);

        string sql = $"SELECT * FROM {_schema}.fn_perfil_sel_by_id(@CoModulo,@CoPerfil)";
        return await _connection.QueryFirstOrDefaultAsync<PerfilModel>(sql, pr);
    }
    public async Task<PerfilModel?> SelDefaultAsync(PerfilFilterDto c)
    {
        DynamicParameters pr = new();
        pr.Add2("CoModulo", c.CoModulo, DbType.Int32);

        string sql = $"SELECT * FROM {_schema}.fn_perfil_sel_default(@CoModulo)";
        return await _connection.QueryFirstOrDefaultAsync<PerfilModel>(sql, pr);
    }
}