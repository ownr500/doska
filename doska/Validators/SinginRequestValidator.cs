using doska.DTO;
using FluentValidation;

namespace doska.Validators;

internal sealed class SinginRequestValidator : AbstractValidator<SigninRequest>
{
    public SinginRequestValidator()
    {
        RuleFor(request => request.Email).EmailAddress();
    }
}