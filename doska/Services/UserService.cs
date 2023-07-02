﻿using doska.Data;
using doska.Data.Entities;
using doska.DTO;
using doska.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace doska.Services;

internal sealed class UserService : IUserService
{
    private readonly UserManager<User> _userManager;
    private readonly IHttpContextAccessor _contextAccessor;
    private readonly AppDbContext _appDbContext;

    public UserService(UserManager<User> userManager, IHttpContextAccessor contextAccessor, AppDbContext appDbContext)
    {
        _userManager = userManager;
        _contextAccessor = contextAccessor;
        _appDbContext = appDbContext;
    }

    public async Task<IActionResult> RegisterAsync(RegisterRequest registerRequest)
    {
        using var memoryStream = new MemoryStream();
        if (registerRequest.Picture != null) await registerRequest.Picture.CopyToAsync(memoryStream);
        var picture = new Picture()
        {
            Id = Guid.NewGuid(),
            PictureBytes = memoryStream.ToArray()
        };

        var user = new User
        {
            IsActive = true,
            FirstName = registerRequest.FirstName,
            LastName = registerRequest.LastName,
            Email = registerRequest.Email,
            UserName = registerRequest.Email,
            PictureId = picture.Id
        };
        _appDbContext.Pictures.Add(picture);
        await _userManager.CreateAsync(user, registerRequest.Password);
        return new OkResult();
    }

    public async Task<ActionResult> DeleteAsync()
    {
        var user = await GetCurrentUserAsync();
        var result = await _userManager.DeleteAsync(user);
        if (result.Succeeded) return new OkResult();
        return new NotFoundResult();
    }

    public async Task<IActionResult> ChangePasswordAsync(ChangePasswordRequest changePasswordRequest)
    {
        var user = await GetCurrentUserAsync();
        var result = await _userManager.ChangePasswordAsync(user, changePasswordRequest.Password,
            changePasswordRequest.NewPassword);
        if (result.Succeeded)
        {
            return new OkResult();
        }

        return new BadRequestObjectResult("Password change failed");
    }

    public async Task<UserInfoResponse> GetUserInfoAsync(UserInfoRequest userInfoRequest)
    {
        var user = await _userManager.FindByEmailAsync(userInfoRequest.Email);
        var userPosts = GetUserPostDtos(user);
        return new UserInfoResponse
        {
            FirstName = user.FirstName,
            LastName = user.LastName,
            CreationDate = user.CreationDate,
            Posts = userPosts
        };
    }

    private static IEnumerable<UserPostDto> GetUserPostDtos(User user)
    {
        var userPosts = user.Posts.Select(post => post.ToDto());
        return userPosts;
    }


    public async Task<UserInfoByIdResponse> GetUserInfoByIdAsync(UserInfoByIdRequest infoByIdRequest)
    {
        var user = await _userManager.FindByIdAsync(infoByIdRequest.Id.ToString());
        var userPosts = GetUserPostDtos(user);

        return new UserInfoByIdResponse
        {
            Id = user.Id,
            FirstName = user.FirstName,
            RegistrationDate = user.CreationDate,
            UserPosts = userPosts
        };
    }

    public List<UserListDto> GetAllUsers()
    {
        var users = _userManager.Users;
        List<UserListDto> usersList = users.Select(item => new UserListDto
        {
            Id = item.Id,
            IsActive = item.IsActive,
            Email = item.Email,
            FirstName = item.FirstName,
            LastName = item.LastName,
            CreationDate = item.CreationDate
        }).ToList();
        return usersList;
    }

    public async Task<User> GetCurrentUserAsync()
    {
        var userClaim = _contextAccessor.HttpContext?.User;
        var userId = _userManager.GetUserId(userClaim);
        return await _userManager.FindByIdAsync(userId);
    }

    public async Task<IActionResult> DeactivateUserAsync(DeactivateUserRequest deactivateUserRequest)
    {
        var user = await _userManager.FindByIdAsync(deactivateUserRequest.Id.ToString());
        user.IsActive = false;
        await _userManager.UpdateAsync(user);
        return new OkResult();
    }

    public async Task<ActionResult> ActivateAllAsync()
    {
        var users = _appDbContext.Users.Where(user => !user.IsActive).ToList();
        users.ForEach(user => user.IsActive = true);
        await _appDbContext.SaveChangesAsync();
        return new OkResult();
    }

    public async Task<bool> UserExists(Guid userId, CancellationToken ct)
    {
        return await _userManager.FindByIdAsync(userId.ToString()) != null;
    }
}