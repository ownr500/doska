using Microsoft.AspNetCore.Identity;

namespace doska.Data.Entities;

public class Role : IdentityRole<Guid>
{
  public virtual List<UserRole> UserRoles { get; init; } = default!;
}
