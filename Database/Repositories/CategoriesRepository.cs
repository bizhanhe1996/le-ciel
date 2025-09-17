using LeCiel.Database.Models;
using LeCiel.DTOs.Requests;
using Microsoft.EntityFrameworkCore;

namespace LeCiel.Database.Repositories;

public class CategoriesRepository(AppContext context) : BaseRepository
{
    private readonly AppContext _context = context;

    public async Task<Category> CreateAsync(Category category)
    {
        var insertedCategory = _context.Categories.Add(category);
        await _context.SaveChangesAsync();
        return insertedCategory.Entity;
    }

    public async Task<List<Category>> IndexAsync()
    {
        var categories = await _context.Categories.ToListAsync();
        return categories;
    }

    public async Task<Category?> FindAsync(uint id)
    {
        var category = await _context.Categories.FindAsync(id);
        return category;
    }

    public async Task<Category?> UpdateAsync(int id, CategoryUpdateRequestDto dto)
    {
        var category = await FindAsync((uint)id);
        if (category == null)
        {
            return null;
        }
        category.Name = dto.Name ?? category.Name;
        category.Description = dto.Description ?? category.Description;
        await _context.SaveChangesAsync();
        return category;
    }

    public async Task<Category?> DeleteAsync(int id)
    {
        var category = await FindAsync((uint)id);
        if (category == null)
        {
            return null;
        }
        var result = _context.Categories.Remove(category);
        await _context.SaveChangesAsync();
        return result.Entity;
    }

    public async Task<ICollection<Product>?> ProductsAsync(int id)
    {
        var category = await FindAsync((uint)id);
        if (category == null)
        {
            return null;
        }
        return category.Products;
    }
}
