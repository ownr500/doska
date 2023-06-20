using doska.DTO;
using doska.Services;
using FluentValidation;

namespace doska.Validators;

internal sealed class DeletePostRequestValidator : AbstractValidator<DeletePostRequest>
{
    public DeletePostRequestValidator(IPermissionsService permissionsService, IPostService postService)
    {
        CascadeMode = CascadeMode.Stop;
        RuleFor(request => request.PostId)
            .NotEmpty()
            .MustAsync(async (postId, ct) => await postService.PostExists(postId, ct))
            .WithMessage("post not found with given id")
            .MustAsync(async (postId, ct) => await permissionsService.UserAuthorNorAdminAsync(postId, ct))
            .WithMessage("no rights to delete");
    }
}