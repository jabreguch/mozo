using Mozo.MaestroData;
using Mozo.Model.Maestro;
///<summary>
///
///</summary>
///<remarks>
///</remarks>
///<history>
/// t[Jonatan Abregu]	18-04-2020	Created
///</history>
namespace Mozo.MaestroBusiness;

public interface IPersonaTipoBusiness
{
    Task<int> InsertAsync(PersonaTipoModel c);
    Task UpdateAsync(PersonaTipoModel c);
    Task<IEnumerable<PersonaTipoModel>> SelAllActiveByPersonaAsync(PersonaTipoModel c);
    Task<IEnumerable<PersonaTipoModel>> SelAllActiveByModuloAndPersonaAsync(PersonaTipoModel c);
    Task<IEnumerable<PersonaTipoModel>> SelAllActiveAsync(PersonaTipoModel c);
}

public class PersonaTipoBusiness : IPersonaTipoBusiness
{
    private readonly IPersonaTipoData _data;
    public PersonaTipoBusiness(IPersonaTipoData data)
    {
        _data = data;
    }
    public async Task<int> InsertAsync(PersonaTipoModel c) => await _data.InsertAsync(c);
    public async Task UpdateAsync(PersonaTipoModel c) => await _data.UpdateAsync(c);
    public async Task<IEnumerable<PersonaTipoModel>> SelAllActiveByPersonaAsync(PersonaTipoModel c) => await _data.SelAllActiveByPersonaAsync(c);
    public async Task<IEnumerable<PersonaTipoModel>> SelAllActiveByModuloAndPersonaAsync(PersonaTipoModel c) => await _data.SelAllActiveByModuloAndPersonaAsync(c);
    public async Task<IEnumerable<PersonaTipoModel>> SelAllActiveAsync(PersonaTipoModel c) => await _data.SelAllActiveAsync(c);


}
