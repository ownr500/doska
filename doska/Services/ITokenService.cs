using doska.Data.Entities;

namespace doska.Services;

internal interface ITokenService
{
    string Generate(User user);
}