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

public interface IPaisData
{
    Task<IEnumerable<PaisModel>> SelAllActiveAsync();
}
public partial class PaisData : IPaisData
{
    private readonly string _schema = EnuCommon.BdDefault.Schema.Maestro;

    private readonly IDbConnection _connection;
    private readonly UserContext _user;
    public PaisData(IDbConnection connection, UserContext user)
    {
        _connection = connection;
        _user = user;
    }
    public async Task<IEnumerable<PaisModel>> SelAllActiveAsync()
    {
        string sql = $"SELECT * FROM {_schema}.fn_pais_sel_all_active()";
        return await _connection.QueryAsync<PaisModel, MonedaModel, PaisModel>(sql,
            (pais, moneda) =>
            {
                pais.Moneda = moneda;
                return pais;
            },
            splitOn: "moneda_split"
        );
    }



}
