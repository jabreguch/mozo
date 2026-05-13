using Mozo.Model.Contabilidad;
using Mozo.Model.Maestro;

using static Mozo.Helper.Global.Glo;

///<summary>
///
///</summary>
///<remarks>
///</remarks>
///<history>
/// t[Jonatan Abregu]	21/09/2021	Created
///</history>
namespace Mozo.Model.Catalogo;

public record ProductoFilterDto : BaseFilterDto
{
    public int? CoProveedorPrincipal { get; set; }
    public int? CoMarca { get; set; }
}

public partial class ProductoModel : BaseModel
{
    public int? CoProducto { get; set; }
    public string? NoProducto { get; set; }
    public string? NoModelo { get; set; }
    public int? CoMarca { get; set; }
    public int? CoLaboratorio { get; set; }
    public int? CoUnidadInventario { get; set; }
    public int? CoUnidadCompra { get; set; }
    public int? CoUnidadVenta { get; set; }
    public int? CoProveedorPrincipal { get; set; }
    public decimal? SsFactorVenta { get; set; }
    public string? NoCodigoFiscal { get; set; }

    public int? CoTipo { get; set; }
    public int? CoCategoria { get; set; }
    public int? CoCategoriaHijo { get; set; }
    public int? FlImpuesto { get; set; }
    public int? FlInventario { get; set; }
    public int? FlImpuestoExento { get; set; }
    public string? TxDescripcionCorta { get; set; }


}

public partial class ProductoModel
{
    #region Field join table primary


    public string? NoEstadoAlmacen { get; set; }



    public string? NoMarca { get; set; }
    public string? NoLaboratorio { get; set; }
    public string? NoMoneda { get; set; }
    public string? NoMonedaSigla { get; set; }
    public string? NoImpuesto { get; set; }
    public string? NoImpuestoSigla { get; set; }
    public string? NoUnidadInventario { get; set; }
    public string? NoUnidadCompra { get; set; }
    public string? NoUnidadVenta { get; set; }
    public string? NoTipoPrecio { get; set; }
    public decimal? SsPrecioVenta { get; set; }
    public decimal? SsPrecioVentaMinimo { get; set; }


    public string? NoProductoCompleto =>
        (NoProducto.NoNulo() ? "/ " + NoProducto : "") +
        (NoMarca.NoNulo() ? "/ " + NoMarca : "") +
        (NoModelo.NoNulo() ? "/ " + NoModelo : "");



    public string? NoProductoCompletoCorto =>
      (NoProducto.NoNulo() ? NoProducto : "") +
      (NoMarca.NoNulo() ? "/ " + NoMarca : "") +
      (NoModelo.NoNulo() ? "/ " + NoModelo : "");

    #endregion

    public PersonaModel? Proveedor { get; set; }
    public ProductoStockModel? ProductoStock { get; set; }
    public List<ProductoPrecioModel>? ProductoPrecioLst { get; set; }
    //public InventarioCompraModel? Inventario { get; set; }

    public TipoCambioModel? TipoCambio { get; set; }

    #region Control
    public int? FlSimpleView { get; set; }

    #endregion



    // public FilterModel Filter { get; set; }

}