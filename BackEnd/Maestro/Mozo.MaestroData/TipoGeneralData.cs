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

public interface ITipoGeneralData
{
    Task<IEnumerable<TipoGeneralModel>> SelAllActiveAsync(TipoGeneralFilterDto c);
    Task<IEnumerable<TipoGeneralModel>> SelAllActiveByModuloAsync(TipoGeneralFilterDto c);
    Task<TipoGeneralModel?> SelByIdAsync(TipoGeneralFilterDto c);
}
public partial class TipoGeneralData : ITipoGeneralData
{
    private readonly string _schema = EnuCommon.BdDefault.Schema.Maestro;

    private readonly IDbConnection _connection;
    private readonly UserContext _user;
    public TipoGeneralData(IDbConnection connection, UserContext user)
    {
        _connection = connection;
        _user = user;
    }
    public async Task<IEnumerable<TipoGeneralModel>> SelAllActiveAsync(TipoGeneralFilterDto c)
    {
        DynamicParameters pr = new();
        pr.Add2("CoGrupo", c.CoGrupo, DbType.Int32);

        string sql = $"SELECT * FROM {_schema}.fn_tipogeneral_sel_all_active(@CoGrupo)";
        return await _connection.QueryAsync<TipoGeneralModel>(sql, pr);
    }
    public async Task<IEnumerable<TipoGeneralModel>> SelAllActiveByModuloAsync(TipoGeneralFilterDto c)
    {
        DynamicParameters pr = new();
        pr.Add2("CoModulo", c.CoModulo, DbType.Int32);
        pr.Add2("CoGrupo", c.CoGrupo, DbType.Int32);

        string sql = $"SELECT * FROM {_schema}.fn_tipogeneral_sel_all_active_by_modulo(@CoModulo,@CoGrupo)";
        return await _connection.QueryAsync<TipoGeneralModel>(sql, pr);
    }
    public async Task<TipoGeneralModel?> SelByIdAsync(TipoGeneralFilterDto c)
    {
        DynamicParameters pr = new();
        pr.Add2("CoGrupo", c.CoGrupo, DbType.Int32);
        pr.Add2("CoTipo", c.CoTipo, DbType.Int32);
        string sql = $@"SELECT * FROM {_schema}.fn_tipogeneral_sel_by_id(
            @CoGrupo,
            @CoTipo
         )";
        return await _connection.QueryFirstOrDefaultAsync<TipoGeneralModel>(sql, pr);
    }

}


