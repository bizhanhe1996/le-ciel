using LeCiel.Database.Models;
using LeCiel.DTOs.Requests;
using LeCiel.Extras.Utils;
using Microsoft.EntityFrameworkCore;

namespace LeCiel.Database.Repositories;

public class CategoriesRepository(AppContext context, Paginator paginator)
    : BaseRepository(context, paginator)
{
    public async Task<Category> CreateAsync(Category category)
    {
        var insertedCategory = _context.Categories.Add(category);
        await _context.SaveChangesAsync();
        return insertedCategory.Entity;
    }

    public async Task<List<Category>> IndexAsync(int page = 1, int pageSize = 10)
    {
        int totalCount = _context.Categories.Count();
        _paginator.SetTotalCount(totalCount).SetPage(page).SetSize(pageSize).Run();

        var categories = await _context
            .Categories.OrderBy(c => c.Id)
            .Skip(_paginator.SkipCount)
            .Take(_paginator.TakeCount)
            .Include(c => c.Products)
            .AsNoTracking()
            .ToListAsync();
        return categories;
    }

    public async Task<Category?> FindAsync(uint id)
    {
        var category = await _context
            .Categories.Include(c => c.Products)
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.Id == id);
        return category;
    }

    public async Task<Category?> UpdateAsync(int id, CategoryUpdateRequestDto dto)
    {
        var category = await _context
            .Categories.Include(c => c.Products)
            .FirstOrDefaultAsync(c => c.Id == id);
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
        var category = await _context
            .Categories.Include(c => c.Products)
            .FirstOrDefaultAsync(c => c.Id == id);
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
        var category = await _context
            .Categories.Include(c => c.Products)
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.Id == id);
        if (category == null)
        {
            return null;
        }
        return category.Products;
    }
}
