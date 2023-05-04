using doska.Data.Entities;

namespace doska.Services;

public interface ITokenService
{
    string Generate(User user);
}