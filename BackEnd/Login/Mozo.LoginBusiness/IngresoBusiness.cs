using Mozo.LoginData;
using Mozo.Model.Seguridad;

namespace Mozo.LoginBusiness;

public interface IIngresoBusiness
{
    Task<int> InsertAsync(IngresoModel c);
    Task UpdateCloseTokenAsync(IngresoModel c);
    Task<IngresoModel?> SelByIdAsync(IngresoFilterDto c);

}
public class IngresoBusiness : IIngresoBusiness
{
    private readonly IIngresoData _data;
    public IngresoBusiness(IIngresoData data)
    {
        _data = data;
    }

    public async Task<int> InsertAsync(IngresoModel c) => await _data.InsertAsync(c);
    public async Task UpdateCloseTokenAsync(IngresoModel c) => await _data.UpdateCloseTokenAsync(c);
    public async Task<IngresoModel?> SelByIdAsync(IngresoFilterDto c) => await _data.SelByIdAsync(c);

}