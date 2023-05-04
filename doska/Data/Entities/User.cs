using Microsoft.AspNetCore.Identity;

namespace doska.Data.Entities;

public class User : IdentityUser<Guid>
{
  public bool IsActive { get; init; }
  public string FirstName { get; set; } = string.Empty;
  public string LastName { get; set; } = string.Empty;
  public DateTime CreationDate { get; init; }
  public virtual ICollection<UserRole> UserRoles { get; set; } = default!;
  // public bool IsAdministrator => UserExpressions.IsAdministrator.Compile()(this);
  // public bool IsManager => UserExpressions.IsManager.Compile()(this);
  // public bool IsClient => UserExpressions.IsClient.Compile()(this);
}
