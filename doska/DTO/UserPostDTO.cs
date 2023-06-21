using doska.Data.Entities;

namespace doska.DTO;

public class UserPost
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public DateTime CreationDate { get; set; }
    public PictureDTO? PictureDto { get; set; }
}