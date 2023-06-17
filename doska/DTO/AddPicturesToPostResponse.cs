using doska.Data.Entities;

namespace doska.DTO;

public class AddPicturesToPostResponse
{
    public List<Picture> Pictures { get; set; } = default!;
}