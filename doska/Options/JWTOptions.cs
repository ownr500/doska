namespace doska.Options;

public class JwtOptions
{
    public string ValidIssuer { get; set; } = default!;
    public string ValidAudience { get; set; } = default!;
    public string Secret { get; set; } = default!;
}