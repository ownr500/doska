using Microsoft.AspNetCore.Identity;

namespace doska.Data.Entities;

public class User : IdentityUser<Guid>
{
  public override Guid Id { get; set; }
  public bool IsActive { get; set; }
  public string FirstName { get; init; } = string.Empty;
  public string LastName { get; init; } = string.Empty;
  public DateTime CreationDate { get; init; } = DateTime.Now;
  public virtual ICollection<UserRole> UserRoles { get; set; } = default!;
  public virtual ICollection<Post> Posts { get; set; } = default!;
  public Guid? PictureId { get; set; } = null;
  public virtual Picture Picture { get; set; } = default!;

  // public bool IsAdministrator => UserExpressions.IsAdministrator.Compile()(this);
  // public bool IsManager => UserExpressions.IsManager.Compile()(this);
  // public bool IsClient => UserExpressions.IsClient.Compile()(this);
}
