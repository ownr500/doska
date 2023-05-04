using doska.Data.Entities;
using doska.DTO;
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
        return _userService.RegisterAsync(registerRequest);
    }

    [HttpPost]
    public Task<ActionResult<SigninResponse>> Signin([FromBody] SigninRequest signinRequest)
    {
        return _signInService.SignInAsync(signinRequest);
    }

    [HttpPost]
    [Authorize]
    public Task<ActionResult> Delete()
    {
        return _userService.DeleteAsync();
    }

    [HttpPost]
    [Authorize]
    public Task<ActionResult<ChangePasswordResponse>> ChangePassword(ChangePasswordRequest changePasswordRequest)
    {
        return _userService.ChangePasswordAsync(changePasswordRequest);
    }

    [HttpPost]
    [Authorize]
    public Task<UserInfoResponse> GetUserInfo([FromBody]UserInfoRequest userInfoRequest)
    {
        return _userService.GetUserInfoAsync(userInfoRequest);
    }
    
    [HttpGet]
    [Authorize(Roles = "Admin")]
    public Task<List<UserListDTO>> GetUsers()
    {
        return _userService.GetAllUsers();
    }
}