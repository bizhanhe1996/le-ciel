using LeCiel.Database.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LeCiel.Database;

public class AppContext : IdentityDbContext<User>
{
    public AppContext(DbContextOptions<AppContext> options)
        : base(options) { }

    public DbSet<Product> Products { get; set; }
}
