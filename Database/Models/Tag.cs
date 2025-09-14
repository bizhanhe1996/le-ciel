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

    // Navigation property
    public ICollection<Product> Products { get; set; } = [];

    public TagResponseDto GetDto()
    {
        return new TagResponseDto(
            Id: Id,
            Name: Name,
            Description: Description,
            CreatedAt: CreatedAt.ToString("yyyy-MM-dd HH:mm:ss"),
            UpdatedAt: UpdatedAt?.ToString("yyyy-MM-dd HH:mm:ss")
        );
    }
}
