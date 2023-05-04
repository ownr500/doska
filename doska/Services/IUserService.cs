﻿using doska.Controllers;
using doska.Data.Entities;
using doska.DTO;
using Microsoft.AspNetCore.Mvc;

namespace doska.Services;

public interface IUserService
{
    Task<RegisterResponse> RegisterAsync(RegisterRequest registerRequest);
    Task<ActionResult> DeleteAsync();
    Task<ActionResult<ChangePasswordResponse>> ChangePasswordAsync(ChangePasswordRequest changePasswordRequest);
    Task<UserInfoResponse> GetUserInfoAsync(UserInfoRequest userInfoRequest);
    Task<List<UserListDTO>> GetAllUsers();
}