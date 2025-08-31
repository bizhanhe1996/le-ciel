using LeCiel.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace LeCiel.Database.Repositories;

public class ProductsRepository : BaseRepository
{
    private readonly AppContext _context;

    public ProductsRepository(AppContext context)
    {
        _context = context;
    }

    public async Task<List<Product>> GetAllAsync()
    {
        var products = await _context.Products.ToListAsync();
        return products;
    }

    public async Task<Product> CreateAsync(Product product)
    {
        var insertedProduct = _context.Products.Add(product);
        await _context.SaveChangesAsync();
        return insertedProduct.Entity;
    }
}
