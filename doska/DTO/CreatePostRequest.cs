namespace doska.DTO;

internal sealed class CreatePostRequest
{
    public string Title { get; set; } = default!;
    public string Content { get; set; } = default!;
}