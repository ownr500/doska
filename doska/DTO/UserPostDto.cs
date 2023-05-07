namespace doska.DTO;

public class UserPostDto
{
    public Guid PostId { get; set; }
    public string Title { get; set; }  = default!;
    public string Content { get; set; } = default!;
    public DateTime ExpirationDate { get; set; }
}