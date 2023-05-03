using doska.Data.Entities;
using doska.DTO;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace doska.Services;

public class SignInService : ISignInService
{
    private readonly UserManager<User> _userManager;
    private readonly IAuthService _authService;

    public SignInService(UserManager<User> userManager, IAuthService authService)
    {
        _userManager = userManager;
        _authService = authService;
    }
    public async Task<ActionResult<SigninResponse>> SignInAsync(SigninRequest signinRequest)
    {
        var user = await _userManager.FindByEmailAsync(signinRequest.Email);
        if (user == null || !await _userManager.CheckPasswordAsync(user, signinRequest.Password))
        {
            return new SigninResponse
            {
                Success = false,
                Error = "FAILED"
            };
        }
        return new SigninResponse {Success = true, Token = _authService.GenerateToken(user)};
    }
}