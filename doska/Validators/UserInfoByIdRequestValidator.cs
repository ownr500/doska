using doska.DTO;
using doska.Services;
using FluentValidation;

namespace doska.Validators;

internal sealed class UserInfoByIdRequestValidator : AbstractValidator<UserInfoByIdRequest>
{
    public UserInfoByIdRequestValidator(IUserService userService)
    {
        RuleFor(request => request.Id)
            .NotNull()
            .NotEmpty()
            .MustAsync(async (id, ct) => await userService.UserExists(id, ct))
            .WithMessage("user not found with given id");
    }
}