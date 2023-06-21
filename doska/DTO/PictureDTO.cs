namespace doska.DTO;

internal sealed class PictureDto
{
    public Guid Id { get; set; }
    public byte[] PictureBytes { get; set; } = default!;
}