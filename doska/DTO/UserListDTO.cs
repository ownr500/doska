namespace doska.DTO;

public class UserListDTO
{
    public Guid Id { get; set; }
    public bool IsActive { get; set; }
    public string Email { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime CreationDate { get; set; }
}