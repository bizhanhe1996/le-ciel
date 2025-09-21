using LeCiel.Database.Models;
using LeCiel.Extras.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LeCiel.Database;

public class AppContext(DbContextOptions<AppContext> options)
    : IdentityDbContext<User, IdentityRole<uint>, uint>(options)
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
        base.OnModelCreating(modelBuilder);
        AddUniqueIndexToUserPhoneNumber(modelBuilder);
        ProductCategoryOneToMany(modelBuilder);
        ProductTagManyToMany(modelBuilder);
    }

    private static void AddUniqueIndexToUserPhoneNumber(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>().HasIndex(u => u.PhoneNumber).IsUnique();
    }

    private static void ProductTagManyToMany(ModelBuilder modelBuilder)
    {
        modelBuilder
            .Entity<Product>()
            .HasMany(p => p.Tags)
            .WithMany(t => t.Products)
            .UsingEntity<Dictionary<string, object>>(
                "ProductTags",
                j => j.HasOne<Tag>().WithMany().HasForeignKey("TagId"),
                j => j.HasOne<Product>().WithMany().HasForeignKey("ProductId")
            );
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
            .Where(e => e.Entity is IModel && targetStates.Contains(e.State));

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
