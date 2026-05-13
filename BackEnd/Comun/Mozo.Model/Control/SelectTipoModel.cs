using System.Text.Json.Serialization;
namespace Mozo.Model.Control;

[Serializable]
public partial class SelectTipoModel 
{
    [JsonPropertyName("Value")] public int? Value { get; set; }
    [JsonPropertyName("Text")] public string? Text { get; set; }
    [JsonPropertyName("Select")] public int? Select { get; set; }
    [JsonPropertyName("Orden")] public int? Orden { get; set; }
}