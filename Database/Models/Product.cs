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

    [NotMapped]
    public Category? Category { get; set; }

    [Required]
    public int Price { get; set; } = 0;

    [MaxLength(512)]
    public string? Description { get; set; } = null!;

    public ProductResponseDto GetDto()
    {
        return new ProductResponseDto(
            Id: Id,
            Name: Name,
            Price: Price,
            Description: Description,
            CreatedAt: CreatedAt.ToString("yyyy-MM-dd HH:mm:ss"),
            UpdatedAt: UpdatedAt?.ToString("yyyy-MM-dd HH:mm:ss")
        );
    }
}
