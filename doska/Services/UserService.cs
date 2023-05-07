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

    public async Task<RegisterResponse> RegisterAsync(RegisterRequest registerRequest)
    {
        var user = new User
        {
            FirstName = registerRequest.FirstName,
            LastName = registerRequest.LastName,
            Email = registerRequest.Email,
            UserName = registerRequest.Email,
            CreationDate = DateTime.Now
        };
        var result = await _userManager.CreateAsync(user, registerRequest.Password);
        var response = new RegisterResponse
        {
            Succeeded = result.Succeeded,
            Errors = result.Errors
        };
        return response;
    }

    public async Task<ActionResult> DeleteAsync()
    {
        var user = await GetCurrentUserAsync();
        var result = await _userManager.DeleteAsync(user);
        if (result.Succeeded) return new OkResult();
        return new NotFoundResult();
    }

    public async Task<ActionResult<ChangePasswordResponse>> ChangePasswordAsync(ChangePasswordRequest changePasswordRequest)
    {
        var user = await GetCurrentUserAsync();
        var result = await _userManager.ChangePasswordAsync(user, changePasswordRequest.Password,
            changePasswordRequest.NewPassword);
        return new ActionResult<ChangePasswordResponse>(new ChangePasswordResponse
        {
            Succeeded = result.Succeeded,
            Errors = result.Errors
        });
    }

    public async Task<UserInfoResponse> GetUserInfoAsync(UserInfoRequest userInfoRequest)
    {
        var user = await _userManager.FindByEmailAsync(userInfoRequest.Email);
        return new UserInfoResponse
        {
            FirstName = user.FirstName,
            LastName = user.LastName,
            CreationDate = user.CreationDate
        };
    }

    public Task<List<UserListDTO>> GetAllUsers()
    {
        IQueryable<User> users = _userManager.Users;
        List<UserListDTO> usersList = users.Select(item => new UserListDTO()
        {
            Id = item.Id,
            IsActive = item.IsActive,
            Email = item.Email,
            FirstName = item.FirstName,
            LastName = item.LastName,
            CreationDate = item.CreationDate
        }).ToList();
        return Task.FromResult(usersList);
    }

    public async Task<User> GetCurrentUserAsync()
    {
        var userClaim = _contextAccessor.HttpContext?.User;
        var userId = _userManager.GetUserId(userClaim);
        return await _userManager.FindByIdAsync(userId);
    }
}