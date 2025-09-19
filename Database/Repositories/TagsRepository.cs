using LeCiel.Database.Models;
using LeCiel.DTOs.Requests;
using LeCiel.Extras.Utils;
using Microsoft.EntityFrameworkCore;

namespace LeCiel.Database.Repositories;

public class TagsRepository(AppContext context, Paginator paginator)
    : BaseRepository(context, paginator)
{
    public async Task<Tag?> CreateAsync(Tag tag)
    {
        var insertedResult = _context.Tags.Add(tag);
        await _context.SaveChangesAsync();
        return insertedResult.Entity;
    }

    public async Task<List<Tag>> IndexAsync(int page = 1, int pageSize = 10)
    {
        int totalCount = _context.Products.Count();
        _paginator.SetTotalCount(totalCount).SetPage(page).SetSize(pageSize).Run();

        var tags = await _context
            .Tags.OrderBy(t => t.Id)
            .Skip(_paginator.SkipCount)
            .Take(_paginator.TakeCount)
            .Include(t => t.Products)
            .AsNoTracking()
            .ToListAsync();
        return tags;
    }

    public async Task<Tag?> FindAsync(uint id)
    {
        var tag = await _context
            .Tags.Include(t => t.Products)
            .AsNoTracking()
            .FirstOrDefaultAsync(t => t.Id == id);
        return tag;
    }

    public async Task<Tag?> UpdateAsync(uint id, TagUpdateRequestDto dto)
    {
        var tag = await _context.Tags.Include(t => t.Products).FirstOrDefaultAsync(t => t.Id == id);
        if (tag == null)
        {
            return null;
        }
        tag.Name = dto.Name ?? tag.Name;
        tag.Description = dto.Description ?? tag.Description;
        await _context.SaveChangesAsync();
        return tag;
    }

    public async Task<Tag?> DeleteAsync(uint id)
    {
        var tag = await _context.Tags.FindAsync(id);
        if (tag == null)
        {
            return null;
        }
        var result = _context.Tags.Remove(tag);
        await _context.SaveChangesAsync();
        return result.Entity;
    }

    public async Task<ICollection<Product>?> ProductsAsync(uint tagId)
    {
        var tag = await _context
            .Tags.Include(t => t.Products)
            .FirstOrDefaultAsync(t => t.Id == tagId);
        if (tag == null)
        {
            return null;
        }
        return tag.Products;
    }
}
