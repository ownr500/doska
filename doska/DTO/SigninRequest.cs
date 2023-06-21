namespace doska.DTO;

internal sealed class SigninRequest
{
    public string Email { get; set; } = default!;
    public string Password { get; set; } = default!;
}