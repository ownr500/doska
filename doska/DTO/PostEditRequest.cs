namespace doska.DTO;

public class PostEditRequest
{
    public Guid PostId { get; set; } = default!;
    public string Title { get; set; } = default!;
    public string Content { get; set; } = default!;
}