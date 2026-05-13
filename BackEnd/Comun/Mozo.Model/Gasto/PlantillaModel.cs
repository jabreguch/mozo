///<summary>
///
///</summary>
///<remarks>
///</remarks>
///<history>
/// t[Jonatan Abregu]	16/11/2018	Created
///</history>
namespace Mozo.Model.Gasto;

public record PlantillaFilter : BaseFilterDto
{
}
[Serializable]
public class PlantillaModel : BaseModel //<PlantillaModel>
{


    public int? CoPlantilla { get; set; }



    public int? CoCuenta { get; set; }



    public int? CoTipoMovimiento { get; set; }



    public int? CoCategoria { get; set; }



    public int? CoSubCategoria { get; set; }



    public int? CoBeneficiario { get; set; }



    public string? CoCategoriaSubCategoria { get; set; }
    //FeIngresoIni FeIngresoFin

    public decimal SsMonto { get; set; }
    public string? FeMovimiento { get; set; }
    public string? NoPlantilla { get; set; }
    public string? NoCategoria { get; set; }
    public string? NoCuenta { get; set; }

    public string? NoSubCategoria { get; set; }

    public string? HoMovimiento { get; set; }


    public string? NoUsuarioBeneficiario { get; set; }


    public string? NoUsuarioProveedor { get; set; }

    public string? HoDevolucion { get; set; }
    public int? FlPrestamo { get; set; }



    public int? CoProveedor { get; set; }

    public string? FeDevolucion { get; set; }
}