using doska.Data;
using doska.Data.Entities;
using doska.DTO;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace doska.Services;

public class UserService : IUserService
{
    private readonly UserManager<User> _userManager;

    public UserService(UserManager<User> userManager)
    {
        _userManager = userManager;
    }

    public async Task<RegisterResponse> Register(RegisterRequest registerRequest)
    {
        var user = new User
        {
            FirstName = registerRequest.FirstName,
            LastName = registerRequest.LastName,
            Email = registerRequest.Email,
            UserName = registerRequest.Email
        };
        var result = await _userManager.CreateAsync(user, registerRequest.Password);
        var response = new RegisterResponse
        {
            Succeeded = result.Succeeded,
            Errors = result.Errors
        };
        return response;
    }
}