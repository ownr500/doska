using doska.Data.Entities;
using doska.DTO;
using doska.Services;
using Microsoft.AspNetCore.Mvc;

namespace doska.Controllers;
[ApiController]
[Route("[controller]/[action]")]
public class AdminController : Controller
{
    private readonly ICategoryService _categoryService;

    public AdminController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }
    [HttpGet]
    public Task<IEnumerable<Category>> GetCategories()
    {
        return _categoryService.GetCategories();
    }

    [HttpPost]
    public Task<IActionResult> AddCategory([FromForm]AddCategoryRequest addCategoryRequest)
    {
        return _categoryService.AddCategory(addCategoryRequest);
    }

    [HttpPost]
    public Task<IActionResult> DeleteCategory([FromForm]DeleteCategoryRequest deleteCategoryRequest)
    {
        return _categoryService.DeleteCategory(deleteCategoryRequest);
    }

    [HttpPost]
    public Task<IActionResult> EditCategory([FromForm] EditCategoryRequest editCategoryRequest)
    {
        return _categoryService.EditCategory(editCategoryRequest);
    }
}