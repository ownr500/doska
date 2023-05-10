namespace doska.DTO;

public class PostDto
{
    public Guid Id { get; set; }
    public string Title { get; set; }  = default!;
    public string Content { get; set; } = default!;
    public DateTime ExpirationDate { get; set; }
    public string FirstName { get; set; } = default!;
    public Guid? UserId { get; set; }
}