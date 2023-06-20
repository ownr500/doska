using doska.DTO;
using FluentValidation;

namespace doska.Validators;

internal sealed class RegisterRequestValidator : AbstractValidator<RegisterRequest>
{
    public RegisterRequestValidator()
    {
        RuleFor(request => request.FirstName)
            .NotEmpty()
            .MaximumLength(35);
        RuleFor(request => request.LastName)
            .NotEmpty()
            .MaximumLength(35);
        RuleFor(request => request.Email)
            .EmailAddress();
        RuleFor(request => request.Password)
            .MinimumLength(6);
    }
}