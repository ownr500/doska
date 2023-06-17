using doska.Data.Entities;
using doska.DTO;
using Microsoft.AspNetCore.Mvc;

namespace doska.Services;

public interface IPostService
{ 
    Task<CreatePostResponse> CreatePostAsync(CreatePostRequest createPostRequest);
    Task<List<PostDto>> GetAllPostsAsync();
    Task<List<UserPostDto>> GetUserPostsAsync();
    Task<PostEditResponse> EditPostAsync(PostEditRequest postEditRequest);
    Task<ActionResult> DeletePostAsync(DeletePostRequest deletePostRequest);
    Task<IActionResult> AddPicturesToPostRequestAsync(ICollection<IFormFile> addPicturesToPostRequest, Guid postId);
}