namespace doska.DTO;

public class PostEditResponse
{
    public Guid PostId { get; set; }
    public string Title { get; set; } = default!;
    public string Content { get; set; } = default!;
}