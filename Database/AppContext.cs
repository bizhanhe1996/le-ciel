using LeCiel.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace LeCiel.Database;

public class AppContext : DbContext
{
    public AppContext(DbContextOptions<AppContext> options)
        : base(options) { }

    public DbSet<Product> Products { get; set; }
}
