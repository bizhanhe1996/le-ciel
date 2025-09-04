using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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

    [NotMapped]
    public ICollection<Product> Products { get; set; } = null!;

    public CategoryResponseDto GetDto()
    {
        return new CategoryResponseDto(
            Id: Id,
            Name: Name,
            Description: Description,
            CreatedAt: CreatedAt.ToString("yyyy-MM-dd HH:mm:ss"),
            UpdatedAt: UpdatedAt?.ToString("yyyy-MM-dd HH:mm:ss")
        );
    }
}
