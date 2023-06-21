using doska.Data.Entities;
using doska.DTO;
using Microsoft.AspNetCore.Mvc;

namespace doska.Services;

public interface IUserService
{
    Task<IActionResult> RegisterAsync(RegisterRequest registerRequest);
    Task<ActionResult> DeleteAsync();
    Task<ActionResult<ChangePasswordResponse>> ChangePasswordAsync(ChangePasswordRequest changePasswordRequest);
    Task<UserInfoResponse> GetUserInfoAsync(UserInfoRequest userInfoRequest);
    Task<List<UserListDto>> GetAllUsers();
    Task<User> GetCurrentUserAsync();
    Task<DeactivateUserResponse> DeactivateUserAsync(DeactivateUserRequest deactivateUserRequest);
    Task<ActionResult> ActivateAllAsync();
    Task<List<UserWithPosts>> GetUsersWithPostsAsync();
    Task<UserInfoByIdResponse> GetUserInfoByIdAsync(UserInfoByIdRequest infoByIdRequest);
    Task<bool> UserExists(Guid userId, CancellationToken ct);
}