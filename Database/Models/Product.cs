using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using LeCiel.DTOs.Responses;
using Microsoft.EntityFrameworkCore;

namespace LeCiel.Database.Models;

[Index(nameof(Name), IsUnique = true)]
public class Product : BaseModel
{
    [Required, MaxLength(128)]
    public string Name { get; set; } = null!;

    public uint? CategoryId { get; set; }

    public Category? Category { get; set; }

    [Required]
    public int Price { get; set; } = 0;

    [MaxLength(512)]
    public string? Description { get; set; } = null!;

    [NotMapped]
    public uint[]? TagsIds { get; set; }

    public ICollection<Tag> Tags { get; set; } = [];

    public ProductResponseSimpleDto GetSimpleDto()
    {
        return new ProductResponseSimpleDto()
        {
            Id = Id,
            Name = Name,
            Description = Description,
            Price = Price,
            CreatedAt = CreatedAt.ToString("yyyy-MM-dd HH:mm:ss"),
            UpdatedAt = UpdatedAt?.ToString("yyyy-MM-dd HH:mm:ss"),
        };
    }

    public ProductResponseFullDto GetFullDto()
    {
        return new ProductResponseFullDto()
        {
            Id = Id,
            Name = Name,
            Price = Price,
            Description = Description,
            Category = Category?.GetSimpleDto(),
            Tags = [.. Tags.Select(t => t.GetSimpleDto())],
            CreatedAt = CreatedAt.ToString("yyyy-MM-dd HH:mm:ss"),
            UpdatedAt = UpdatedAt?.ToString("yyyy-MM-dd HH:mm:ss"),
        };
    }
}
