namespace doska.DTO;

internal sealed class SigninResponse
{
    public bool Success { get; set; }
    public string? Token { get; set; }
    public string? Error { get; set; }
}