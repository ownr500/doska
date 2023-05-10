namespace doska.DTO;

public class UserWithPosts
{
    public Guid UserId { get; set; }
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public List<string> Titles { get; set; } = default!;
}