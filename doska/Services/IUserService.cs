using doska.DTO;
using Microsoft.AspNetCore.Mvc;

namespace doska.Services;

public interface IUserService
{
    Task<RegisterResponse> Register(RegisterRequest registerRequest);
}