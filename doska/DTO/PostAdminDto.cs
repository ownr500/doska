namespace doska.DTO;

public class PostAdminDto
{
    public Guid Id { get; set; } = default!;
    public string Title { get; set; } = default!;
    public bool IsActive { get; set; }
    public string Content { get; set; } = default!;
    public DateTime CreationDate { get; set; }
    public DateTime ExpirationDate { get; set; }
    public Guid? UserId { get; set; } = default;
}