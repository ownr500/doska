using doska.DTO;
using FluentValidation;

namespace doska.Validators;

internal sealed class ChangePasswordRequestValidator : AbstractValidator<ChangePasswordRequest>
{
    public ChangePasswordRequestValidator()
    {
        RuleFor(request => request.Password)
            .MinimumLength(6);
        RuleFor(request => request.NewPassword)
            .MinimumLength(6)
            .Equal(request => request.PasswordConfirmation)
            .WithMessage("Пароли не совпадают");
    }
}