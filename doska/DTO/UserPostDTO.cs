namespace doska.DTO;

public class UserPostDto
{
    public Guid Id { get; set; }
    public string Title { get; set; } = default!;
    public string Content { get; set; } = default!;
    public DateTime CreationDate { get; set; }
    public PictureDto? PictureDto { get; set; }
}
