using Dapper;

using Mozo.Helper.Enu;
using Mozo.Helper.Global;
using Mozo.Model.Seguridad;

using System.Data;

///<summary>
///
///</summary>
///<remarks>
///</remarks>
///<history>
/// t[Jonatan Abregu]	16/11/2018	Created
///</history>
namespace Mozo.SeguridadData;

public interface IPaginaData
{
    Task<int> InsertAsync(PaginaModel c);
    Task UpdateAsync(PaginaModel c);
    Task UpdateStateAsync(PaginaModel c);
    Task DeleteByIdAsync(PaginaFilterDto c);
    Task<PaginaModel?> SelByIdAsync(PaginaFilterDto c);
    Task<IEnumerable<PaginaModel>> SelAllPaginaAsync(PaginaFilterDto c);    
    Task<IEnumerable<PaginaModel>> SelAllActivePaginaAsync(PaginaFilterDto c);
    
}
public partial class PaginaData : IPaginaData
{
    private readonly string _schema = EnuCommon.BdDefault.Schema.Seguridad;
    private readonly IDbConnection _connection;
    private readonly UserContext _user;
    public PaginaData(IDbConnection connection, UserContext user)
    {
        _connection = connection;
        _user = user;
    }

    public async Task<int> InsertAsync(PaginaModel c)
    {
        DynamicParameters pr = new();
        pr.Add2("CoModulo", c.CoModulo, DbType.Int32);
        pr.Add2("CoArea", c.CoArea, DbType.Int32);
        pr.Add2("CoMenu", c.CoMenu, DbType.Int32);        
        pr.Add2("NoOpcion", c.NoOpcion, DbType.String);
        pr.Add2("NuOrden", c.NuOrden, DbType.Int32);
        pr.Add2("CoTipoPagina", c.CoTipoPagina, DbType.Int32);        
        pr.Add2("NoControlador", c.NoControlador, DbType.String);
        pr.Add2("NoAccion", c.NoAccion, DbType.String);
        pr.Add2("CoUsuCre", _user.CoPersona, DbType.Int32);

        string sql = $@"SELECT {_schema}.fn_pagina_insert(
               @CoModulo,
               @CoArea,
               @CoMenu,               
               @NoOpcion,
               @NuOrden,
               @CoTipoPagina,               
               @NoControlador,
               @NoAccion,
               @CoUsuCre
            )";
        return await _connection.ExecuteScalarAsync<int>(sql, pr);
    }
    public async Task UpdateAsync(PaginaModel c)
    {
        DynamicParameters pr = new();
        pr.Add2("CoPagina", c.CoPagina, DbType.Int32);
        pr.Add2("CoArea", c.CoArea, DbType.Int32);
        pr.Add2("NoOpcion", c.NoOpcion, DbType.String);
        pr.Add2("NuOrden", c.NuOrden, DbType.Int32);        
        pr.Add2("NoControlador", c.NoControlador, DbType.String);
        pr.Add2("NoAccion", c.NoAccion, DbType.String);
        pr.Add2("CoUsuMod", _user.CoPersona, DbType.Int32);

        string sql = $@"CALL {_schema}.usp_pagina_update(
               @CoPagina,
               @CoArea,            
               @NoOpcion,
               @NuOrden,
               @NoControlador,
               @NoAccion,
               @CoUsuMod
        )";
        await _connection.ExecuteAsync(sql, pr);
    }
    public async Task UpdateStateAsync(PaginaModel c)
    {
        DynamicParameters pr = new();
        pr.Add2("CoPagina", c.CoPagina, DbType.Int32);
        pr.Add2("FlEstReg", c.FlEstReg, DbType.Int32);
        pr.Add2("CoUsuMod", _user.CoPersona, DbType.Int32);

        string sql = $"CALL {_schema}.usp_pagina_update_state(@CoPagina,@FlEstReg,@CoUsuMod)";
        await _connection.ExecuteAsync(sql, pr);
    }
    public async Task DeleteByIdAsync(PaginaFilterDto c)
    {
        DynamicParameters pr = new();
        pr.Add2("CoPagina", c.CoPagina, DbType.Int32);
        pr.Add2("CoUsuEli", _user.CoPersona, DbType.Int32);
        string sql = $"CALL {_schema}.usp_pagina_delete_by_id(@CoPagina,@CoUsuEli)";
        await _connection.ExecuteAsync(sql, pr);
    }
    public async Task<PaginaModel?> SelByIdAsync(PaginaFilterDto c)
    {
        DynamicParameters pr = new();
        pr.Add2("CoModulo", c.CoModulo, DbType.Int32);
        pr.Add2("CoPagina", c.CoPagina, DbType.Int32);
        string sql = $"SELECT * FROM {_schema}.fn_pagina_sel_by_id(@CoModulo,@CoPagina)";
        return await _connection.QueryFirstOrDefaultAsync<PaginaModel>(sql, pr);
    }
    public async Task<IEnumerable<PaginaModel>> SelAllPaginaAsync(PaginaFilterDto c)
    {
        c.CoTipoPagina = EnuTipoGeneral.Seguridad.Pagina.Paginaa;
        DynamicParameters pr = new();
        pr.Add2("CoModulo", c.CoModulo, DbType.Int32);
        pr.Add2("CoTipoPagina", c.CoTipoPagina, DbType.Int32);

        string sql = $"SELECT * FROM {_schema}.fn_pagina_sel_all(@CoModulo,@CoTipoPagina)";
        return await _connection.QueryAsync<PaginaModel>(sql, pr);
    }
    
    
    public async Task<IEnumerable<PaginaModel>> SelAllActivePaginaAsync(PaginaFilterDto c)
    {
        c.CoTipoPagina = EnuTipoGeneral.Seguridad.Pagina.Paginaa;
        DynamicParameters pr = new();
        pr.Add2("CoModulo", c.CoModulo, DbType.Int32);
        pr.Add2("CoTipoPagina", c.CoTipoPagina, DbType.Int32);
        string sql = $"SELECT * FROM {_schema}.fn_pagina_sel_all_active(@CoModulo,@CoTipoPagina)";
        return await _connection.QueryAsync<PaginaModel>(sql, pr);
    }    

}