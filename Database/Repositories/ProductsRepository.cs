using LeCiel.Database.Models;
using LeCiel.DTOs.Requests;
using Microsoft.EntityFrameworkCore;

namespace LeCiel.Database.Repositories;

public class ProductsRepository(AppContext context) : BaseRepository
{
    private readonly AppContext _context = context;

    public async Task<Product?> CreateAsync(Product product)
    {
        if (product.TagsIds != null && product.TagsIds.Length > 0)
        {
            var tags = await _context.Tags.Where(t => product.TagsIds.Contains(t.Id)).ToListAsync();
            product.Tags = tags;
        }

        var insertionResult = _context.Products.Add(product);
        await _context.SaveChangesAsync();

        insertionResult.Entity.Category = await _context.Categories.FindAsync(product.CategoryId);
        insertionResult.Entity.Tags = product.Tags;
        return insertionResult.Entity;
    }

    public async Task<List<Product>> IndexAsync()
    {
        var products = await _context.Products.Include(p => p.Category).ToListAsync();
        return products;
    }

    public async Task<Product?> FindAsync(uint id)
    {
        var product = await _context.Products.FindAsync(id);
        if (product == null)
        {
            return null;
        }
        product.Category = await _context.Categories.FindAsync(product.CategoryId);
        return product;
    }

    public async Task<Product?> UpdateAsync(uint id, ProductUpdateRequestDto dto)
    {
        var product = await _context.Products.FindAsync(id);
        if (product == null)
        {
            return null;
        }
        product.Name = dto.Name ?? product.Name;
        product.Price = dto.Price ?? product.Price;
        product.Description = dto.Description ?? product.Description;
        product.CategoryId = dto.CategoryId ?? product.CategoryId;
        product.Category = await _context.Categories.FindAsync(product.CategoryId);

        var tags = _context.Tags.Where(t => product.TagsIds.Contains(t.Id)).ToList();
        product.Tags = tags;
        await _context.SaveChangesAsync();
        return product;
    }

    public async Task<Product?> DeleteAsync(uint id)
    {
        var product = await _context.Products.FindAsync(id);
        if (product == null)
        {
            return null;
        }
        var category = await _context.Categories.FindAsync(product.CategoryId);
        var result = _context.Products.Remove(product);
        await _context.SaveChangesAsync();
        result.Entity.Category = category;
        return result.Entity;
    }
}
