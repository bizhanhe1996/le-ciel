using LeCiel.Database.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LeCiel.Database;

public class AppContext : IdentityDbContext<User>
{
    public AppContext(DbContextOptions<AppContext> options)
        : base(options) { }

    public DbSet<Product> Products { get; set; }

    public override int SaveChanges()
    {
        UpdateTimestamps();
        return base.SaveChanges();
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        UpdateTimestamps();
        return base.SaveChangesAsync(cancellationToken);
    }

    private void UpdateTimestamps()
    {
        var entries = ChangeTracker
            .Entries()
            .Where(e =>
                e.Entity is Product
                && (e.State == EntityState.Modified || e.State == EntityState.Added)
            );

        foreach (var entityEntry in entries)
        {
            if (entityEntry.State == EntityState.Added)
            {
                ((Product)entityEntry.Entity).CreatedAt = DateTime.UtcNow;
            }
            else if (entityEntry.State == EntityState.Modified)
            {
                ((Product)entityEntry.Entity).UpdatedAt = DateTime.UtcNow;
            }
        }
    }
}
