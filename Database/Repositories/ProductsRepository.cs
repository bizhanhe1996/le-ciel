using LeCiel.Database.Models;
using LeCiel.DTOs.Requests.Product;
using Microsoft.EntityFrameworkCore;

namespace LeCiel.Database.Repositories;

public class ProductsRepository(AppContext context) : BaseRepository
{
    private readonly AppContext _context = context;

    public async Task<Product> CreateAsync(Product product)
    {
        var insertedProduct = _context.Products.Add(product);
        await _context.SaveChangesAsync();
        return insertedProduct.Entity;
    }

    public async Task<List<Product>> GetAllAsync()
    {
        var products = await _context.Products.ToListAsync();
        return products;
    }

    public async Task<Product?> FindAsync(uint id)
    {
        var product = await _context.Products.FindAsync(id);
        return product;
    }

    public async Task<bool?> UpdateAsync(uint id, UpdateProductRequest updateProductRequest)
    {
        var product = await _context.Products.FindAsync(id);
        if (product == null)
        {
            return false;
        }
        _context.Entry(product).CurrentValues.SetValues(updateProductRequest.GetModel());
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(uint id)
    {
        var product = await _context.Products.FindAsync(id);
        if (product == null)
        {
            return false;
        }
        _context.Products.Remove(product);
        await _context.SaveChangesAsync();
        return true;
    }
}
