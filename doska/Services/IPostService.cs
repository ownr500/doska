using doska.Data.Entities;
using doska.DTO;
using Microsoft.AspNetCore.Mvc;

namespace doska.Services;

public interface IPostService
{ 
    Task<CreatePostResponse> CreatePostAsync(CreatePostRequest createPostRequest);
    Task<List<UserPostDto>> GetAllPostsAsync();
    Task<IActionResult> EditPostAsync(PostEditRequest postEditRequest);
    Task<ActionResult> DeletePostAsync(DeletePostRequest deletePostRequest, CancellationToken ct);
    Task<IActionResult> AddPicturesToPostRequestAsync(ICollection<IFormFile> addPicturesToPostRequest, Guid postId);
    Task<bool> PostExists(Guid postId, CancellationToken cancellationToken);
}