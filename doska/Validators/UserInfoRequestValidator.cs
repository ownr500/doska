using doska.DTO;
using FluentValidation;

namespace doska.Validators;

internal sealed class UserInfoRequestValidator : AbstractValidator<UserInfoRequest>
{
    public UserInfoRequestValidator()
    {
        RuleFor(request => request.Email).EmailAddress();
    }
}