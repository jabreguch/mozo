namespace Mozo.Model.Seguridad.Auth;



[Serializable]
public class TokenModel
{
    public string? NoToken { get; set; }
    public string? NoRefresh { get; set; }
    public long? FeExpiration { get; set; }
    // public long? FeIssued { get; set; }
}
