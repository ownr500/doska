using doska.Data;
using doska.Data.Entities;
using doska.DTO;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace doska.Services;

public class UserService : IUserService
{
    private readonly UserManager<User> _userManager;
    private readonly IHttpContextAccessor _contextAccessor;

    public UserService(UserManager<User> userManager, IHttpContextAccessor contextAccessor)
    {
        _userManager = userManager;
        _contextAccessor = contextAccessor;
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

    public async Task<ActionResult> Delete()
    {
        var userClaim = _contextAccessor.HttpContext?.User;
        var userId = _userManager.GetUserId(userClaim);
        var user = await _userManager.FindByIdAsync(userId);
        var result = await _userManager.DeleteAsync(user);
        if (result.Succeeded) return new OkResult();
        return new NotFoundResult();
    }
}