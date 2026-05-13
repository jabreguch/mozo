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

public interface IPersonaData
{
    Task<int> InsertAsync(PersonaModel c, IDbTransaction? tran = null);
    Task UpdateAsync(PersonaModel c);
    Task UpdateStateAsync(PersonaModel c);
    Task DeleteByIdAsync(PersonaModel c);
    Task<PersonaModel?> SelByIdAsync(PersonaModel c);
    Task<PersonaModel?> SelRepeatAsync(PersonaModel c);
    Task<IEnumerable<PersonaModel>> SelAllAsync(PersonaModel c);
    Task<IEnumerable<PersonaModel>> SelAllActiveAsync(PersonaModel c);
}
public partial class PersonaData : IPersonaData
{
    private readonly string _schema = EnuCommon.BdDefault.Schema.Maestro;

    private readonly IDbConnection _connection;
    private readonly UserContext _user;
    public PersonaData(IDbConnection connection, UserContext user)
    {
        _connection = connection;
        _user = user;
    }



    public async Task<int> InsertAsync(PersonaModel c, IDbTransaction? tran = null)
    {
        (DynamicParameters pr, string args) = BuildDinamicParameter.Build(c, _user.CoEmpresa, _user.CoPersona,
            "CoEmpresa",
            "CoTipoDocumento",
            "NuDocumento",
            "NoPersona",
            "NoApellidoP",
            "NoApellidoM",
            "FeNacimiento",
            "NoDireccion",
            "NoReferencia",
            "NoCodigoPostal",
            "CoSexo",
            "CoEstadoCivil",
            "CoPais",
            "CoUbigeo",
            "CoRubro",
            "CoProfesion",
            "CoUsuCre"
        );
        string sql = $"SELECT {_schema}.fn_persona_insert({args})";

        if (tran == null)
            return await _connection.ExecuteScalarAsync<int>(sql, pr);
        else
            return await _connection.ExecuteScalarAsync<int>(sql, pr, tran);

    }
    public async Task UpdateAsync(PersonaModel c)
    {
        (DynamicParameters pr, string args) = BuildDinamicParameter.Build(c, _user.CoEmpresa, _user.CoPersona,
            "CoEmpresa",
            "CoPersona",
            "CoTipoDocumento",
            "NuDocumento",
            "NoPersona",
            "NoApellidoP",
            "NoApellidoM",
            "FeNacimiento",
            "NoDireccion",
            "NoReferencia",
            "NoCodigoPostal",
            "CoSexo",
            "CoEstadoCivil",
            "CoPais",
            "CoUbigeo",
            "CoRubro",
            "CoProfesion",
            "CoUsuMod"
        );
        string sql = $"CALL {_schema}.usp_persona_update({args})";
        await _connection.ExecuteScalarAsync(sql, pr);
    }
    public async Task UpdateStateAsync(PersonaModel c)
    {
        (DynamicParameters pr, string args) = BuildDinamicParameter.Build(c, _user.CoEmpresa, _user.CoPersona,
            "CoEmpresa",
            "CoPersona",
            "FlEstReg",
            "CoUsuMod"
        );
        string sql = $"CALL {_schema}.usp_persona_update_state({args})";
        await _connection.ExecuteScalarAsync(sql, pr);
    }
    public async Task DeleteByIdAsync(PersonaModel c)
    {
        (DynamicParameters pr, string args) = BuildDinamicParameter.Build(c, _user.CoEmpresa, _user.CoPersona,
            "CoEmpresa",
            "CoPersona",
            "CoUsuEli"
        );
        string sql = $"CALL {_schema}.usp_persona_delete_by_id({args})";
        await _connection.ExecuteScalarAsync(sql, pr);
    }
    public async Task<IEnumerable<PersonaModel>> SelAllAsync(PersonaModel c)
    {
        (DynamicParameters pr, string args) = BuildDinamicParameter.Build(c, _user.CoEmpresa, _user.CoPersona,
            "CoEmpresa",
            "CoModulo",
            "CoPersonaTipo",
            "CoTipoDocumento",
            "NoInputSearch",
            "FlEstReg",
            "PageSize",
            "PageIndex"
        );
        string sql = $"SELECT * FROM {_schema}.fn_persona_sel_all({args})";
        return await _connection.QueryAsync<PersonaModel>(sql, pr);
    }
    public async Task<PersonaModel?> SelByIdAsync(PersonaModel c)
    {
        (DynamicParameters pr, string args) = BuildDinamicParameter.Build(c, _user.CoEmpresa, _user.CoPersona,
            "CoEmpresa",
            "CoPersona"
        );
        string sql = $"SELECT * FROM {_schema}.fn_persona_sel_by_id({args})";
        return await _connection.QueryFirstOrDefaultAsync<PersonaModel>(sql, pr);
    }
    public async Task<PersonaModel?> SelRepeatAsync(PersonaModel c)
    {
        (DynamicParameters pr, string args) = BuildDinamicParameter.Build(c, _user.CoEmpresa, _user.CoPersona,
            "CoEmpresa",
            "CoPersona",
            "CoTipoDocumento",
            "NuDocumento"
        );
        string sql = $"SELECT * FROM {_schema}.fn_persona_sel_repeat({args})";
        return await _connection.QueryFirstOrDefaultAsync<PersonaModel>(sql, pr);
    }
    public async Task<IEnumerable<PersonaModel>> SelAllActiveAsync(PersonaModel c)
    {
        (DynamicParameters pr, string args) = BuildDinamicParameter.Build(c, _user.CoEmpresa, _user.CoPersona,
            "CoEmpresa",
            "CoModulo",
            "CoPersonaTipo",
            "NoInputSearch"
        );
        string sql = $"SELECT * FROM {_schema}.fn_persona_sel_all_active({args})";
        return await _connection.QueryAsync<PersonaModel>(sql, pr);
    }
}
