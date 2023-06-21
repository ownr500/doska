using doska.Data.Entities;
using doska.DTO;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace doska.Services;

public class SignInService : ISignInService
{
    private readonly UserManager<User> _userManager;
    private readonly ITokenService _tokenService;

    public SignInService(UserManager<User> userManager, ITokenService tokenService)
    {
        _userManager = userManager;
        _tokenService = tokenService;
    }
    public async Task<ActionResult<SigninResponse>> SignInAsync(SigninRequest signinRequest)
    {
        var user = await _userManager.FindByEmailAsync(signinRequest.Email);
        if (user == null || !await _userManager.CheckPasswordAsync(user, signinRequest.Password))
        {
            return new SigninResponse
            {
                Success = false,
                Error = "Wrong password"
            };
        }
        return new SigninResponse {Success = true, Token = _tokenService.Generate(user)};
    }
}