using LeCiel.Database.Models;
using LeCiel.DTOs.Requests;

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
}
