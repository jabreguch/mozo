using Mozo.Helper.Enu;
using Mozo.MaestroData;
using Mozo.Model.Maestro;

using System.Data;

namespace Mozo.MaestroBusiness;

public interface IPersonaBusiness
{
    Task<int?> InsertAsync(PersonaModel c);
    Task UpdateAsync(PersonaModel c);
    Task UpdateStateAsync(PersonaModel c);
    Task DeleteByIdAsync(PersonaModel c);
    Task<PersonaModel?> SelByIdAsync(PersonaModel c);
    Task<PersonaModel?> SelRepeatAsync(PersonaModel c);
    Task<IEnumerable<PersonaModel>> SelAllAsync(PersonaModel c);
    Task<IEnumerable<PersonaModel>> SelAllActiveAsync(PersonaModel c);
}

public class PersonaBusiness : IPersonaBusiness
{
    private readonly IDbConnection _connection;
    private readonly IPersonaData _data;
    private readonly IPersonaTipoData _personaTipoData;
    private readonly IRedSocialData _redSocialData;

    public PersonaBusiness(IDbConnection connection,
        IPersonaData data,
        IPersonaTipoData personaTipoData,
        IRedSocialData redSocialData)
    {
        _connection = connection;
        _data = data;
        _personaTipoData = personaTipoData;
        _redSocialData = redSocialData;

    }

    public async Task<int?> InsertAsync(PersonaModel c)
    {
        if (_connection.State != ConnectionState.Open) _connection.Open();
        using IDbTransaction tran = _connection.BeginTransaction();
        try
        {
            // Insert Persona
            c.CoPersona = await _data.InsertAsync(c, tran);

            // Insert PersonaTipo
            PersonaTipoModel personaTipo = new()
            {
                CoModulo = c.CoModulo,
                CoPersona = c.CoPersona,
                CoTipo = c.CoPersonaTipo,
                FlEstReg = 1,
            };
            personaTipo.CoPersonaTipo = await _personaTipoData.InsertAsync(personaTipo, tran);

            // Teléfono fijo
            if (!string.IsNullOrEmpty(c.NoTelefonoFijo))
            {
                await _redSocialData.InsertAsync(new RedSocialModel
                {
                    FlPersona = 1,
                    CoPersona = c.CoPersona,
                    CoTipoRedSocial = EnuTipoGeneral.Maestro.RedSocial.TelefonoFijo,
                    CoEtiqueta = EnuTipoGeneral.Maestro.EtiquetaRedSocial.Personal,
                    NoRedSocial = c.NoTelefonoFijo,
                }, tran);
            }

            // Teléfono móvil
            if (!string.IsNullOrEmpty(c.NoTelefonoMovil))
            {
                await _redSocialData.InsertAsync(new RedSocialModel
                {
                    FlPersona = 1,
                    CoPersona = c.CoPersona,
                    CoTipoRedSocial = EnuTipoGeneral.Maestro.RedSocial.TelefonoMovil,
                    CoEtiqueta = EnuTipoGeneral.Maestro.EtiquetaRedSocial.Personal,
                    NoRedSocial = c.NoTelefonoMovil
                }, tran);
            }

            // Correo electrónico
            if (!string.IsNullOrEmpty(c.NoCorreoElectronico))
            {
                await _redSocialData.InsertAsync(new RedSocialModel
                {
                    FlPersona = 1,
                    CoPersona = c.CoPersona,
                    CoTipoRedSocial = EnuTipoGeneral.Maestro.RedSocial.CorreoElectronico,
                    CoEtiqueta = EnuTipoGeneral.Maestro.EtiquetaRedSocial.Personal,
                    NoRedSocial = c.NoCorreoElectronico
                }, tran);
            }
            tran.Commit();
            return c.CoPersona;
        }
        catch
        {
            tran.Rollback();
            throw; // loggear error aquí
        }
    }

    public async Task UpdateAsync(PersonaModel c) => await _data.UpdateAsync(c);
    public async Task UpdateStateAsync(PersonaModel c) => await _data.UpdateStateAsync(c);
    public async Task DeleteByIdAsync(PersonaModel c) => await _data.DeleteByIdAsync(c);
    public async Task<IEnumerable<PersonaModel>> SelAllAsync(PersonaModel c) => await _data.SelAllAsync(c);
    public async Task<PersonaModel?> SelByIdAsync(PersonaModel c) => await _data.SelByIdAsync(c);
    public async Task<PersonaModel?> SelRepeatAsync(PersonaModel c) => await _data.SelRepeatAsync(c);
    public async Task<IEnumerable<PersonaModel>> SelAllActiveAsync(PersonaModel c) => await _data.SelAllActiveAsync(c);

}
