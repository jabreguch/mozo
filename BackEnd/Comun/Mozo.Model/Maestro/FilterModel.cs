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
namespace Mozo.Model.Maestro
{

    [Serializable]
    public class FilterModel : BaseFilterModel
    {
        [JsonPropertyName("CoTipoPersona")] public int? CoTipoPersona { get; set; }
        [JsonPropertyName("CoTipoPersona")] public int? CoModulo { get; set; }
        [JsonPropertyName("CoTipoDocumento")] public int? CoTipoDocumento { get; set; }
        /*Tipo*/
        [JsonPropertyName("CoGrupoPadre")] public int? CoGrupoPadre { get; set; }
        [JsonPropertyName("CoTipoPadre")] public int? CoTipoPadre { get; set; }
        [JsonPropertyName("FlGeneral")] public int? FlGeneral { get; set; }
        [JsonPropertyName("CoGrupo")] public int? CoGrupo { get; set; }

    }

}

