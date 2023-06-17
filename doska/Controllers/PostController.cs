using doska.Data.Entities;
using doska.DTO;
using doska.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace doska.Controllers;
[ApiController]
[Route("[controller]/[action]")]
public class PostController : ControllerBase
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
        return _postService.CreatePostAsync(createPostRequest);
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> AddPicturesToPost(
        [FromForm] ICollection<IFormFile> addPicturesToPostRequest, Guid postId)
    {
        if (addPicturesToPostRequest.Count > 5)
        {
            return new BadRequestObjectResult("not more then 5 files");
        }

        return await _postService.AddPicturesToPostRequestAsync(addPicturesToPostRequest, postId);
    }
    
    [HttpGet]
    public Task<List<PostDto>> GetAllPosts()
    {
        return _postService.GetAllPostsAsync();
    }

    [HttpGet]
    [Authorize]
    public Task<List<UserPostDto>> GetUserPosts()
    {
        return _postService.GetUserPostsAsync();
    }

    [HttpPost]
    [Authorize]
    public Task<PostEditResponse> EditPost(PostEditRequest postEditRequest)
    {
        return _postService.EditPostAsync(postEditRequest);
    }

    [HttpPost]
    [Authorize]
    public Task<ActionResult> DeletePost(DeletePostRequest deletePostRequest)
    {
        return _postService.DeletePostAsync(deletePostRequest);
    }
    
    
    // [HttpGet]
    // public Task<IActionResult> AddPictures()
    // {
    //     // IActionResult status = new StatusCodeResult(285);
    //     // return Task.FromResult(status);
    //     return Task.FromResult(Test());
    // }
    //
    // private Task<IActionResult> Test()
    // {
    //     return Ok();
    // }
}