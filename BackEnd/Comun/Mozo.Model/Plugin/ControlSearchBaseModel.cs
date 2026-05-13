namespace Mozo.Model;

public class ControlSearchBaseModel
{
    public string? Id { get; set; }
    public string? Value { get; set; }

    public string? NameField { get; set; }
    public int? ValueField { get; set; }
    public bool Required { get; set; }
    public int? CoEmpresa { get; set; }
    public string? NoFuncion { get; set; }


    public string? NoPlaceholder { get; set; }

    public bool FlLoadItems { get; set; } = false;
    public bool FlSimpleView { get; set; } = false;
    public bool FlCreateItem { get; set; } = true;
}