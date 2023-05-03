using doska.Data.Entities;

namespace doska.Services;

public interface IAuthService
{
    string GenerateToken(User user);
}