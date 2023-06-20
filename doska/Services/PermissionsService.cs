using doska.Data;
using doska.Data.Entities;
using Microsoft.AspNetCore.Identity;

namespace doska.Services;

internal sealed class PermissionsService : IPermissionsService
{
    private readonly UserManager<User> _userManager;
    private readonly IUserService _userService;
    private readonly AppDbContext _appDbContext;

    public PermissionsService(UserManager<User> userManager, IUserService userService, AppDbContext appDbContext)
    {
        _userManager = userManager;
        _userService = userService;
        _appDbContext = appDbContext;
    }
    public async Task<bool> UserAuthorNorAdminAsync(Guid postId, CancellationToken ct)
    {
        var post = await _appDbContext.Posts.FindAsync(postId);
        var user = await _userService.GetCurrentUserAsync();
        return post != null && (user.Id == post.UserId || await _userManager.IsInRoleAsync(user, "Admin"));
    }
}