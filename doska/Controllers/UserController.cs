using doska.DTO;
using doska.Services;
using Microsoft.AspNetCore.Mvc;

namespace doska.Controllers;
[ApiController]
[Route("[controller]/[action]")]
public class UserController : Controller
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }
    [HttpPost]
    public Task<RegisterResponse> Register([FromBody] RegisterRequest registerRequest)
    {
        return _userService.Register(registerRequest);
    }
}