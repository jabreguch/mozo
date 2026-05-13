using Mozo.MaestroData;
using Mozo.Model.Maestro;

namespace Mozo.MaestroBusiness;

public interface ITipoGeneralBusiness
{
    Task<IEnumerable<TipoGeneralModel>> SelAllActiveAsync(TipoGeneralFilterDto c);
    Task<IEnumerable<TipoGeneralModel>> SelAllActiveByModuloAsync(TipoGeneralFilterDto c);
    Task<TipoGeneralModel?> SelByIdAsync(TipoGeneralFilterDto c);
}
public class TipoGeneralBusiness : ITipoGeneralBusiness
{
    private readonly ITipoGeneralData _data;
    public TipoGeneralBusiness(ITipoGeneralData data)
    {
        _data = data;
    }
    public async Task<IEnumerable<TipoGeneralModel>> SelAllActiveAsync(TipoGeneralFilterDto c) => await _data.SelAllActiveAsync(c);
    public async Task<IEnumerable<TipoGeneralModel>> SelAllActiveByModuloAsync(TipoGeneralFilterDto c) => await _data.SelAllActiveByModuloAsync(c);
    public async Task<TipoGeneralModel?> SelByIdAsync(TipoGeneralFilterDto c) => await _data.SelByIdAsync(c);

}
