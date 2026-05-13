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
namespace Mozo.Model.Facturacion
{

    [Serializable]
    public class FilterModel : BaseFilterModel
    {
        [JsonPropertyName("CoTipoDocumento")] public int? CoTipoDocumento { get; set; }
        [JsonPropertyName("CoCliente")] public int? CoCliente { get; set; }
        [JsonPropertyName("NuNotaSalida")] public int? NuNotaSalida { get; set; }

        [JsonPropertyName("CoProveedor")] public int? CoProveedor { get; set; }
        [JsonPropertyName("CoModoPago")] public int? CoModoPago { get; set; }

    }

}

