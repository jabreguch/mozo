using System.Text.Json.Serialization;

namespace Mozo.Model.Seguridad;

[Serializable]
public class GlobalMenuModel
{
    [JsonPropertyName("PaginaSeleccionada")] public PaginaModel? PaginaSeleccionada { get; set; }
    [JsonPropertyName("SubPaginaSeleccionada")] public SubPaginaModel? SubPaginaSeleccionada { get; set; }
    [JsonPropertyName("ModuloUsuarioCol")] public List<ModuloUsuarioModel>? ModuloUsuarioCol { get; set; }
}