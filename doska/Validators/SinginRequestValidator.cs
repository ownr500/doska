using doska.DTO;
using FluentValidation;

namespace doska.Validators;

public class SinginRequestValidator : AbstractValidator<SigninRequest>
{
    public SinginRequestValidator()
    {
        RuleFor(request => request.Email).EmailAddress();
    }
}