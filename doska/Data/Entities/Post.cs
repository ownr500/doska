namespace doska.Data.Entities;

public class Post
{
    public Guid Id { get; set; } = default!;
    public string Title { get; set; } = default!;
    public bool IsActive { get; set; }
    public string Content { get; set; } = default!;
    public DateTime CreationDate { get; set; }
    public DateTime ExpirationDate { get; set; }
    public Guid? UserId { get; set; } = default;
    public virtual User User { get; init; } = default!;

    public virtual ICollection<Picture> Pictures { get; set; } = new List<Picture>();
    // public virtual ICollection<Picture> Pictures { get; set; } = default!;
}