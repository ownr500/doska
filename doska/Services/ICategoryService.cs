using doska.Data.Entities;
using doska.DTO;
using Microsoft.AspNetCore.Mvc;

namespace doska.Services;

public interface ICategoryService
{
    Task<IEnumerable<Category>> GetCategories();
    Task<IActionResult> AddCategory(AddCategoryRequest addCategoryRequest);
    Task<bool> ExistingCategory(Guid categoryId, CancellationToken ct);
    Task<bool> CategoryNameExists(string name, CancellationToken ct);
    Task<IActionResult> DeleteCategory(DeleteCategoryRequest deleteCategoryRequest);
    Task<IActionResult> EditCategory(EditCategoryRequest editCategoryRequest);
}