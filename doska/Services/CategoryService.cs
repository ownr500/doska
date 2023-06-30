using doska.Data;
using doska.Data.Entities;
using doska.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace doska.Services;

public class CategoryService : ICategoryService
{
    private readonly AppDbContext _appDbContext;

    public CategoryService(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }
    public async Task<IEnumerable<Category>> GetCategories()
    {
        return await _appDbContext.Categories.ToListAsync();
    }

    public async Task<IActionResult> AddCategory(AddCategoryRequest addCategoryRequest)
    {
        if (addCategoryRequest.ParentId != null)
        {
            var cat = await _appDbContext.Categories.FirstOrDefaultAsync(cat => cat.Id == addCategoryRequest.ParentId);
            if (cat == null) return new NotFoundResult();
        }

        var category = new Category
        {
            Id = Guid.NewGuid(),
            Name = addCategoryRequest.Name,
            ParentId = addCategoryRequest.ParentId
        };
        _appDbContext.Categories.Add(category);
        await _appDbContext.SaveChangesAsync();
        return new OkResult();
    }


    public async Task<IActionResult> DeleteCategory(DeleteCategoryRequest deleteCategoryRequest)
    {
        var category = await _appDbContext
            .Categories
            .FirstOrDefaultAsync(item => item.Id == deleteCategoryRequest.Id);
        if (category == null)
        {
            return new BadRequestObjectResult("category not found by id");
        } 
        _appDbContext.Categories.Remove(category);
        await _appDbContext.SaveChangesAsync();
        return new OkResult();
    }

    public async Task<IActionResult> EditCategory(EditCategoryRequest editCategoryRequest)
    {
        if (editCategoryRequest.ParentId != null)
        {
            var cat = await _appDbContext.Categories.FirstOrDefaultAsync(cat => cat.Id == editCategoryRequest.ParentId);
            if (cat == null) return new NotFoundResult();
        }

        var itemToEdit = _appDbContext
            .Categories
            .FirstOrDefaultAsync(item => item.Id == editCategoryRequest.Id).Result;
        if (itemToEdit == null)
        {
            return new BadRequestObjectResult("Category not found with given id");
        }
        itemToEdit.Name = editCategoryRequest.Name;
        if (itemToEdit.ParentId == null && editCategoryRequest.ParentId != null)
        {
            itemToEdit.ParentId = editCategoryRequest.ParentId;
        }

        if (itemToEdit.ParentId != null && editCategoryRequest.ParentId == null)
        {
            itemToEdit.ParentId = null;
        }

        if (itemToEdit.ParentId != null && editCategoryRequest.ParentId != null)
        {
            itemToEdit.ParentId = editCategoryRequest.ParentId;
        }
        await _appDbContext.SaveChangesAsync();
        return new OkResult();
    }
    
    
    public async Task<bool> ExistingCategory(Guid categoryId, CancellationToken ct)
    {
        return await _appDbContext.Categories.AnyAsync(category => category.Id == categoryId);
    }

    public async Task<bool> CategoryNameExists(string name, CancellationToken ct)
    {
        //TODO by default returns false if exists, need to rewrite
        return await _appDbContext.Categories.AnyAsync(item => item.Name == name) ? false : true;
    }
}
