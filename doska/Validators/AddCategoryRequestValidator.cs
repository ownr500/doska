using doska.DTO;
using doska.Services;
using FluentValidation;

namespace doska.Validators;

public class AddCategoryRequestValidator : AbstractValidator<AddCategoryRequest>
{
    public AddCategoryRequestValidator(ICategoryService categoryService)
    {
        RuleFor(request => request.Name)
            .MinimumLength(4)
            .MaximumLength(50)
            .MustAsync(async (cat, ct) => await categoryService.CategoryNameExists(cat, ct))
            .WithMessage("Category name already exists");
    }
}