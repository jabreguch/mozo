using Dapper;

using Mozo.Helper.Enu;
using Mozo.Helper.Global;
using Mozo.Model.Maestro;

using System.Data;
namespace Mozo.MaestroData.Report;

public interface ITipoData
{
    Task<IEnumerable<TipoParticularModel>> SelAllAsync(TipoParticularModel c);
}
public partial class TipoData : ITipoData
{
    private readonly string _schema = EnuCommon.BdDefault.Schema.MaestroRpt;
    private readonly IDbConnection _connection;
    private readonly UserContext _user;
    public TipoData(IDbConnection connection, UserContext user)
    {
        _connection = connection;
        _user = user;
    }
    public async Task<IEnumerable<TipoParticularModel>> SelAllAsync(TipoParticularModel c)
    {
        (DynamicParameters pr, string args) = BuildDinamicParameter.Build(c, _user.CoEmpresa, _user.CoPersona,
            "CoEmpresa",
            "CoGrupo",
            "NoInputSearch",
            "FlEstReg"
        );
        string sql = $"SELECT * FROM {_schema}.fn_tipo_sel_all({args})";
        return await _connection.QueryAsync<TipoParticularModel>(sql, pr);
    }

}