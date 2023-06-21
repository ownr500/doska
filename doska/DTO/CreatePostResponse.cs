namespace doska.DTO;

public class CreatePostResponse
{
    public Guid Id { get; set; }
    public string Title { get; set; } = default!;
    public string Content { get; set; } = default!;
    public int Price { get; set; } = default!;
    public DateTime ExpirationDate { get; set; }
}