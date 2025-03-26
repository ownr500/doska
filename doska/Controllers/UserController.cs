using Microsoft.AspNetCore.Mvc;

namespace doska.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly ILogger<UserController> _logger;

    public UserController(ILogger<UserController> logger)
    {
        _logger = logger;
    }

    [HttpPost(Name = "api/register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequestDto requestDto, CancellationToken ct)
    {
        var result = await _userService.RegisterAsync(requestDto.ToModel(), ct);
    }
}