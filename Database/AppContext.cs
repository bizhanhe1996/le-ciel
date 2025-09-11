using LeCiel.Database.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LeCiel.Database;

public class AppContext : IdentityDbContext<User>
{
    public AppContext(DbContextOptions<AppContext> options)
        : base(options) { }

    public DbSet<Product> Products { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Tag> Tags { get; set; }

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

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .Entity<Category>()
            .HasMany(c => c.Products)
            .WithOne(p => p.Category)
            .HasForeignKey(p => p.CategoryId)
            .OnDelete(DeleteBehavior.SetNull);
        base.OnModelCreating(modelBuilder);
    }

    private void UpdateTimestamps()
    {
        var targetStates = new[] { EntityState.Modified, EntityState.Added };
        var entries = ChangeTracker
            .Entries()
            .Where(e => e.Entity is BaseModel && targetStates.Contains(e.State));

        foreach (var entityEntry in entries)
        {
            if (entityEntry.State == EntityState.Added)
            {
                ((BaseModel)entityEntry.Entity).CreatedAt = DateTime.UtcNow;
            }
            else if (entityEntry.State == EntityState.Modified)
            {
                ((BaseModel)entityEntry.Entity).UpdatedAt = DateTime.UtcNow;
            }
        }
    }
}
