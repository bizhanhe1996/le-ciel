using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace LeCiel.Database.Models;

[Index(nameof(Name), IsUnique = true)]
public class Product : BaseModel
{
    [Required, MaxLength(128)]
    public string Name { get; set; } = null!;

    [Required]
    public int Price { get; set; } = 0;

    [MaxLength(512)]
    public string? Description { get; set; } = null!;
}
