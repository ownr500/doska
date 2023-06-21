using doska.Data.Entities;
using doska.DTO;

namespace doska.Extensions;

internal static class PictureExtensions
{
    public static PictureDto ToDto(this Picture picture)
    {
        return new PictureDto
        {
            Id = picture.Id,
            PictureBytes = picture.PictureBytes
        };
    }
}