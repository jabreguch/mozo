using System;
using System.Text.Json.Serialization;
namespace Mozo.Comun.Helper.Global
{ 

    [Serializable()]
    public class GlobalSessionMarketinkgModel
    {
        //*Begin Token*//
        [JsonPropertyName("CoIngreso")] public int? CoIngreso { get; set; }
        [JsonPropertyName("CoPermiso")] public int? CoPermiso { get; set; }
        [JsonPropertyName("CoEmpresa")] public int? CoEmpresa { get; set; }
        [JsonPropertyName("CoPersona")] public int? CoPersona { get; set; }    //CoUsuario    
        [JsonPropertyName("NoUsuario")] public string NoUsuario { get; set; }
        [JsonPropertyName("FeCaduca")] public string FeCaduca { get; set; }

        [JsonPropertyName("NoClave")] public string NoClave { get; set; }
        [JsonPropertyName("NoPersona")] public string NoPersona { get; set; }
        [JsonPropertyName("NoApellidoP")] public string NoApellidoP { get; set; }
        [JsonPropertyName("NoApellidoM")] public string NoApellidoM { get; set; }

        //[JsonPropertyName("FeIssued")] public string FeIssued { get; set; }
        [JsonPropertyName("NoCaptchaSistema")] public string NoCaptchaSistema { get; set; }
        //*End Token*//
        //*Begin Usuario*//
        [JsonPropertyName("NoEmpresa")] public string NoEmpresa { get; set; }
        [JsonPropertyName("NoCorreo")] public string NoCorreo { get; set; }
        [JsonPropertyName("NoTelefono")] public string NoTelefono { get; set; }
        [JsonPropertyName("NoCelular")] public string NoCelular { get; set; }
        [JsonPropertyName("NoWhatsapp")] public string NoWhatsapp { get; set; }



        [JsonPropertyName("NoUrl")] public string NoUrl { get; set; }
        [JsonPropertyName("NoUrlFacebook")] public string NoUrlFacebook { get; set; }
        [JsonPropertyName("NoUrlLinkedLn")] public string NoUrlLinkedLn { get; set; }
        [JsonPropertyName("NoUrlTwitter")] public string NoUrlTwitter { get; set; }
        [JsonPropertyName("NoUrlYoutube")] public string NoUrlYoutube { get; set; }
        [JsonPropertyName("NoUrlInstagram")] public string NoUrlInstagram { get; set; }
        [JsonPropertyName("NoDireccion")] public string NoDireccion { get; set; }

    

    }




}