using LeCiel.Database.Models;
using LeCiel.DTOs.Requests;
using Microsoft.EntityFrameworkCore;

namespace LeCiel.Database.Repositories;

public class TagsRepository(AppContext context) : BaseRepository
{
    private readonly AppContext _context = context;

    public async Task<Tag?> CreateAsync(Tag tag)
    {
        var insertedResult = _context.Tags.Add(tag);
        await _context.SaveChangesAsync();
        return insertedResult.Entity;
    }

    public async Task<List<Tag>> IndexAsync()
    {
        var tags = await _context.Tags.ToListAsync();
        return tags;
    }

    public async Task<Tag?> FindAsync(uint id)
    {
        var tag = await _context.Tags.FindAsync(id);
        return tag;
    }

    public async Task<Tag?> UpdateAsync(uint id, TagUpdateRequestDto dto)
    {
        var tag = await _context.Tags.FindAsync(id);
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
