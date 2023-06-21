namespace doska.Services;

public interface IPermissionsService
{
    Task<bool> UserAuthorNorAdminAsync(Guid postId, CancellationToken ct);
}