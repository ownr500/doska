namespace doska.Options;

public class JWTOptions
{
    public string ValidIssuer { get; set; }
    public string ValidAudience { get; set; }
    public string Secret { get; set; }
}