using doska.Data.Entities;

namespace doska.DTO;

internal sealed class AddPicturesToPostResponse
{
    public List<Picture> Pictures { get; set; } = default!;
}