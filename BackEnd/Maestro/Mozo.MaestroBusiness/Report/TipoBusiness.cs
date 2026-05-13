using Mozo.MaestroData;
using Mozo.Model.Maestro;

namespace Mozo.MaestroBusiness.Report;

public interface ITipoBusiness
{
    Task<IEnumerable<TipoParticularModel>> SelAllAsync(TipoParticularModel c);
}

public class TipoBusiness : ITipoBusiness
{
    private readonly ITipoData _data;
    public TipoBusiness(ITipoData data)
    {
        _data = data;
    }
    public async Task<IEnumerable<TipoParticularModel>> SelAllAsync(TipoParticularModel c) => await _data.SelAllAsync(c);
}