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

public interface ITipoParticularData
{
    Task<int> InsertAsync(TipoParticularModel c);
    Task UpdateAsync(TipoParticularModel c);
    Task UpdateStateAsync(TipoParticularModel c);
    Task UpdateCommandAsync(TipoParticularModel c);
    Task UpdateDefaultAsync(TipoParticularModel c);
    Task DeleteByIdAsync(TipoParticularModel c);
    Task<int> SelOrdenNextAsync(TipoParticularModel c);
    Task<TipoParticularModel?> SelByIdAsync(TipoParticularModel c);
    Task<TipoParticularModel?> SelByIdGrupoAsync(TipoParticularModel c);
    Task<TipoParticularModel?> SelDefaultAsync(TipoParticularModel c);
    Task<IEnumerable<TipoParticularModel>> SelAllAsync(TipoParticularDto c);
    Task<int> SelAllCountAsync(TipoParticularDto c);
    Task<IEnumerable<TipoParticularModel>> SelAllHijoAsync(TipoParticularModel c);
    Task<IEnumerable<TipoParticularModel>> SelAllGrupoByModuloAsync(TipoParticularModel c);
    Task<IEnumerable<TipoParticularModel>> SelAllActiveAsync(TipoParticularModel c);
    Task<IEnumerable<TipoParticularModel>> SelAllActiveHijoAsync(TipoParticularModel c);
    Task<IEnumerable<TipoParticularModel>> SelAllActiveHijoByGrupoAsync(TipoParticularModel c);
}

public partial class TipoParticularData : ITipoParticularData
{
    private readonly string _schema = EnuCommon.BdDefault.Schema.Maestro;
    private readonly IDbConnection _connection;
    private readonly UserContext _user;
    public TipoParticularData(IDbConnection connection, UserContext user)
    {
        _connection = connection;
        _user = user;
    }
    //   IN p_coempresa integer,
    //IN p_cotipo integer,
    //IN p_cogrupo integer,
    //IN p_notipo character varying,
    //   IN p_nosigla character varying,
    //IN p_txdescripcion character varying,
    //   IN p_nuorden integer,
    //   IN p_fldefault integer,
    //   IN p_nocomando character varying,
    //IN p_novalor character varying,
    //   IN p_cousumod integer)
    public async Task<int> InsertAsync(TipoParticularModel c)
    {
        (DynamicParameters pr, string args) = BuildDinamicParameter.Build(c, _user.CoEmpresa, _user.CoPersona,
            "CoEmpresa",
            "CoModulo",
            "CoGrupo",
            "NoTipo",
            "NoSigla",
            "TxDescripcion",
            "CoTipoPadre",
            "CoGrupoPadre",
            "NuOrden",
            "FlDefault",
            "NoComando",
            "NoValor",
            "CoUsuCre"
        );
        string sql = $"SELECT {_schema}.fn_tipoparticular_insert({args})";
        return await _connection.ExecuteScalarAsync<int>(sql, pr);
    }
    public async Task UpdateAsync(TipoParticularModel c)
    {
        (DynamicParameters pr, string args) = BuildDinamicParameter.Build(c, _user.CoEmpresa, _user.CoPersona,
            "CoEmpresa",
            "CoTipo",
            "CoGrupo",
            "NoTipo",
            "NoSigla",
            "TxDescripcion",
            "NuOrden",
            "FlDefault",
            "NoComando",
            "NoValor",
            "CoUsuMod"
        );
        string sql = $"CALL {_schema}.usp_tipoparticular_update({args})";
        await _connection.ExecuteScalarAsync(sql, pr);
    }
    public async Task DeleteByIdAsync(TipoParticularModel c)
    {
        (DynamicParameters pr, string args) = BuildDinamicParameter.Build(c, _user.CoEmpresa, _user.CoPersona,
            "CoEmpresa",
            "CoTipo",
            "CoGrupo",
            "CoUsuEli"
        );
        string sql = $"CALL {_schema}.usp_tipoparticular_delete_by_id({args})";
        await _connection.ExecuteScalarAsync(sql, pr);
    }

    public async Task UpdateStateAsync(TipoParticularModel c)
    {
        (DynamicParameters pr, string args) = BuildDinamicParameter.Build(c, _user.CoEmpresa, _user.CoPersona,
            "CoEmpresa",
            "CoTipo",
            "CoGrupo",
            "FlEstReg",
            "CoUsuMod"
        );
        string sql = $"CALL {_schema}.usp_tipoparticular_update_state({args})";
        await _connection.ExecuteScalarAsync(sql, pr);
    }

    public async Task UpdateCommandAsync(TipoParticularModel c)
    {
        (DynamicParameters pr, string args) = BuildDinamicParameter.Build(c, _user.CoEmpresa, _user.CoPersona,
            "CoEmpresa",
            "CoTipo",
            "CoGrupo",
            "NoComando",
            "CoUsuMod"
        );
        string sql = $"CALL {_schema}.usp_tipoparticular_update_command({args})";
        await _connection.ExecuteScalarAsync(sql, pr);
    }

    public async Task<IEnumerable<TipoParticularModel>> SelAllAsync(TipoParticularDto c)
    {
        (DynamicParameters pr, string args) = BuildDinamicParameter.Build(c, _user.CoEmpresa, _user.CoPersona,
            "CoEmpresa",
            "CoGrupo",
            "NoInputSearch",
            "FlEstReg",
            "CoTipo",
            "PageSize",
            "PageIndex"
        );
        string sql = $"SELECT * FROM {_schema}.fn_tipoparticular_sel_all({args})";
        return await _connection.QueryAsync<TipoParticularModel>(sql, pr);
    }

    public async Task<int> SelAllCountAsync(TipoParticularDto c)
    {
        (DynamicParameters pr, string args) = BuildDinamicParameter.Build(c, _user.CoEmpresa, _user.CoPersona,
           "CoEmpresa",
            "CoGrupo",
            "NoInputSearch",
            "FlEstReg",
            "CoTipo"
        );
        string sql = @$"SELECT * FROM {_schema}.fn_tipoparticular_sel_all_count({args})";
        return await _connection.ExecuteScalarAsync<int>(sql, pr);
    }


    public async Task<IEnumerable<TipoParticularModel>> SelAllHijoAsync(TipoParticularModel c)
    {
        (DynamicParameters pr, string args) = BuildDinamicParameter.Build(c, _user.CoEmpresa, _user.CoPersona,
            "CoEmpresa",
            "CoGrupoPadre",
            "CoTipoPadre",
            "NoInputSearch",
            "FlEstReg",
            "CoTipo",
            "PageSize",
            "PageIndex"
        );
        string sql = $"SELECT * FROM {_schema}.fn_tipoparticular_sel_all_hijo({args})";
        return await _connection.QueryAsync<TipoParticularModel>(sql, pr);
    }
    public async Task<TipoParticularModel?> SelByIdAsync(TipoParticularModel c)
    {
        (DynamicParameters pr, string args) = BuildDinamicParameter.Build(c, _user.CoEmpresa, _user.CoPersona,
            "CoEmpresa",
            "CoGrupo",
            "CoTipo"
        );
        string sql = $"SELECT * FROM {_schema}.fn_tipoparticular_sel_by_id({args})";
        return await _connection.QueryFirstOrDefaultAsync<TipoParticularModel>(sql, pr);
    }
    public async Task<int> SelOrdenNextAsync(TipoParticularModel c)
    {
        (DynamicParameters pr, string args) = BuildDinamicParameter.Build(c, _user.CoEmpresa, _user.CoPersona,
            "CoEmpresa",
            "CoGrupo",
            "CoTipoPadre"
        );
        string sql = $"SELECT * FROM {_schema}.fn_tipoparticular_sel_orden_next({args})";
        return await _connection.ExecuteScalarAsync<int>(sql, pr);
    }
    public async Task<TipoParticularModel?> SelByIdGrupoAsync(TipoParticularModel c)
    {
        (DynamicParameters pr, string args) = BuildDinamicParameter.Build(c, _user.CoEmpresa, _user.CoPersona,
            "CoGrupo"
        );
        string sql = $"SELECT * FROM {_schema}.fn_tipoparticular_sel_by_grupo({args})";
        return await _connection.QueryFirstOrDefaultAsync<TipoParticularModel>(sql, pr);
    }
    public async Task<IEnumerable<TipoParticularModel>> SelAllActiveAsync(TipoParticularModel c)
    {
        (DynamicParameters pr, string args) = BuildDinamicParameter.Build(c, _user.CoEmpresa, _user.CoPersona,
            "CoEmpresa",
            "CoGrupo",
            "NoInputSearch"
        );
        string sql = $"SELECT * FROM {_schema}.fn_tipoparticular_sel_all_active({args})";
        return await _connection.QueryAsync<TipoParticularModel>(sql, pr);
    }
    public async Task<IEnumerable<TipoParticularModel>> SelAllActiveHijoAsync(TipoParticularModel c)
    {
        (DynamicParameters pr, string args) = BuildDinamicParameter.Build(c, _user.CoEmpresa, _user.CoPersona,
            "CoEmpresa",
            "CoGrupo",
            "CoTipoPadre",
            "NoInputSearch"
        );
        string sql = $"SELECT * FROM {_schema}.fn_tipoparticular_sel_all_active_hijo({args})";
        return await _connection.QueryAsync<TipoParticularModel>(sql, pr);
    }
    public async Task<IEnumerable<TipoParticularModel>> SelAllActiveHijoByGrupoAsync(TipoParticularModel c)
    {
        (DynamicParameters pr, string args) = BuildDinamicParameter.Build(c, _user.CoEmpresa, _user.CoPersona,
            "CoEmpresa",
            "CoGrupo",
            "NoInputSearch"
        );
        string sql = $"SELECT * FROM {_schema}.fn_tipoparticular_sel_all_active_hijo_by_grupo({args})";
        return await _connection.QueryAsync<TipoParticularModel>(sql, pr);
    }
    public async Task<IEnumerable<TipoParticularModel>> SelAllGrupoByModuloAsync(TipoParticularModel c)
    {
        (DynamicParameters pr, string args) = BuildDinamicParameter.Build(c, _user.CoEmpresa, _user.CoPersona,
            "CoEmpresa",
            "CoModulo"
        );
        string sql = $"SELECT * FROM {_schema}.fn_tipoparticular_sel_all_grupo_by_modulo({args})";
        return await _connection.QueryAsync<TipoParticularModel>(sql, pr);
    }
    public async Task UpdateDefaultAsync(TipoParticularModel c)
    {
        (DynamicParameters pr, string args) = BuildDinamicParameter.Build(c, _user.CoEmpresa, _user.CoPersona,
            "CoEmpresa",
            "CoTipo",
            "CoGrupo",
            "FlDefault",
            "CoUsuEli"
        );
        string sql = $"CALL {_schema}.usp_tipoparticular_update_default({args})";
        await _connection.ExecuteScalarAsync(sql, pr);
    }
    public async Task<TipoParticularModel?> SelDefaultAsync(TipoParticularModel c)
    {
        (DynamicParameters pr, string args) = BuildDinamicParameter.Build(c, _user.CoEmpresa, _user.CoPersona,
            "CoEmpresa",
            "CoGrupo"
        );
        string sql = $"SELECT * FROM {_schema}.fn_tipoparticular_sel_default({args})";
        return await _connection.QueryFirstOrDefaultAsync<TipoParticularModel>(sql, pr);
    }
}
