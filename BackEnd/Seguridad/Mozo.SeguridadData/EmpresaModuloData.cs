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

public interface IEmpresaModuloData
{
    Task<int> InsertAsync(EmpresaModuloModel c);
    Task UpdateAsync(EmpresaModuloModel c);
    Task DeleteByIdAsync(EmpresaModuloFilterDto c);

    Task<EmpresaModuloModel?> SelByIdAsync(EmpresaModuloFilterDto c);
    Task<IEnumerable<EmpresaModuloModel>> SelAllAsync(EmpresaModuloFilterDto c);

}
public partial class EmpresaModuloData : IEmpresaModuloData
{

    private readonly string _schema = EnuCommon.BdDefault.Schema.Seguridad;
    private readonly IDbConnection _connection;
    private readonly UserContext _user;
    public EmpresaModuloData(IDbConnection connection, UserContext user)
    {
        _connection = connection;
        _user = user;
    }

    public async Task<int> InsertAsync(EmpresaModuloModel c)
    {
        DynamicParameters pr = new();
        pr.Add2("CoEmpresa", c.CoEmpresa, DbType.Int32);
        pr.Add2("CoModulo", c.CoModulo, DbType.Int32);
        string sql = $"SELECT {_schema}.fn_empresamodulo_insert(@CoEmpresa,@CoModulo)";
        return await _connection.ExecuteScalarAsync<int>(sql, pr);
    }

    public async Task UpdateAsync(EmpresaModuloModel c)
    {
        DynamicParameters pr = new();
        pr.Add2("CoEmpresaModulo", c.CoEmpresaModulo, DbType.Int32);
        pr.Add2("CoEmpresa", c.CoEmpresa, DbType.Int32);
        pr.Add2("CoModulo", c.CoModulo, DbType.Int32);

        string sql = $"CALL {_schema}.usp_empresamodulo_update(@CoEmpresaModulo,@CoEmpresa,@CoModulo)";
        await _connection.ExecuteScalarAsync(sql, pr);
    }
    public async Task<EmpresaModuloModel?> SelByIdAsync(EmpresaModuloFilterDto c)
    {
        DynamicParameters pr = new();
        pr.Add2("CoEmpresaModulo", c.CoEmpresaModulo, DbType.Int32);
        string sql = $"SELECT * FROM {_schema}.fn_empresamodulo_sel_by_id(@CoEmpresaModulo)";
        return await _connection.QueryFirstOrDefaultAsync<EmpresaModuloModel>(sql, pr);
    }

    public async Task<IEnumerable<EmpresaModuloModel>> SelAllAsync(EmpresaModuloFilterDto c)
    {
        DynamicParameters pr = new();
        pr.Add2("CoEmpresa", c.CoEmpresa, DbType.Int32);

        string sql = $"SELECT * FROM {_schema}.fn_empresamodulo_sel_all(@CoEmpresa)";
        return await _connection.QueryAsync<EmpresaModuloModel>(sql, pr);
    }
    public async Task DeleteByIdAsync(EmpresaModuloFilterDto c)
    {
        DynamicParameters pr = new();
        pr.Add2("CoEmpresaModulo", c.CoEmpresaModulo, DbType.Int32);
        pr.Add2("CoUsuEli", _user.CoPersona, DbType.Int32);
        string sql = $"CALL {_schema}.usp_empresamodulo_delete_by_id(@CoEmpresaModulo,@CoUsuEli)";
        await _connection.ExecuteScalarAsync(sql, pr);
    }


}