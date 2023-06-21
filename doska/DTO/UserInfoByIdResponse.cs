namespace doska.DTO;

internal sealed class UserInfoByIdResponse
{
    public Guid Id { get; set; }
    public string FirstName { get; set; } = default!;
    public DateTime RegistrationDate { get; set; }
    public IEnumerable<UserPostDto>? UserPosts { get; set; }
}