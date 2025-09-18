using System.ComponentModel.DataAnnotations;
using LeCiel.DTOs.Responses;
using Microsoft.EntityFrameworkCore;

namespace LeCiel.Database.Models;

[Index(nameof(Name), IsUnique = true)]
public class Category : BaseModel
{
    [Required, MaxLength(128)]
    public string Name { get; set; } = null!;

    [MaxLength(512)]
    public string? Description { get; set; }

    public ICollection<Product> Products { get; set; } = [];

    public CategoryResponseSimpleDto GetSimpleDto()
    {
        return new CategoryResponseSimpleDto
        {
            Id = Id,
            Name = Name,
            Description = Description,
            CreatedAt = CreatedAt.ToString("yyyy-MM-dd HH:mm:ss"),
            UpdatedAt = UpdatedAt?.ToString("yyyy-MM-dd HH:mm:ss"),
        };
    }

    public CategoryResponseFullDto GetFullDto()
    {
        return new CategoryResponseFullDto
        {
            Id = Id,
            Name = Name,
            Description = Description,
            Products = [.. Products.Select(p => p.GetSimpleDto())],
            CreatedAt = CreatedAt.ToString("yyyy-MM-dd HH:mm:ss"),
            UpdatedAt = UpdatedAt?.ToString("yyyy-MM-dd HH:mm:ss"),
        };
    }
}
