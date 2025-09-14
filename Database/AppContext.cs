using LeCiel.Database.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LeCiel.Database;

public class AppContext(DbContextOptions<AppContext> options) : IdentityDbContext<User>(options)
{
    // DbSets
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
        ProductCategoryOneToMany(modelBuilder);
        ProductTagManyToMany(modelBuilder);

        base.OnModelCreating(modelBuilder);
    }

    private static void ProductTagManyToMany(ModelBuilder modelBuilder)
    {
        modelBuilder
            .Entity<Product>()
            .HasMany(p => p.Tags)
            .WithMany(t => t.Products)
            .UsingEntity(j => j.ToTable("ProductTags"));
    }

    private static void ProductCategoryOneToMany(ModelBuilder modelBuilder)
    {
        modelBuilder
            .Entity<Category>()
            .HasMany(c => c.Products)
            .WithOne(p => p.Category)
            .HasForeignKey(p => p.CategoryId)
            .OnDelete(DeleteBehavior.SetNull);
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
