using doska.DTO;
using doska.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace doska.Controllers;
[ApiController]
[Route("[controller]/[action]")]
public class PostController : Controller
{
    private readonly IPostService _postService;

    public PostController(IPostService postService)
    {
        _postService = postService;
    }
    
    [HttpPost]
    [Authorize]
    public Task<CreatePostResponse> CreatePost([FromBody]CreatePostRequest createPostRequest)
    {
        return _postService.CreatePost(createPostRequest);
    }

    [HttpPost]
    public Task<List<PostDto>> GetAllPosts()
    {
        return _postService.GetAllPosts();
    }
}