using LeCiel.Database.Models;
using LeCiel.DTOs.Requests;
using Microsoft.EntityFrameworkCore;

namespace LeCiel.Database.Repositories;

public class ProductsRepository(AppContext context) : BaseRepository
{
    private readonly AppContext _context = context;

    public async Task<Product?> CreateAsync(Product product)
    {
        var insertionResult = _context.Products.Add(product);
        await _context.SaveChangesAsync();
        insertionResult.Entity.Category = await _context.Categories.FindAsync(product.CategoryId);
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
        product.Category = await _context.Categories.FindAsync(product.CategoryId);
        _context.Products.Remove(product);
        await _context.SaveChangesAsync();
        return product;
    }
}
