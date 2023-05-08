using Ardalis.ApiEndpoints;
using doska.DTO;
using doska.Services;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace doska.Endpoints.Post;

public class GetPosts : EndpointBaseAsync
    .WithoutRequest
    .WithResult<List<PostDto>>
{
    private readonly IPostService _postService;

    public GetPosts(IPostService postService)
    {
        _postService = postService;
    }
    
    [HttpGet("posts")]
    [SwaggerOperation( Summary = "Get all posts",
        Description = "Display all posts",
        OperationId = "Posts.GetAll",
        Tags = new [] {"PostEndpoint"})]
    public override Task<List<PostDto>> HandleAsync(CancellationToken cancellationToken = default)
    {
        return _postService.GetAllPostsAsync();
    }
}