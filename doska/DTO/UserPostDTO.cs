using doska.Data.Entities;

namespace doska.DTO;

public class UserPostDto
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public DateTime CreationDate { get; set; }
    public PictureDto? PictureDto { get; set; }
}