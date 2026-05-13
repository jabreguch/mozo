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

public interface IDocumentoIdentidadData
{
    Task<IEnumerable<DocumentoIdentidadModel>> SelAllActiveAsync();
}
public partial class DocumentoIdentidadData : IDocumentoIdentidadData
{
    private readonly string _schema = EnuCommon.BdDefault.Schema.Maestro;

    private readonly IDbConnection _connection;
    private readonly UserContext _user;
    public DocumentoIdentidadData(IDbConnection connection, UserContext user)
    {
        _connection = connection;
        _user = user;
    }

    public async Task<IEnumerable<DocumentoIdentidadModel>> SelAllActiveAsync()
    {
        string sql = $"SELECT * FROM {_schema}.fn_documentoidentidad_sel_all_active()";
        return await _connection.QueryAsync<DocumentoIdentidadModel>(sql);
    }


}
