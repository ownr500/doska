using doska.DTO;
using doska.Services;
using FluentValidation;

namespace doska.Validators;

public class DeleteCategoryRequestValidator : AbstractValidator<DeleteCategoryRequest>
{
    public DeleteCategoryRequestValidator(ICategoryService categoryService)
    {
        CascadeMode = CascadeMode.Stop;
        RuleFor(request => request.Id)
            .NotEmpty()
            .MustAsync(async (id, ct) => await categoryService.ExistingCategory(id, ct))
            .WithMessage("Category not found with given id");
    }
}