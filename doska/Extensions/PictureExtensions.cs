using doska.Data.Entities;
using doska.DTO;

namespace doska.Extensions;

internal static class PictureExtensions
{
    public static PictureDTO ToDto(this Picture picture)
    {
        return new PictureDTO
        {
            Id = picture.Id,
            PictureBytes = picture.PictureBytes
        };
    }
}