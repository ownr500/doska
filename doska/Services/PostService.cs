using doska.Data;
using doska.Data.Entities;
using doska.DTO;
using Microsoft.AspNetCore.Mvc;
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
    public async Task<CreatePostResponse> CreatePostAsync(CreatePostRequest createPostRequest)
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

    public async Task<List<PostDto>> GetAllPostsAsync()
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
        var post = await _appDbContext.Posts.Where(post => post.Id == postEditRequest.PostId).ToListAsync();
        var postToEdit = post.FirstOrDefault();
        if (postToEdit == null)
        {
            return new PostEditResponse();
        }
        postToEdit.Title = postEditRequest.Title;
        postToEdit.Content = postEditRequest.Content;
        await _appDbContext.SaveChangesAsync();
        return new PostEditResponse
        {
            PostId = postToEdit.Id,
            Title = postToEdit.Title,
            Content = postToEdit.Content
        };
    }

    public async Task<ActionResult> DeletePostAsync(DeletePostRequest deletePostRequest)
    {
        var user = await _userService.GetCurrentUserAsync();
        var post = await _appDbContext.Posts.FindAsync(deletePostRequest.PostId);
        if (post == null || user.Id != post.UserId)
        {
            return new BadRequestResult();
        }

        _appDbContext.Posts.Remove(post);
        await _appDbContext.SaveChangesAsync();
        return new OkResult();
    }

    public async Task<List<PostAdminDto>> GetAllPostsAdminAsync()
    {
        return await _appDbContext.Posts.Select(post => new PostAdminDto
        {
            Id = post.Id,
            Title = post.Title,
            Content = post.Content,
            CreationDate = post.CreationDate,
            ExpirationDate = post.ExpirationDate,
            UserId = post.UserId
        }).ToListAsync();
    }

    public async Task<ActionResult> PostDeleteAsync(DeletePostRequest deletePostRequest)
    {
        var post =  await _appDbContext.Posts.FindAsync(deletePostRequest.PostId);
        if (post != null)
        {
            _appDbContext.Posts.Remove(post);
            await _appDbContext.SaveChangesAsync();
            return new OkResult();
        }
        return new BadRequestResult();
    }
}