using Mozo.Helper.Enu;

namespace Mozo.Model.Maestro;

public class PersonaSearchModel : ControlSearchBaseModel
{
    public PersonaSearchModel(int? coModulo, int? coTipoPersona, int? coEmpresa)
    {
        CoEmpresa = coEmpresa;
        CoModulo = coModulo;
        CoTipoPersona = coTipoPersona;
    }

    public PersonaSearchModel()
    {
    }


    public int? CoModulo { get; set; }
    public int? CoTipoPersona { get; set; }



    public int? CoTipoDocumento { get; set; } = EnuTipoGeneral.Maestro.DocumentoPersonaJuridicaYNatural.Ninguno;

    public string? View { get; set; } = "GetById";
}