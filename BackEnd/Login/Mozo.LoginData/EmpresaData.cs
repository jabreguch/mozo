
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

public interface IEmpresaData
{
    Task<EmpresaModel?> SelByIdAsync(EmpresaFilterDto c);
    Task<IEnumerable<EmpresaModel>> SelAllAsync();
}
public partial class EmpresaData : IEmpresaData
{
    private readonly string _schema = EnuCommon.BdDefault.Schema.Login;
    private readonly IDbConnection _connection;
    private readonly UserContext _user;
    public EmpresaData(IDbConnection connection, UserContext user)
    {
        _connection = connection;
        _user = user;
    }
    public async Task<EmpresaModel?> SelByIdAsync(EmpresaFilterDto c)
    {
        DynamicParameters pr = new();
        pr.Add2("CoEmpresa", _user.CoEmpresa, DbType.Int32);
        pr.Add2("NoEmpresaCorto", c.NoEmpresaCorto, DbType.String);
        pr.Add2("NuDocumento", c.NuDocumento, DbType.String);
        string sql = $"SELECT * FROM {_schema}.fn_empresa_sel_by_id(@CoEmpresa,@NoEmpresaCorto,@NuDocumento)";
        return await _connection.QueryFirstOrDefaultAsync<EmpresaModel>(sql, pr);
    }
    public async Task<IEnumerable<EmpresaModel>> SelAllAsync()
    {
        string sql = $"SELECT * FROM {_schema}.fn_empresa_sel_all()";
        return await _connection.QueryAsync<EmpresaModel>(sql);
    }

}