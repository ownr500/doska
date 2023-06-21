namespace doska.Services;

internal interface IPermissionsService
{
    Task<bool> UserAuthorNorAdminAsync(Guid postId, CancellationToken ct);
}