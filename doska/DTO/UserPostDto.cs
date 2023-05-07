namespace doska.DTO;

public class UserPostDto
{
    public string Title { get; set; }  = default!;
    public string Content { get; set; } = default!;
    public DateTime ExpirationDate { get; set; }
}