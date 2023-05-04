using Microsoft.AspNetCore.Identity;

namespace doska.DTO;

public class ChangePasswordResponse
{
    public bool Succeeded { get; set; }
    public IEnumerable<IdentityError> Errors { get; set; }
}