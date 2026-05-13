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

public interface IPermisoData
{
    Task<PermisoModel?> SelByUserAsync(PermisoFilterDto c);
    Task<PermisoModel?> SelByIdAsync(PermisoFilterDto c);
    Task UpdateLanguageAsync(PermisoModel c);
}

public partial class PermisoData : IPermisoData
{
    private readonly string _schema = EnuCommon.BdDefault.Schema.Login;
    private readonly IDbConnection _connection;
    private readonly UserContext _user;
    public PermisoData(IDbConnection connection, UserContext user)
    {
        _connection = connection;
        _user = user;
    }
    public async Task<PermisoModel?> SelByUserAsync(PermisoFilterDto c)
    {
        DynamicParameters pr = new();
        pr.Add2("NoUsuario", c.NoUsuario, DbType.String);
        pr.Add2("NoClave", c.NoClave, DbType.String);

        string sql = $"SELECT * FROM {_schema}.fn_permiso_sel_by_user(@NoUsuario,@NoClave)";
        PermisoModel? i = await _connection.QueryFirstOrDefaultAsync<PermisoModel>(sql, pr);

        return i;
    }

    public async Task<PermisoModel?> SelByIdAsync(PermisoFilterDto c)
    {
        DynamicParameters pr = new();
        pr.Add2("CoEmpresa", _user.CoEmpresa, DbType.Int32);
        pr.Add2("CoPermiso", c.CoPermiso, DbType.Int32);
        string sql = $"SELECT * FROM {_schema}.fn_permiso_sel_by_id(@CoEmpresa,@CoPermiso)";
        return await _connection.QueryFirstOrDefaultAsync<PermisoModel>(sql, pr);
    }

    public async Task UpdateLanguageAsync(PermisoModel c)
    {
        DynamicParameters pr = new();
        pr.Add2("CoEmpresa", _user.CoEmpresa, DbType.Int32);
        pr.Add2("CoPermiso", c.CoPermiso, DbType.Int32);
        pr.Add2("CoLang", c.CoLang, DbType.Int32);

        string sql = $"CALL {_schema}.usp_permiso_update_language(@CoEmpresa,@CoPermiso,@CoLang)";
        await _connection.ExecuteScalarAsync(sql, pr);
    }

}
