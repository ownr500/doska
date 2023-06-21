namespace doska.DTO;

public class ChangePasswordRequest
{
    public string Password { get; set; }
    public string NewPassword { get; set; }
    public string PasswordConfirmation { get; set; }
}