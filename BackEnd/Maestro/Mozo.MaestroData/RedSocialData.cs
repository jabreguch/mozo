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

public interface IRedSocialData
{
    Task<int> InsertAsync(RedSocialModel c, IDbTransaction? tran = null);
    Task UpdateAsync(RedSocialModel c);
    Task DeleteByIdAsync(RedSocialFilterDto c);
    Task<RedSocialModel?> SelByIdAsync(RedSocialFilterDto c);
    Task<IEnumerable<RedSocialModel>> SelAllAsync(RedSocialFilterDto c);
}
public partial class RedSocialData : IRedSocialData
{
    private readonly string _schema = EnuCommon.BdDefault.Schema.Maestro;

    private readonly IDbConnection _connection;
    private readonly UserContext _user;
    public RedSocialData(IDbConnection connection, UserContext user)
    {
        _connection = connection;
        _user = user;
    }


    public async Task<int> InsertAsync(RedSocialModel c, IDbTransaction? tran = null)
    {

        DynamicParameters pr = new();
        pr.Add2("CoEmpresa", c.FlEmpresaNotKey == 1 ? c.CoEmpresa : _user.CoEmpresa, DbType.Int32);
        pr.Add2("FlPersona", c.FlPersona, DbType.Int32);
        pr.Add2("CoPersona", c.CoPersona, DbType.Int32);
        pr.Add2("CoTipoRedSocial", c.CoTipoRedSocial, DbType.Int32);
        pr.Add2("CoTipoUrl", c.CoTipoUrl, DbType.Int32);
        pr.Add2("CoEtiqueta", c.CoEtiqueta, DbType.Int32);
        pr.Add2("NoRedSocial", c.NoRedSocial, DbType.String);
        pr.Add2("FlWhatsapp", c.FlWhatsapp, DbType.Int32);
        pr.Add2("CoUsuCre", _user.CoPersona, DbType.Int32);

        string sql = $@"SELECT {_schema}.fn_redsocial_insert(
            @CoEmpresa,
            @FlPersona,
            @CoPersona,
            @CoTipoRedSocial,
            @CoTipoUrl,
            @CoEtiqueta,
            @NoRedSocial,
            @FlWhatsapp,
            @CoUsuCre)";

        if (tran == null)
            return await _connection.ExecuteScalarAsync<int>(sql, pr);
        else
            return await _connection.ExecuteScalarAsync<int>(sql, pr, tran);
    }
    public async Task UpdateAsync(RedSocialModel c)
    {
        DynamicParameters pr = new();
        pr.Add2("CoEmpresa", c.FlEmpresaNotKey == 1 ? c.CoEmpresa : _user.CoEmpresa, DbType.Int32);
        pr.Add2("CoRedSocial", c.CoRedSocial, DbType.Int32);
        pr.Add2("CoTipoUrl", c.CoTipoUrl, DbType.Int32);
        pr.Add2("CoEtiqueta", c.CoEtiqueta, DbType.Int32);
        pr.Add2("NoRedSocial", c.NoRedSocial, DbType.String);
        pr.Add2("FlWhatsapp", c.FlWhatsapp, DbType.Int32);
        pr.Add2("CoUsuMod", _user.CoPersona, DbType.Int32);

        string sql = $@"CALL {_schema}.usp_redsocial_update(
            @CoEmpresa,
            @CoRedSocial,
            @CoTipoUrl,
            @CoEtiqueta,
            @NoRedSocial,
            @FlWhatsapp,
            @CoUsuMod
        )";
        await _connection.ExecuteScalarAsync(sql, pr);
    }
    public async Task DeleteByIdAsync(RedSocialFilterDto c)
    {
        DynamicParameters pr = new();
        pr.Add2("CoEmpresa", c.FlEmpresaNotKey == 1 ? c.CoEmpresa : _user.CoEmpresa, DbType.Int32);
        pr.Add2("CoRedSocial", c.CoRedSocial, DbType.Int32);
        pr.Add2("CoUsuEli", _user.CoPersona, DbType.Int32);

        string sql = $@"CALL {_schema}.usp_redsocial_delete_by_id(
            @CoEmpresa,
            @CoRedSocial,
            @CoUsuEli
          )";
        await _connection.ExecuteScalarAsync(sql, pr);
    }

    public async Task<IEnumerable<RedSocialModel>> SelAllAsync(RedSocialFilterDto c)
    {
        DynamicParameters pr = new();
        pr.Add2("CoEmpresa", c.FlEmpresaNotKey == 1 ? c.CoEmpresa : _user.CoEmpresa, DbType.Int32);
        pr.Add2("FlPersona", c.FlPersona, DbType.Int32);
        pr.Add2("CoTipoRedSocial", c.CoTipoRedSocial, DbType.Int32);
        pr.Add2("CoPersona", c.CoPersona, DbType.Int32);
        pr.Add2("CoRedSocial", c.CoRedSocial, DbType.Int32);

        string sql = $@"SELECT * FROM {_schema}.fn_redsocial_sel_all(
            @CoEmpresa,
            @FlPersona,
            @CoTipoRedSocial,
            @CoPersona,
            @CoRedSocial
          )";
        return await _connection.QueryAsync<RedSocialModel>(sql, pr);
    }
    public async Task<RedSocialModel?> SelByIdAsync(RedSocialFilterDto c)
    {
        DynamicParameters pr = new();
        pr.Add2("CoEmpresa", c.FlEmpresaNotKey == 1 ? c.CoEmpresa : _user.CoEmpresa, DbType.Int32);
        pr.Add2("CoRedSocial", c.CoRedSocial, DbType.Int32);

        string sql = $@"SELECT * FROM {_schema}.fn_redsocial_sel_by_id(
            @CoEmpresa,
            @CoRedSocial
         )";
        return await _connection.QueryFirstOrDefaultAsync<RedSocialModel>(sql, pr);
    }

}
