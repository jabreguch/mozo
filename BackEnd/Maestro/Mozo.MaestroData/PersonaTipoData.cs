using Dapper;

using Mozo.Helper.Enu;
using Mozo.Helper.Global;
using Mozo.Model.Maestro;

using System.Data;
///<summary>
///
///</summary>
///<remarks>
///</remarks>
///<history>
/// t[Jonatan Abregu]	26/04/2022	Created
///</history>
namespace Mozo.MaestroData;

public interface IPersonaTipoData
{
    Task<int> InsertAsync(PersonaTipoModel c, IDbTransaction? tran = null);
    Task UpdateAsync(PersonaTipoModel c);
    Task<IEnumerable<PersonaTipoModel>> SelAllActiveByPersonaAsync(PersonaTipoModel c);
    Task<IEnumerable<PersonaTipoModel>> SelAllActiveByModuloAndPersonaAsync(PersonaTipoModel c);
    Task<IEnumerable<PersonaTipoModel>> SelAllActiveAsync(PersonaTipoModel c);
}

public partial class PersonaTipoData : IPersonaTipoData
{
    private readonly string _schema = EnuCommon.BdDefault.Schema.Maestro;

    private readonly IDbConnection _connection;
    private readonly UserContext _user;
    public PersonaTipoData(IDbConnection connection, UserContext user)
    {
        _connection = connection;
        _user = user;
    }

    public async Task<int> InsertAsync(PersonaTipoModel c, IDbTransaction? tran = null)
    {
        (DynamicParameters pr, string args) = BuildDinamicParameter.Build(c, _user.CoEmpresa, _user.CoPersona,
            "CoEmpresa",
            "CoPersona",
            "CoTipo",
            "CoModulo",
            "CoUsuCre"
        );
        string sql = $"SELECT {_schema}.fn_personatipo_insert({args})";

        if (tran == null)
            return await _connection.ExecuteScalarAsync<int>(sql, pr);
        else
            return await _connection.ExecuteScalarAsync<int>(sql, pr, tran);
    }
    public async Task UpdateAsync(PersonaTipoModel c)
    {
        (DynamicParameters pr, string args) = BuildDinamicParameter.Build(c, _user.CoEmpresa, _user.CoPersona,
            "CoEmpresa",
            "CoPersonaTipo",
            "CoTipo",
            "CoUsuMod"
        );
        string sql = $"CALL {_schema}.usp_personatipo_update({args})";
        await _connection.ExecuteScalarAsync<int>(sql, pr);
    }
    public async Task<IEnumerable<PersonaTipoModel>> SelAllActiveByPersonaAsync(PersonaTipoModel c)
    {
        (DynamicParameters pr, string args) = BuildDinamicParameter.Build(c, _user.CoEmpresa, _user.CoPersona,
            "CoEmpresa",
            "CoPersona"
        );
        string sql = $"SELECT * FROM {_schema}.fn_personatipo_sel_all_active_by_persona({args})";
        return await _connection.QueryAsync<PersonaTipoModel>(sql, pr);
    }
    public async Task<IEnumerable<PersonaTipoModel>> SelAllActiveByModuloAndPersonaAsync(PersonaTipoModel c)
    {
        (DynamicParameters pr, string args) = BuildDinamicParameter.Build(c, _user.CoEmpresa, _user.CoPersona,
           "CoEmpresa",
           "CoModulo",
           "CoPersona"
       );
        string sql = $"SELECT * FROM {_schema}.fn_personatipo_sel_all_active_by_modulo_and_persona({args})";
        return await _connection.QueryAsync<PersonaTipoModel>(sql, pr);
    }
    public async Task<IEnumerable<PersonaTipoModel>> SelAllActiveAsync(PersonaTipoModel c)
    {
        (DynamicParameters pr, string args) = BuildDinamicParameter.Build(c, _user.CoEmpresa, _user.CoPersona,
            "CoEmpresa"
        );
        string sql = $"SELECT * FROM {_schema}.fn_personatipo_sel_all_active({args})";
        return await _connection.QueryAsync<PersonaTipoModel>(sql, pr);
    }
}
