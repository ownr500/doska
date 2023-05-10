using doska.Data;
using doska.Data.Entities;
using doska.DTO;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace doska.Services;

public class PostService : IPostService
{
    private readonly AppDbContext _appDbContext;
    private readonly IUserService _userService;
    private readonly UserManager<User> _userManager;

    public PostService(AppDbContext appDbContext, IUserService userService, UserManager<User>  userManager)
    {
        _appDbContext = appDbContext;
        _userService = userService;
        _userManager = userManager;
    }
    public async Task<CreatePostResponse> CreatePostAsync(CreatePostRequest createPostRequest)
    {
        var user = await _userService.GetCurrentUserAsync();
        var currentDate = DateTime.Now;
        var newPost = new Post
        {
            Id = new Guid(),
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
            Id = newPost.Id,
            Title = newPost.Title,
            Content = newPost.Content,
            ExpirationDate = newPost.ExpirationDate
        };
    }

    public async Task<List<PostDto>> GetAllPostsAsync()
    {
        return await _appDbContext.Posts
            .Select(post =>
                new PostDto
                {
                    Id = post.Id,
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

    public async Task<List<UserPostDto>> GetUserPostsAsync()
    {
        var user = await _userService.GetCurrentUserAsync();
        var posts = _appDbContext.Posts.Where(post => post.UserId == user.Id);
        return posts.Select(post => new UserPostDto
        {
            PostId = post.Id,
            Title = post.Title,
            Content = post.Content,
            ExpirationDate = post.ExpirationDate
        }).ToList();
    }

    public async Task<PostEditResponse> EditPostAsync(PostEditRequest postEditRequest)
    {
        var user = await _userService.GetCurrentUserAsync();
        var post = await _appDbContext.Posts.Where(post => post.Id == postEditRequest.PostId).FirstOrDefaultAsync();
        if (post == null)
        {
            return new PostEditResponse();
        }
        post.Title = postEditRequest.Title;
        post.Content = postEditRequest.Content;
        await _appDbContext.SaveChangesAsync();
        return new PostEditResponse
        {
            PostId = post.Id,
            Title = post.Title,
            Content = post.Content
        };
    }

    public async Task<ActionResult> DeletePostAsync(DeletePostRequest deletePostRequest)
    {
        var user = await _userService.GetCurrentUserAsync();
        var post = await _appDbContext.Posts.FindAsync(deletePostRequest.PostId);
        await ThrowIfAuthorNorAdminAsync(post, user);

        _appDbContext.Posts.Remove(post!);
        await _appDbContext.SaveChangesAsync();
        return new OkResult();
    }

    private async Task ThrowIfAuthorNorAdminAsync(Post? post, User user)
    {
        if (post == null || user.Id != post?.UserId || ! await _userManager.IsInRoleAsync(user, "Admin"))
        {
            throw new Exception();
        }
    }
}