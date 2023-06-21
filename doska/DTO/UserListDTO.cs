namespace doska.DTO;

internal sealed class UserListDto
{
    public Guid Id { get; set; }
    public bool IsActive { get; set; }
    public string Email { get; set; } = default!;
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public DateTime CreationDate { get; set; }
}