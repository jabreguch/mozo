///<summary>
///
///</summary>
///<remarks>
///</remarks>
///<history>
/// t[Jonatan Abregu]	16/11/2018	Created
///</history>

namespace Mozo.Model.Maestro;

public record UbigeoFilter : BaseFilterDto { }

public partial class UbigeoModel : BaseModel //<UbigeoModel>
{
    public string? NoUbigeo { get; set; }
    public string? NoCodigo { get; set; }
    public int? CoUbigeoParent { get; set; }
    //public int? CoUbigeo { get; set; }
    public short? CoNivel { get; set; }
    public int? CoPais { get; set; }

    // public string? CoDepartamento { get; set; }
    // public string? CoProvincia { get; set; }
    // public string? CoDistrito { get; set; }
    // public string? CoUbigeo { get; set; }
    // public string? NoDepartamento { get; set; }
    // public string? NoProvincia { get; set; }
    // public string? NoDistrito { get; set; }
}

public partial class UbigeoModel
{

}