using doska.DTO;
using doska.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace doska.Controllers;
[ApiController]
[Route("[controller]/[action]")]
public class AdminController : Controller
{
    private readonly IPostService _postService;

    public AdminController(IPostService postService)
    {
        _postService = postService;
    }
    
    [HttpGet]
    [Authorize(Roles = "Admin")]
    public Task<List<PostAdminDto>> GetAllPosts()
    {
        return _postService.GetAllPostsAdminAsync();
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public Task<ActionResult> PostDelete(DeletePostRequest deletePostRequest)
    {
        return _postService.PostDeleteAsync(deletePostRequest);
    }
}