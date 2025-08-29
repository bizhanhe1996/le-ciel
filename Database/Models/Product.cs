using System.ComponentModel.DataAnnotations;
using LeCiel.Extras.Interfaces;

namespace LeCiel.Database.Models;

public class Product : IModel
{
    [Key]
    public uint Id { get; set; }

    [Required, MaxLength(100)]
    public string Name { get; set; } = null!;

    [Required]
    public int Price { get; set; } = 0;

    [MaxLength(512)]
    public string? Description { get; set; } = null!;
}
