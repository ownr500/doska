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
    public Task<IActionResult> Register([FromForm] RegisterRequest registerRequest)
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
    public Task<IActionResult> ChangePassword([FromForm]ChangePasswordRequest changePasswordRequest)
    {
        return _userService.ChangePasswordAsync(changePasswordRequest);
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public Task<UserInfoResponse> GetUserInfo([FromBody]UserInfoRequest userInfoRequest)
    {
        return _userService.GetUserInfoAsync(userInfoRequest);
    }

    [HttpPost]
    [Authorize]
    public Task<UserInfoByIdResponse> GetUserInfoById([FromBody] UserInfoByIdRequest infoByIdRequest)
    {
        return _userService.GetUserInfoByIdAsync(infoByIdRequest);
    } 

    [HttpGet]
    [Authorize(Roles = "Admin")]
    public Task<List<UserListDto>> GetUsers()
    {
        return _userService.GetAllUsers();
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public Task<DeactivateUserResponse> DeactivateUser(DeactivateUserRequest deactivateUserRequest)
    {
        return _userService.DeactivateUserAsync(deactivateUserRequest);
    }

    [HttpGet]
    [Authorize(Roles = "Admin")]
    public Task<ActionResult> ActivateAllUsers()
    {
        return _userService.ActivateAllAsync();
    }
    
    [HttpGet]
    [Authorize(Roles = "Admin")]
    public Task<List<UserWithPosts>> GetUsersWithPosts()
    {
        return _userService.GetUsersWithPostsAsync();
    }
}