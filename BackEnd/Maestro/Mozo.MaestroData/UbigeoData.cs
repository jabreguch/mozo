using Dapper;

using Mozo.Helper.Enu;
using Mozo.Model;

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
public interface IUbigeoData
{
    Task<UbigeoModel?> SelById(UbigeoModel c);
    Task<IEnumerable<UbigeoModel>> SelAllDepartamento(UbigeoModel c);
    Task<IEnumerable<UbigeoModel>> SelAllProvincia(UbigeoModel c);
    Task<IEnumerable<UbigeoModel>> SelAllDistrito(UbigeoModel c);
}

public partial class UbigeoData : IUbigeoData
{
    private readonly string _schema = EnuCommon.BdDefault.Schema.Maestro;
    private readonly IDbConnection _connection;
    public UbigeoData(IDbConnection connection)
    {
        _connection = connection;
    }
    public async Task<UbigeoModel?> SelById(UbigeoModel c)
    {

        string sql = $"SELECT * FROM {_schema}.fn_ubigeo_sel_by_id(@CoUbigeo)";

        return await _connection.QueryFirstOrDefaultAsync<UbigeoModel>(sql, c);
    }
    public async Task<IEnumerable<UbigeoModel>> SelAllDepartamento(UbigeoModel c)
    {

        string sql = $"SELECT * FROM {_schema}.fn_ubigeo_sel_all_departamento())";

        return await _connection.QueryAsync<UbigeoModel>(sql, c);
    }
    public async Task<IEnumerable<UbigeoModel>> SelAllProvincia(UbigeoModel c)
    {

        string sql = $"SELECT * FROM {_schema}.fn_ubigeo_sel_all_provincia(@CoDepartamento)";

        return await _connection.QueryAsync<UbigeoModel>(sql, c);
    }
    public async Task<IEnumerable<UbigeoModel>> SelAllDistrito(UbigeoModel c)
    {

        string sql = $"SELECT * FROM {_schema}.fn_ubigeo_sel_all_distrito(@CoDepartamento, @CoProvincia)";

        return await _connection.QueryAsync<UbigeoModel>(sql, c);
    }
}
