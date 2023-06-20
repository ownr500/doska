using doska.DTO;
using FluentValidation;

namespace doska.Validators;

internal sealed class CreatePostRequestValidator : AbstractValidator<CreatePostRequest>
{
    public CreatePostRequestValidator()
    {
        RuleFor(request => request.Title)
            .NotEmpty()
            .MaximumLength(150);

        RuleFor(request => request.Content)
            .NotEmpty()
            .MaximumLength(750);
    }
}