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

public interface IIngresoData
{
    Task<int> InsertAsync(IngresoModel c);
    Task UpdateCloseTokenAsync(IngresoModel c);
    Task<IngresoModel?> SelByIdAsync(IngresoFilterDto c);

}
public partial class IngresoData : IIngresoData
{

    private readonly string _schema = EnuCommon.BdDefault.Schema.Login;
    private readonly IDbConnection _connection;
    private readonly UserContext _user;
    public IngresoData(IDbConnection connection, UserContext user)
    {
        _connection = connection;
        _user = user;
    }
    public async Task<int> InsertAsync(IngresoModel c)
    {
        DynamicParameters pr = new();
        pr.Add2("CoEmpresa", c.CoEmpresa, DbType.Int32);
        pr.Add2("CoPermiso", c.CoPermiso, DbType.Int32);
        pr.Add2("CoPersona", c.CoPersona, DbType.Int32);
        pr.Add2("NoIp", c.NoIp, DbType.String);
        pr.Add2("NoRefreshToken", c.NoRefreshToken, DbType.String);

        string sql = $"SELECT {_schema}.fn_ingreso_insert(@CoEmpresa,@CoPermiso,@CoPersona,@NoIp,@NoRefreshToken)";
        return await _connection.ExecuteScalarAsync<int>(sql, pr);
    }
    public async Task<IngresoModel?> SelByIdAsync(IngresoFilterDto c)
    {
        DynamicParameters pr = new();
        pr.Add2("CoEmpresa", _user.CoEmpresa, DbType.Int32);
        pr.Add2("CoIngreso", c.CoIngreso, DbType.Int32);
        string sql = $"SELECT * FROM {_schema}.fn_ingreso_sel_by_id(@CoEmpresa,@CoIngreso)";
        return await _connection.QueryFirstOrDefaultAsync<IngresoModel>(sql, pr);
    }

    public async Task UpdateCloseTokenAsync(IngresoModel c)
    {
        DynamicParameters pr = new();
        pr.Add2("CoEmpresa", _user.CoEmpresa, DbType.Int32);
        pr.Add2("CoIngreso", c.CoIngreso, DbType.Int32);
        pr.Add2("NoRefreshTokenPrevious", c.NoRefreshTokenPrevious, DbType.String);

        string sql = $"CALL {_schema}.usp_ingreso_update_close_token(@CoEmpresa,@CoIngreso,@NoRefreshTokenPrevious)";
        await _connection.ExecuteScalarAsync(sql, pr);
    }

}