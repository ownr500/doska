namespace doska.DTO;

public class UserInfoResponse
{
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public DateTime CreationDate { get; init; }
}