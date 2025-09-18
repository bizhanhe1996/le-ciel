using System.ComponentModel.DataAnnotations;
using LeCiel.DTOs.Responses;
using Microsoft.EntityFrameworkCore;

namespace LeCiel.Database.Models;

[Index(nameof(Name), IsUnique = true)]
public class Tag : BaseModel
{
    [Required, MaxLength(128)]
    public required string Name { get; set; }

    [MaxLength(512)]
    public string? Description { get; set; }

    public ICollection<Product> Products { get; set; } = [];

    public TagResponseSimpleDto GetSimpleDto()
    {
        return new TagResponseSimpleDto
        {
            Id = Id,
            Name = Name,
            Description = Description,
            CreatedAt = CreatedAt.ToString("yyyy-MM-dd HH:mm:ss"),
            UpdatedAt = UpdatedAt?.ToString("yyyy-MM-dd HH:mm:ss"),
        };
    }

    public TagResponseFullDto GetFullDto()
    {
        return new TagResponseFullDto
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
