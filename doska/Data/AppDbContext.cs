using System.Reflection;
using doska.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace doska.Data;

public class AppDbContext : IdentityDbContext<User, Role, Guid,
  IdentityUserClaim<Guid>, UserRole, IdentityUserLogin<Guid>,
  IdentityRoleClaim<Guid>, IdentityUserToken<Guid>>
{
  // private readonly IDomainEventDispatcher? _dispatcher;

  public AppDbContext(DbContextOptions<AppDbContext> options)
      : base(options)
  {

  }

  public override DbSet<User> Users { get; set; } = default!;
  public override DbSet<IdentityRoleClaim<Guid>> RoleClaims { get; set; } = default!;
  public override DbSet<UserRole> UserRoles { get; set; } = default!;
  public override DbSet<Role> Roles { get; set; } = default!;
  public DbSet<Post> Posts { get; set; } = default!;
  public override DbSet<IdentityUserClaim<Guid>> UserClaims { get; set; } = default!;

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    base.OnModelCreating(modelBuilder);
    modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
  }

  public override async Task<int> SaveChangesAsync(CancellationToken ct = new())
  {
    int result = await base.SaveChangesAsync(ct).ConfigureAwait(false);
    return result;
  }

  public override int SaveChanges()
  {
    return SaveChangesAsync().GetAwaiter().GetResult();
  }
}
