using Dapper;

using Mozo.Helper.Enu;
using Mozo.Helper.Global;
using Mozo.Model.Maestro;

using Npgsql;

using NpgsqlTypes;

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

public interface IArchivoData
{
    Task<int> InsertMetaDataAsync(ArchivoModel c);
    Task UpdateMetaDataAsync(ArchivoModel c);
    Task UpdateAsync(ArchivoModel c);
    Task DeleteByIdAsync(ArchivoFilterDto c);

    Task UpdateOrdenMasivoAsync(OrdenMasivoRequest req);
    Task UpdateOrdenAsync(ArchivoModel c);

    Task<ArchivoModel?> SelByIdAsync(ArchivoFilterDto c);
    Task<ArchivoModel?> SelMetaDataByUkAsync(ArchivoFilterDto c);
    Task<IEnumerable<ArchivoModel>> SelAllAsync(ArchivoFilterDto c);
    Task<ArchivoModel?> SelMetaDataByIdAsync(ArchivoFilterDto c);

    Task<IEnumerable<ArchivoModel>> SelAllActiveAsync(ArchivoFilterDto c);


}

public partial class ArchivoData : IArchivoData
{
    private readonly string _schema = EnuCommon.BdDefault.Schema.Maestro;

    private readonly IDbConnection _connection;
    private readonly UserContext _user;

    public ArchivoData(
        IDbConnection connection,
        UserContext user)
    {
        _connection = connection;
        _user = user;
    }

    public async Task<int> InsertMetaDataAsync(ArchivoModel c)
    {
        DynamicParameters pr = new();
        pr.Add2("CoEmpresa", c.FlEmpresaNotKey == 1 ? c.CoEmpresa : _user.CoEmpresa, DbType.Int32);
        pr.Add2("CoTipoEntidad", c.CoTipoEntidad, DbType.Int32);
        pr.Add2("CoEntidad", c.CoEntidad, DbType.Int32);
        pr.Add2("CoTipo", c.CoTipo, DbType.Int32);
        pr.Add2("FlGaleria", c.FlGaleria, DbType.Int32);
        pr.Add2("NoArchivo", c.NoArchivo, DbType.String);
        pr.Add2("NoExtension", c.NoExtension, DbType.String);
        pr.Add2("NoRuta", c.NoRuta, DbType.String);
        pr.Add2("NuBytes", c.NuBytes, DbType.Int32);
        pr.Add2("NuAlto", c.NuAlto, DbType.Int32);
        pr.Add2("NuAncho", c.NuAncho, DbType.Int32);
        pr.Add2("CoUsuCre", _user.CoPersona, DbType.Int32);

        string sql = $@"SELECT {_schema}.fn_archivo_insert_meta_data(
            @CoEmpresa,
            @CoTipoEntidad,
            @CoEntidad,
            @CoTipo,
            @FlGaleria,
            @NoArchivo,
            @NoExtension, 
            @NoRuta,
            @NuBytes,
            @NuAlto,
            @NuAncho,            
            @CoUsuCre
            )";
        return await _connection.ExecuteScalarAsync<int>(sql, pr);
    }

    public async Task UpdateMetaDataAsync(ArchivoModel c)
    {

        DynamicParameters pr = new();
        pr.Add2("CoEmpresa", c.FlEmpresaNotKey == 1 ? c.CoEmpresa : _user.CoEmpresa, DbType.Int32);
        pr.Add2("CoArchivo", c.CoArchivo, DbType.Int32);
        pr.Add2("NoArchivo", c.NoArchivo, DbType.String);
        pr.Add2("NoExtension", c.NoExtension, DbType.String);
        pr.Add2("NoRuta", c.NoRuta, DbType.String);
        pr.Add2("NuBytes", c.NuBytes, DbType.Decimal);
        pr.Add2("NuAlto", c.NuAlto, DbType.Int32);
        pr.Add2("NuAncho", c.NuAncho, DbType.Int32);
        pr.Add2("CoUsuMod", _user.CoPersona, DbType.Int32);

        string sql = $@"CALL {_schema}.usp_archivo_update_meta_data(
            @CoEmpresa,            
            @CoArchivo,            
            @NoArchivo,
            @NoExtension,
            @NoRuta,
            @NuBytes,
            @NuAlto,
            @NuAncho,            
            @CoUsuMod
            )";
        await _connection.ExecuteScalarAsync(sql, pr);
    }



    public async Task UpdateAsync(ArchivoModel c)
    {

        DynamicParameters pr = new();
        pr.Add2("CoEmpresa", c.FlEmpresaNotKey == 1 ? c.CoEmpresa : _user.CoEmpresa, DbType.Int32);
        pr.Add2("CoArchivo", c.CoArchivo, DbType.Int32);
        pr.Add2("NuOrden", c.NuOrden, DbType.Int32);
        pr.Add2("TxDescripcion", c.TxDescripcion, DbType.String);
        pr.Add2("FlDefault", c.FlDefault, DbType.Int32);
        pr.Add2("NoTitulo", c.NoTitulo, DbType.String);
        pr.Add2("CoUsuMod", _user.CoPersona, DbType.Int32);

        string sql = $@"CALL {_schema}.usp_archivo_update(
            @CoEmpresa,            
            @CoArchivo,            
            @NuOrden,
            @TxDescripcion,            
            @FlDefault,            
            @NoTitulo,
            @CoUsuMod
            )";
        await _connection.ExecuteScalarAsync(sql, pr);
    }


    public async Task UpdateOrdenAsync(ArchivoModel c)
    {
        DynamicParameters pr = new();
        pr.Add2("CoEmpresa", c.FlEmpresaNotKey == 1 ? c.CoEmpresa : _user.CoEmpresa, DbType.Int32);
        pr.Add2("CoArchivo", c.CoArchivo, DbType.Int32);
        pr.Add2("NuOrden", c.NuOrden, DbType.Int32);
        pr.Add2("CoUsuMod", _user.CoPersona, DbType.Int32);

        string sql = $@"CALL {_schema}.usp_archivo_update_orden(
            @CoEmpresa,
            @CoArchivo,
            @NuOrden,
            @CoUsuMod
        )";
        await _connection.ExecuteScalarAsync(sql, pr);
    }

    public async Task UpdateOrdenMasivoAsync(OrdenMasivoRequest req)
    {
        int[]? coArchivos = req.Ordenes.Select(o => o.CoArchivo).ToArray();
        int[]? nuOrdenes = req.Ordenes.Select(o => o.NuOrden).ToArray();

        DynamicParameters pr = new();
        pr.Add2("CoEmpresa", req.FlEmpresaNotKey == 1 ? req.CoEmpresa : _user.CoEmpresa, DbType.Int32);
        // Forma explícita con NpgsqlDbType
        pr.Add("CoArchivos", new NpgsqlParameter
        {
            Value = coArchivos,
            NpgsqlDbType = NpgsqlDbType.Array | NpgsqlDbType.Integer
        });
        pr.Add("NuOrdenes", new NpgsqlParameter
        {
            Value = nuOrdenes,
            NpgsqlDbType = NpgsqlDbType.Array | NpgsqlDbType.Integer
        });
        pr.Add2("CoUsuMod", _user.CoPersona, DbType.Int32);

        string sql = $@"CALL {_schema}.usp_archivo_update_orden_masivo(
            @CoEmpresa,
            @CoArchivos,
            @NuOrdenes,
            @CoUsuMod
        )";
        await _connection.ExecuteScalarAsync(sql, pr);
    }


    public async Task DeleteByIdAsync(ArchivoFilterDto c)
    {
        DynamicParameters pr = new();
        pr.Add2("CoEmpresa", c.FlEmpresaNotKey == 1 ? c.CoEmpresa : _user.CoEmpresa, DbType.Int32);
        pr.Add2("CoArchivo", c.CoArchivo, DbType.Int32);
        pr.Add2("CoUsuEli", _user.CoPersona, DbType.Int32);

        string sql = $@"CALL {_schema}.usp_archivo_delete_by_id(
            @CoEmpresa,
            @CoArchivo,
            @CoUsuEli
        )";
        await _connection.ExecuteScalarAsync(sql, pr);
    }
    public async Task<IEnumerable<ArchivoModel>> SelAllAsync(ArchivoFilterDto c)
    {
        DynamicParameters pr = new();
        pr.Add2("CoEmpresa", c.FlEmpresaNotKey == 1 ? c.CoEmpresa : _user.CoEmpresa, DbType.Int32);
        pr.Add2("CoTipoEntidad", c.CoTipoEntidad, DbType.Int32);
        pr.Add2("CoEntidad", c.CoEntidad, DbType.Int32);

        string sql = $@"SELECT * FROM {_schema}.fn_archivo_sel_all(
            @CoEmpresa,
            @CoTipoEntidad,
            @CoEntidad
        )";
        return await _connection.QueryAsync<ArchivoModel>(sql, pr);
    }
    public async Task<IEnumerable<ArchivoModel>> SelAllActiveAsync(ArchivoFilterDto c)
    {
        DynamicParameters pr = new();
        pr.Add2("CoEmpresa", c.FlEmpresaNotKey == 1 ? c.CoEmpresa : _user.CoEmpresa, DbType.Int32);
        pr.Add2("CoTipoEntidad", c.CoTipoEntidad, DbType.Int32);
        pr.Add2("CoEntidad", c.CoEntidad, DbType.Int32);

        string sql = $@"SELECT * FROM {_schema}.fn_archivo_sel_all_active(
            @CoEmpresa,
            @CoTipoEntidad,
            @CoEntidad
        )";
        return await _connection.QueryAsync<ArchivoModel>(sql, pr);
    }
    public async Task<ArchivoModel?> SelByIdAsync(ArchivoFilterDto c)
    {
        DynamicParameters pr = new();
        pr.Add2("CoEmpresa", c.FlEmpresaNotKey == 1 ? c.CoEmpresa : _user.CoEmpresa, DbType.Int32);
        pr.Add2("CoArchivo", c.CoArchivo, DbType.Int32);

        string sql = $@"SELECT * FROM {_schema}.fn_archivo_sel_by_id(
            @CoEmpresa,
            @CoArchivo
        )";
        return await _connection.QueryFirstOrDefaultAsync<ArchivoModel>(sql, pr);
    }

    public async Task<ArchivoModel?> SelMetaDataByUkAsync(ArchivoFilterDto c)
    {
        DynamicParameters pr = new();
        pr.Add2("CoEmpresa", c.FlEmpresaNotKey == 1 ? c.CoEmpresa : _user.CoEmpresa, DbType.Int32);
        pr.Add2("CoTipoEntidad", c.CoTipoEntidad, DbType.Int32);
        pr.Add2("CoEntidad", c.CoEntidad, DbType.Int32);
        pr.Add2("CoTipo", c.CoTipo, DbType.Int32);
        pr.Add2("NuOrden", c.NuOrden, DbType.Int32);

        string sql = $@"SELECT * FROM {_schema}.fn_archivo_sel_metadata_by_uk(
            @CoEmpresa,
            @CoTipoEntidad,
            @CoEntidad,
            @CoTipo,
            @NuOrden
        )";
        return await _connection.QueryFirstOrDefaultAsync<ArchivoModel>(sql, pr);
    }

    public async Task<ArchivoModel?> SelMetaDataByIdAsync(ArchivoFilterDto c)
    {
        DynamicParameters pr = new();
        pr.Add2("CoEmpresa", c.FlEmpresaNotKey == 1 ? c.CoEmpresa : _user.CoEmpresa, DbType.Int32);
        pr.Add2("CoArchivo", c.CoArchivo, DbType.Int32);

        string sql = $@"SELECT * FROM {_schema}.fn_archivo_sel_metadata_by_id(
            @CoEmpresa,
            @CoArchivo
        )";
        return await _connection.QueryFirstOrDefaultAsync<ArchivoModel>(sql, pr);
    }





}
