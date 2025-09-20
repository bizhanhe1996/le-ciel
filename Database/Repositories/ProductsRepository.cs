using LeCiel.Database.Models;
using LeCiel.DTOs.Requests;
using LeCiel.Extras.Interfaces;
using LeCiel.Extras.Utils;
using Microsoft.EntityFrameworkCore;

namespace LeCiel.Database.Repositories;

public class ProductsRepository : BaseRepository, IRepository<Product, ProductUpdateRequestDto>
{
    public ProductsRepository(AppContext context, Paginator paginator)
        : base(context, paginator) { }

    public async Task<Product?> CreateAsync(Product product)
    {
        if (product.TagsIds is not null)
        {
            product.Tags = await _context
                .Tags.Where(t => product.TagsIds.Contains(t.Id))
                .ToListAsync();
            ;
        }
        try
        {
            var insertionResult = _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return insertionResult.Entity;
        }
        catch
        {
            throw;
        }
    }

    public async Task<List<Product>> IndexAsync(int page = 1, int pageSize = 10)
    {
        int totalCount = _context.Products.Count();
        _paginator.SetTotalCount(totalCount).SetPage(page).SetSize(pageSize).Run();

        var products = await _context
            .Products.OrderBy(p => p.Id)
            .Skip(_paginator.SkipCount)
            .Take(_paginator.TakeCount)
            .Include(p => p.Category)
            .Include(p => p.Tags)
            .AsNoTracking()
            .ToListAsync();
        return products;
    }

    public async Task<Product?> FindAsync(uint id)
    {
        var product = await _context
            .Products.Include(p => p.Category)
            .Include(p => p.Tags)
            .FirstOrDefaultAsync(p => p.Id == id);
        if (product == null)
        {
            return null;
        }
        return product;
    }

    public async Task<Product?> UpdateAsync(uint id, ProductUpdateRequestDto dto)
    {
        var product = await _context
            .Products.Include(p => p.Tags)
            .Include(p => p.Category)
            .FirstOrDefaultAsync(p => p.Id == id);
        if (product == null)
        {
            return null;
        }
        product.Name = dto.Name ?? product.Name;
        product.Price = dto.Price ?? product.Price;
        product.Description = dto.Description ?? product.Description;
        product.CategoryId = dto.CategoryId ?? product.CategoryId;
        if (dto.TagsIds is not null)
        {
            product.Tags = await _context.Tags.Where(t => dto.TagsIds.Contains(t.Id)).ToListAsync();
        }
        await _context.SaveChangesAsync();
        return product;
    }

    public async Task<Product?> DeleteAsync(uint id)
    {
        var product = await _context
            .Products.Include(p => p.Category)
            .Include(p => p.Tags)
            .FirstOrDefaultAsync(p => p.Id == id);
        if (product == null)
        {
            return null;
        }
        var result = _context.Products.Remove(product);
        await _context.SaveChangesAsync();
        return result.Entity;
    }
}
