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

public interface IRedirectData
{
    Task<int> InsertAsync(RedirectModel c);
    Task DeleteByIdAsync(RedirectFilterDto c);
    Task<RedirectModel?> SelByIdAsync(RedirectFilterDto c);
}
public partial class RedirectData : IRedirectData
{
    private readonly string _schema = EnuCommon.BdDefault.Schema.Login;

    private readonly IDbConnection _connection;
    private readonly UserContext _user;
    public RedirectData(IDbConnection connection, UserContext user)
    {
        _connection = connection;
        _user = user;
    }
    public async Task<int> InsertAsync(RedirectModel c)
    {
        DynamicParameters pr = new();
        pr.Add2("CoEmpresa", _user.CoEmpresa, DbType.Int32);
        pr.Add2("CoPermiso", c.CoPermiso, DbType.Int32);
        pr.Add2("CoPersona", _user.CoPersona, DbType.Int32);
        pr.Add2("CoModulo", c.CoModulo, DbType.Int32);
        pr.Add2("FeRedirect", c.FeRedirect, DbType.String);

        string sql = $"SELECT {_schema}.fn_redirect_insert(@CoEmpresa,@CoPermiso,@CoPersona,@CoModulo,@FeRedirect)";
        return await _connection.ExecuteScalarAsync<int>(sql, pr);
    }
    public async Task DeleteByIdAsync(RedirectFilterDto c)
    {
        DynamicParameters pr = new();
        pr.Add2("CoEmpresa", _user.CoEmpresa, DbType.Int32);
        pr.Add2("CoRedirect", c.CoRedirect, DbType.Int32);
        string sql = $"SELECT {_schema}.usp_redirect_delete_by_id(@CoEmpresa,@CoRedirect)";
        await _connection.ExecuteScalarAsync(sql, pr);
    }
    public async Task<RedirectModel?> SelByIdAsync(RedirectFilterDto c)
    {
        DynamicParameters pr = new();
        pr.Add2("CoEmpresa", _user.CoEmpresa, DbType.Int32);
        pr.Add2("CoRedirect", c.CoRedirect, DbType.Int32);
        string sql = $"SELECT * FROM {_schema}.fn_redirect_sel_by_id(@CoEmpresa,@CoRedirect)";
        return await _connection.QueryFirstOrDefaultAsync<RedirectModel>(sql, pr);
    }

}