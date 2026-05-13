///<summary>
///
///</summary>
///<remarks>
///</remarks>
///<history>
/// t[Jonatan Abregu]	10-04-2020	Created
///</history>
namespace Mozo.Model.Condominio;

public record DeudaDetalleFilter : BaseFilterDto
{

}

[Serializable]
public class DeudaDetalleModel : BaseModel //<DeudaDetalleModel>
{
    public int? CoDeudaDetalle { get; set; }
    public int? CoDeuda { get; set; }
    public int? CoBanco { get; set; }
    public int? CoTipoPago { get; set; }
    public int? FlIngreso { get; set; }
    public int? CoPeriodo { get; set; }
    public int? CoConsumo { get; set; }
    public int? NuOrden { get; set; }
    public decimal? SsMontoConsumoAcordado { get; set; }
    public decimal? SsMontoConsumoSinAcordar { get; set; }
    public decimal? SsMontoConsumoPagoAcordado { get; set; }
    public decimal? SsMontoConsumoPagoSinAcordar { get; set; }
    public decimal? SsMontoConsumoPago { get; set; }
    public decimal? SsMontoConsumo { get; set; }
    public string? FePago { get; set; }
    public string? NoCuentaBancaria { get; set; }
    public string? NoCuentaContable { get; set; }
    public string? NoBanco { get; set; }
    public string? NoTipoPago { get; set; }
    public string? NoTipoServicio { get; set; }

    // public string? NoCategoria { get; set; }
    // public string? NoSubCategoria { get; set; }




    public int? CoPredio { get; set; }
    public int? CoTipoServicio { get; set; }
    public int? FlAcordadoInquilino { get; set; }
    public int? CoCuenta { get; set; }
    public int? CoMovimiento { get; set; }
    public int? CoCondominio { get; set; }
    public string? NoCondominio { get; set; }
    public string? NoDireccion { get; set; }

    // public string? CoUbigeo { get; set; }
    // public string? CoDepartamento { get; set; }
    // public string? CoProvincia { get; set; }
    // public string? CoDistrito { get; set; }
    // public string? NoDepartamento { get; set; }
    // public string? NoProvincia { get; set; }
    // public string? NoDistrito { get; set; }
    public int? CoTipoPredio { get; set; }
    public string? TxDescripcion { get; set; }
    public ConsumoModel? Consumo { get; set; }
}