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

public interface IMenuData
{
    Task<int> InsertAsync(MenuModel c);
    Task UpdateAsync(MenuModel c);
    Task UpdateStateAsync(MenuModel c);
    Task DeleteByIdAsync(MenuFilterDto c);
    Task<IEnumerable<MenuModel>> SelAllAsync(MenuFilterDto c);
    Task<IEnumerable<MenuModel>> SelAllActiveAsync(MenuFilterDto c);
    Task<MenuModel?> SelByIdAsync(MenuFilterDto c);
}

public partial class MenuData : IMenuData

{
    private readonly string _schema = EnuCommon.BdDefault.Schema.Seguridad;

    private readonly IDbConnection _connection;
    private readonly UserContext _user;
    public MenuData(IDbConnection connection, UserContext user)
    {
        _connection = connection;
        _user = user;
    }

    public async Task<int> InsertAsync(MenuModel c)
    {
        DynamicParameters pr = new();
        pr.Add2("CoModulo", c.CoModulo, DbType.Int32);
        pr.Add2("NuOrden", c.NuOrden, DbType.Int32);
        pr.Add2("NoMenu", c.NoMenu, DbType.String);
        pr.Add2("CoUsuCre", _user.CoPersona, DbType.Int32);

        string sql = $"SELECT {_schema}.fn_menu_insert(@CoModulo,@NuOrden,@NoMenu,@CoUsuCre)";
        return await _connection.ExecuteScalarAsync<int>(sql, pr);
    }
    public async Task UpdateAsync(MenuModel c)
    {
        DynamicParameters pr = new();
        pr.Add2("CoMenu", c.CoMenu, DbType.Int32);
        pr.Add2("NuOrden", c.NuOrden, DbType.Int32);
        pr.Add2("NoMenu", c.NoMenu, DbType.String);
        pr.Add2("CoUsuMod", _user.CoPersona, DbType.Int32);

        string sql = $"CALL {_schema}.usp_menu_update(@CoMenu,@NuOrden,@NoMenu,@CoUsuMod)";
        await _connection.ExecuteScalarAsync(sql, pr);
    }
    public async Task UpdateStateAsync(MenuModel c)
    {
        DynamicParameters pr = new();
        pr.Add2("CoMenu", c.CoMenu, DbType.Int32);
        pr.Add2("FlEstReg", c.FlEstReg, DbType.Int32);
        pr.Add2("CoUsuMod", _user.CoPersona, DbType.Int32);
        string sql = $"CALL {_schema}.usp_menu_update_state(@CoMenu,@FlEstReg,@CoUsuMod)";
        await _connection.ExecuteScalarAsync(sql, pr);
    }
    public async Task DeleteByIdAsync(MenuFilterDto c)
    {
        DynamicParameters pr = new();
        pr.Add2("CoMenu", c.CoMenu, DbType.Int32);
        pr.Add2("CoUsuEli", _user.CoPersona, DbType.Int32);
        string sql = $"CALL {_schema}.usp_menu_delete_by_id(@CoMenu,@CoUsuEli)";
        await _connection.ExecuteScalarAsync(sql, pr);
    }
    public async Task<IEnumerable<MenuModel>> SelAllAsync(MenuFilterDto c)
    {
        DynamicParameters pr = new();
        pr.Add2("CoModulo", c.CoModulo, DbType.Int32);
        string sql = $"SELECT * FROM {_schema}.fn_menu_sel_all(@CoModulo)";
        return await _connection.QueryAsync<MenuModel>(sql, pr);
    }
    public async Task<IEnumerable<MenuModel>> SelAllActiveAsync(MenuFilterDto c)
    {
        DynamicParameters pr = new();
        pr.Add2("CoModulo", c.CoModulo, DbType.Int32);
        string sql = $"SELECT * FROM {_schema}.fn_menu_sel_all_active(@CoModulo)";
        return await _connection.QueryAsync<MenuModel>(sql, pr);
    }
    public async Task<MenuModel?> SelByIdAsync(MenuFilterDto c)
    {
        DynamicParameters pr = new();
        pr.Add2("CoModulo", c.CoModulo, DbType.Int32);
        pr.Add2("CoMenu", c.CoMenu, DbType.Int32);
        string sql = $"SELECT * FROM {_schema}.fn_menu_sel_by_id(@CoModulo,@CoMenu)";
        return await _connection.QueryFirstOrDefaultAsync<MenuModel>(sql, pr);
    }

}