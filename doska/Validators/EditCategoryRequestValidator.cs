using doska.DTO;
using doska.Services;
using FluentValidation;

namespace doska.Validators;

public class EditCategoryRequestValidator : AbstractValidator<EditCategoryRequest>
{
    public EditCategoryRequestValidator(ICategoryService categoryService)
    {
        CascadeMode = CascadeMode.Stop;
        RuleFor(request => request.Id)
            .NotEmpty()
            .MustAsync(async (id, ct) => await categoryService.ExistingCategory(id, ct))
            .WithMessage("Category not found with given id");
        RuleFor(request => request.Name)
            .MinimumLength(4)
            .MaximumLength(50)
            .MustAsync(async (cat, ct) => await categoryService.CategoryNameExists(cat, ct))
            .WithMessage("Category name already exists");
    }
}