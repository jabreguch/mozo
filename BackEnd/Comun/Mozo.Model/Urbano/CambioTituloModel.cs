///<summary>
///
///</summary>
///<remarks>
///</remarks>
///<history>
/// t[Jonatan Abregu]	02/03/2022	Created
///</history>
namespace Mozo.Model.Urbano;

public record CambioTituloFilter : BaseFilterDto { }

[Serializable]
public partial class CambioTituloModel : BaseModel //<CambioTituloModel>
{


    public int? CoProyecto { get; set; }



    public int? CoCredito { get; set; }



    public int? CoCambioTitulo { get; set; }



    public int? CoClienteAntes { get; set; }



    public int? CoDocumentoTributarioAntes { get; set; }



    public string? FeDocumentoAntes { get; set; }



    public string? NuDocumentoAntes { get; set; }




    public int? CoClienteDespues { get; set; }



    public int? CoDocumentoTributarioDespues { get; set; }



    public string? FeDocumentoDespues { get; set; }



    public string? NuDocumentoDespues { get; set; }

    public int? CoEtapa { get; set; }
}

public partial class CambioTituloModel
{
    public string? NoClienteAntes { get; set; }
    public string? NoTipoDocumentoTributarioAntes { get; set; }
    public string? NoClienteDespues { get; set; }
    public string? NoTipoDocumentoTributarioDespues { get; set; }





}