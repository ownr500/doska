namespace doska.DTO;

public class PostDTO
{
    public string Title { get; set; }
    public string Content { get; set; }
    public DateTime ExpirationDate { get; set; }
    public string FirstName { get; set; }
    public Guid UserId { get; set; }
}