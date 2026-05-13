namespace Mozo.Model.Maestro;

public record ArchivoFilterDto : BaseFilterDto
{
    public int? CoEmpresa { get; set; }
    public int? CoArchivo { get; set; }
    public int? CoTipoEntidad { get; set; }
    public int? CoEntidad { get; set; }
    public int? CoTipo { get; set; }
    public int? NuOrden { get; set; }


}





public class OrdenArchivoDto
{
    public int CoArchivo { get; set; }
    public int NuOrden { get; set; }
}

public class OrdenMasivoRequest
{
    public int FlEmpresaNotKey { get; set; }
    public int? CoEmpresa { get; set; }
    public List<OrdenArchivoDto> Ordenes { get; set; } = new();
}


[Serializable]
public partial class ArchivoModel : BaseModel //<ArchivoModel>
{

    public int? CoEmpresa { get; set; }
    public int? CoTipoEntidad { get; set; }
    public int? CoEntidad { get; set; }
    public int? CoArchivo { get; set; }
    public int? CoTipo { get; set; }

    public string? NoArchivo { get; set; }
    public string? NoExtension { get; set; }
    public string? NoRuta { get; set; }

    public int? NuOrden { get; set; }
    public string? TxDescripcion { get; set; }
    public int? FlDefault { get; set; }
    public long? NuBytes { get; set; }
    public int? NuAlto { get; set; }
    public int? NuAncho { get; set; }
    public string? NoTitulo { get; set; }

}

public partial class ArchivoModel
{
    public string? Url { get; set; }
    public string? ContentType { get; set; }
    public int? FlGaleria { get; set; }
}