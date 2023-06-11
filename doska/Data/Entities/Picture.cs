namespace doska.Data.Entities;

public class Picture
{
    public Guid Id { get; set; }
    public byte[] PictureBytes { get; set; }
    public virtual ICollection<Post> Posts { get; set; } = default!;
}