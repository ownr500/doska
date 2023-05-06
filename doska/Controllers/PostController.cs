using doska.Data.Entities;
using doska.DTO;
using doska.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace doska.Controllers;
[ApiController]
[Route("[controller]/[action]")]
public class PostController : Controller
{
    private readonly UserManager<User> _userManager;
    private readonly IPostService _postService;

    public PostController(UserManager<User> userManager, IPostService postService)
    {
        _userManager = userManager;
        _postService = postService;
    }
    
    [HttpPost]
    [Authorize]
    public Task<CreatePostResponse> CreatePost([FromBody]CreatePostRequest createPostRequest)
    {
        return _postService.CreatePost(createPostRequest);
    }

    [HttpPost]
    public Task<IEnumerable<PostDTO>> GetAllPosts()
    {
        return _postService.GetAllPosts();
    }
}