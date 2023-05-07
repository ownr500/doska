using doska.Data;
using doska.Data.Entities;
using doska.DTO;
using Microsoft.EntityFrameworkCore;

namespace doska.Services;

public class PostService : IPostService
{
    private readonly AppDbContext _appDbContext;
    private readonly IUserService _userService;

    public PostService(AppDbContext appDbContext, IUserService userService)
    {
        _appDbContext = appDbContext;
        _userService = userService;
    }
    public async Task<CreatePostResponse> CreatePost(CreatePostRequest createPostRequest)
    {
        var user = await _userService.GetCurrentUserAsync();
        var currentDate = DateTime.Now;
        var newPost = new Post
        {
            Title = createPostRequest.Title,
            Content = createPostRequest.Content,
            CreationDate = currentDate,
            ExpirationDate = currentDate.AddDays(15),
            UserId = user.Id
        };
        _appDbContext.Posts.Add(newPost);
        try
        {
            await _appDbContext.SaveChangesAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }

        return new CreatePostResponse
        {
            Title = newPost.Title,
            Content = newPost.Content,
            ExpirationDate = newPost.ExpirationDate
        };
    }

    public async Task<List<PostDto>> GetAllPosts()
    {
        return await _appDbContext.Posts
            .Select(post =>
                new PostDto
                {
                    Title = post.Title,
                    Content = post.Content,
                    ExpirationDate = post.ExpirationDate,
                    FirstName = post.User.FirstName,
                    UserId = post.UserId
                }
            )
            .OrderBy(post => post.ExpirationDate)
            .ToListAsync();
    }
}