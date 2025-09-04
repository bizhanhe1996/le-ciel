using LeCiel.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace LeCiel.Database.Repositories;

public class CategoriesRepository(AppContext context)
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
        var result = await _context.Categories.FindAsync(id);
        return result;
    }
}
