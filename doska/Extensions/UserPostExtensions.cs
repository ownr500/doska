using doska.Data.Entities;
using doska.DTO;

namespace doska.Extensions;

internal static class UserPostExtensions
{
    public static UserPost ToDto(this Post post)
    {
        return new UserPost
        {
            Id = post.Id,
            Title = post.Title,
            Content = post.Content,
            CreationDate = post.CreationDate,
            PictureDto = post.Pictures.FirstOrDefault()?.ToDto()
        };
    }
}