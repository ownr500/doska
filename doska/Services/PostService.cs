using doska.Data;
using doska.Data.Entities;
using doska.DTO;
using doska.Extensions;
using doska.Options;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace doska.Services;

internal sealed class PostService : IPostService
{
    private readonly AppDbContext _appDbContext;
    private readonly IUserService _userService;
    private readonly PostOptions _options;

    public PostService(AppDbContext appDbContext, IUserService userService, IOptions<PostOptions> options)
    {
        _appDbContext = appDbContext;
        _userService = userService;
        _options = options.Value;
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
            Id = newPost.Id,
            Title = newPost.Title,
            Content = newPost.Content,
            ExpirationDate = newPost.ExpirationDate
        };
    }

    public async Task<List<UserPostDto>> GetAllPostsAsync()
    {
        return await _appDbContext.Posts
            .Select(post => post.ToDto())
            .ToListAsync();
    }

    public async Task<IActionResult> EditPostAsync(PostEditRequest postEditRequest)
    {
        //user post validation
        var post = await _appDbContext.Posts.FindAsync(postEditRequest.PostId);
        if (post == null)
        {
            return new BadRequestObjectResult("no post found");
        }
        
        //picture remove

        if (postEditRequest.IdsToRemove != null)
        {
            foreach (var id in postEditRequest.IdsToRemove)
            {
                var picture = await _appDbContext.Pictures.FindAsync(id);
                if (picture == null)
                {
                    return new BadRequestObjectResult("wrong picture ids");
                }

                _appDbContext.Pictures.Remove(picture);
            }
        }

        //upload new pictures
        if (postEditRequest.Pictures is { Count: > 0 })
        {
            if (postEditRequest.Pictures.Count + post.Pictures.Count > _options.PictureOptions.MaxCount)
            {
                return new BadRequestObjectResult("number of uploaded pics exceeded");
            }

            var picturesToUpload = postEditRequest.Pictures.Select(item =>
            {
                using var memoryStream = new MemoryStream();
                item.CopyTo(memoryStream);
                var pictureBytes = memoryStream.ToArray();
                var newPicture = new Picture
                {
                    Id = Guid.NewGuid(),
                    PictureBytes = pictureBytes
                };
                post.Pictures.Add(newPicture);
                return newPicture;
            });
            await _appDbContext.Pictures.AddRangeAsync(picturesToUpload);
        }

        post.Title = postEditRequest.Title;
        post.Content = postEditRequest.Content;
        
        await _appDbContext.SaveChangesAsync();
        return new OkResult();
    }

    public async Task<ActionResult> DeletePostAsync(DeletePostRequest deletePostRequest,
        CancellationToken ct)
    {
        var post = await _appDbContext.Posts.FindAsync(new object?[] { deletePostRequest.PostId }, cancellationToken: ct);

        _appDbContext.Posts.Remove(post!);
        await _appDbContext.SaveChangesAsync(ct);
        return new OkResult();
    }

    public async Task<IActionResult> AddPicturesToPostRequestAsync(ICollection<IFormFile> addPicturesToPostRequest, Guid postId)
    {
        var post = await _appDbContext.Posts.FirstOrDefaultAsync(post => post.Id == postId);
        var user = await _userService.GetCurrentUserAsync();
        if (post == null) return new BadRequestObjectResult("no post found with given id");
        if (post.UserId != user.Id) return new BadRequestObjectResult("wrong user");
        var pictures = addPicturesToPostRequest.Select(item =>
        {
            using var memoryStream = new MemoryStream();
            item.CopyTo(memoryStream);
            var pictureBytes = memoryStream.ToArray();
            var newPicture = new Picture
            {
                Id = Guid.NewGuid(),
                PictureBytes = pictureBytes
            };
            post.Pictures.Add(newPicture);
            return newPicture;
        });
        _appDbContext.Pictures.AddRange(pictures);
        await _appDbContext.SaveChangesAsync();
        return new OkResult();
    }

    public async Task<bool> PostExists(Guid postId, CancellationToken cancellationToken)
    {
        return await _appDbContext.Posts.FindAsync(postId) != null;
    }
}