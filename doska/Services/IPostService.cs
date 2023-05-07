using doska.DTO;

namespace doska.Services;

public interface IPostService
{ 
    Task<CreatePostResponse> CreatePostAsync(CreatePostRequest createPostRequest);
    Task<List<PostDto>> GetAllPostsAsync();
    Task<List<UserPostDto>> GetUserPostsAsync();
    Task<PostEditResponse> EditPostAsync(PostEditRequest postEditRequest);
}