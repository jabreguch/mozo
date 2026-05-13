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

public interface IPermisoData
{
    Task<int> InsertAsync(PermisoModel c);
    Task UpdateAsync(PermisoModel c);
    Task UpdateClaveAsync(PermisoModel c);
    Task UpdateStateAsync(PermisoModel c);
    Task<PermisoModel?> SelByIdAsync(PermisoFilterDto c);
    //Task<IEnumerable<PermisoModel>> SelAllAsync(PermisoModel c);
    //Task<IEnumerable<PermisoModel>> SelAllActiveAsync(PermisoModel c);
}
public partial class PermisoData : IPermisoData
{
    private readonly string _schema = EnuCommon.BdDefault.Schema.Seguridad;
    private readonly IDbConnection _connection;
    private readonly UserContext _user;
    public PermisoData(IDbConnection connection, UserContext user)
    {
        _connection = connection;
        _user = user;
    }
    public async Task<int> InsertAsync(PermisoModel c)
    {
        DynamicParameters pr = new();
        pr.Add2("CoEmpresa", _user.CoEmpresa, DbType.Int32);
        pr.Add2("CoPersona", c.CoPersona, DbType.Int32);
        pr.Add2("NoUsuario", c.NoUsuario, DbType.String);
        pr.Add2("NoClave", c.NoClave, DbType.String);
        pr.Add2("CoUsuCre", _user.CoPersona, DbType.Int32);

        string sql = $@"SELECT {_schema}.fn_permiso_insert(
            @CoEmpresa,
            @CoPersona,
            @NoUsuario,
            @NoClave,
            @CoUsuCre
        )";
        return await _connection.ExecuteScalarAsync<int>(sql, pr);
    }
    public async Task UpdateAsync(PermisoModel c)
    {
        DynamicParameters pr = new();
        pr.Add2("CoEmpresa", _user.CoEmpresa, DbType.Int32);
        pr.Add2("CoPermiso", c.CoPermiso, DbType.Int32);
        pr.Add2("CoPersona", c.CoPersona, DbType.Int32);
        pr.Add2("NoUsuario", c.NoUsuario, DbType.String);
        pr.Add2("NoClave", c.NoClave, DbType.String);
        pr.Add2("CoUsuMod", _user.CoPersona, DbType.Int32);

        string sql = $@"CALL {_schema}.usp_perfil_update(
            @CoEmpresa,
            @CoPermiso,
            @CoPersona,
            @NoUsuario,
            @NoClave,
            @CoUsuMod
        )";
        await _connection.ExecuteScalarAsync(sql, pr);
    }
    public async Task UpdateClaveAsync(PermisoModel c)
    {
        DynamicParameters pr = new();
        pr.Add2("CoEmpresa", _user.CoEmpresa, DbType.Int32);
        pr.Add2("CoPermiso", c.CoPermiso, DbType.Int32);
        pr.Add2("NoClaveNuevo", c.NoUsuario, DbType.String);
        pr.Add2("CoUsuMod", _user.CoPersona, DbType.Int32);

        string sql = $"CALL {_schema}.usp_permiso_update_clave(@CoEmpresa,@CoPermiso,@NoClaveNuevo,@CoUsuMod)";
        await _connection.ExecuteScalarAsync(sql, pr);
    }
    public async Task UpdateStateAsync(PermisoModel c)
    {
        DynamicParameters pr = new();
        pr.Add2("CoEmpresa", _user.CoEmpresa, DbType.Int32);
        pr.Add2("CoPermiso", c.CoPermiso, DbType.Int32);
        pr.Add2("FlEstReg", c.FlEstReg, DbType.Int32);
        pr.Add2("CoUsuMod", _user.CoPersona, DbType.Int32);

        string sql = $"CALL {_schema}.usp_permiso_update_state(@CoEmpresa,@CoPermiso,@FlEstReg,@CoUsuMod)";
        await _connection.ExecuteScalarAsync(sql, pr);
    }

    public async Task<PermisoModel?> SelByIdAsync(PermisoFilterDto c)
    {
        DynamicParameters pr = new();
        pr.Add2("CoEmpresa", _user.CoEmpresa, DbType.Int32);
        pr.Add2("CoPersona", c.CoPermiso, DbType.Int32);

        string sql = $"SELECT * FROM {_schema}.fn_permiso_sel_by_id(@CoEmpresa,@CoPersona)";
        return await _connection.QueryFirstOrDefaultAsync<PermisoModel>(sql, pr);
    }

}