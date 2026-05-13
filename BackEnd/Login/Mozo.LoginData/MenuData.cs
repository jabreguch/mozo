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
namespace Mozo.LoginData;

public interface IMenuData
{
    Task<IEnumerable<MenuModel>> SelAllAsync(MenuFilterDto c);
}
public partial class MenuData : IMenuData
{
    private readonly string _schema = EnuCommon.BdDefault.Schema.Login;

    private readonly IDbConnection _connection;
    private readonly UserContext _user;
    public MenuData(IDbConnection connection, UserContext user)
    {
        _connection = connection;
        _user = user;
    }


    public async Task<IEnumerable<MenuModel>> SelAllAsync(MenuFilterDto c)
    {
        DynamicParameters pr = new();
        pr.Add2("CoEmpresa", _user.CoEmpresa, DbType.Int32);
        pr.Add2("CoModulo", c.CoModulo, DbType.Int32);
        pr.Add2("CoPersona", c.CoPersona, DbType.Int32);
        string sql = $"SELECT * FROM {_schema}.fn_menu_sel_all(@CoEmpresa,@CoModulo,@CoPersona)";
        return await _connection.QueryAsync<MenuModel>(sql, pr);
    }


}