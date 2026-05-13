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
namespace Mozo.Model.Urbano
{

    [Serializable]
    public class FilterModel : BaseFilterModel
    {
        [JsonPropertyName("FlVencio")] public int? FlVencio { get; set; }
        [JsonPropertyName("CoEtapa")] public int? CoEtapa { get; set; }
        [JsonPropertyName("NoManzana")] public string NoManzana { get; set; }
        [JsonPropertyName("CoModoPago")] public int? CoModoPago { get; set; }
        [JsonPropertyName("FlBono")] public int? FlBono { get; set; }
        [JsonPropertyName("CoProyecto")] public int? CoProyecto { get; set; }
    }

}

