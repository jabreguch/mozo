namespace Mozo.Model.Inventario;

public class ArticuloSearchModel : ControlSearchBaseModel
{
    //*
    // 1  = Compra
    // 2  = Venta
    //
    public int? CoProceso { get; set; }
    public int? CoLocal { get; set; }
    public int? CoCompra { get; set; }
    public int? CoVenta { get; set; }
}