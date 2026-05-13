using System;
using System.Text.Json.Serialization;

///<summary>
///
///</summary>
///<remarks>
///</remarks>
///<history>
/// t[Jonatan Abregu]	16/11/2021	Created
///</history>
namespace Mozo.Model.Inventario
{

    [Serializable]
    public class FilterModel : BaseFilterModel
    {
        [JsonPropertyName("CoArticulo")] public int? CoArticulo { get; set; }
        [JsonPropertyName("CoFamilia")] public int? CoFamilia { get; set; }
        [JsonPropertyName("CoClase")] public int? CoClase { get; set; }
        [JsonPropertyName("CoMarca")] public int? CoMarca { get; set; }

        [JsonPropertyName("CoProveedor")] public int? CoProveedor { get; set; }

        [JsonPropertyName("CoArticuloNorma")] public int? CoArticuloNorma { get; set; }
        [JsonPropertyName("CoLocal")] public int? CoLocal { get; set; }
        [JsonPropertyName("CoAlmacen")] public int? CoAlmacen { get; set; }

        /*Field to Control Search Artículo*/
        [JsonPropertyName("CoProceso")] public int? CoProceso { get; set; }


        [JsonPropertyName("CoSegmentoGrupo")] public int? CoSegmentoGrupo { get; set; }
        [JsonPropertyName("CoSegmento")] public int? CoSegmento { get; set; }

        [JsonPropertyName("CoCompra")] public int? CoCompra { get; set; }
        [JsonPropertyName("CoVenta")] public int? CoVenta { get; set; }

    }

}

