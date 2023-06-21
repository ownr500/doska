﻿using doska.Data.Entities;
using doska.DTO;
using Microsoft.AspNetCore.Mvc;

namespace doska.Services;

internal interface IUserService
{
    Task<IActionResult> RegisterAsync(RegisterRequest registerRequest);
    Task<ActionResult> DeleteAsync();
    Task<IActionResult> ChangePasswordAsync(ChangePasswordRequest changePasswordRequest);
    Task<UserInfoResponse> GetUserInfoAsync(UserInfoRequest userInfoRequest);
    Task<List<UserListDto>> GetAllUsers();
    Task<User> GetCurrentUserAsync();
    Task<IActionResult> DeactivateUserAsync(DeactivateUserRequest deactivateUserRequest);
    Task<ActionResult> ActivateAllAsync();
    Task<UserInfoByIdResponse> GetUserInfoByIdAsync(UserInfoByIdRequest infoByIdRequest);
    Task<bool> UserExists(Guid userId, CancellationToken ct);
}