using doska.DTO;

namespace doska.Services;

public interface IPostService
{ 
    Task<CreatePostResponse> CreatePost(CreatePostRequest createPostRequest);
    Task<List<PostDto>> GetAllPosts();
    Task<List<UserPostDto>> GetUserPosts();
}