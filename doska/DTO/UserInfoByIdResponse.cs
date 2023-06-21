using doska.Data.Entities;

namespace doska.DTO;

public class UserInfoByIdResponse
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public DateTime RegistrationDate { get; set; }
    public IEnumerable<UserPostDto>? UserPosts { get; set; }
}