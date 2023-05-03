using doska.DTO;
using doska.Helper;
using doska.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace doska.Controllers;
[ApiController]
[Route("[controller]/[action]")]
public class UserController : Controller
{
    private readonly IUserService _userService;
    private readonly ISignInService _signInService;

    public UserController(IUserService userService, ISignInService signInService)
    {
        _userService = userService;
        _signInService = signInService;
    }
    [HttpPost]
    public Task<RegisterResponse> Register([FromBody] RegisterRequest registerRequest)
    {
        return _userService.Register(registerRequest);
    }

    [HttpPost]
    public async Task<ActionResult<SigninResponse>> Signin([FromBody] SigninRequest signinRequest)
    {
        return await _signInService.SignIn(signinRequest);
    }

    [HttpPost]
    [Authorize]
    public Task<ActionResult> Delete()
    {
        return _userService.Delete();
    }
}