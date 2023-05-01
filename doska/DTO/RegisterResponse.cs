using Microsoft.AspNetCore.Identity;

namespace doska.DTO;

public class RegisterResponse
{
    public bool Succeeded { get; set; }
    public IEnumerable<IdentityError> Errors { get; set; }
}