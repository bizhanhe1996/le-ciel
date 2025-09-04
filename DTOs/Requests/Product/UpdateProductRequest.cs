using System.ComponentModel.DataAnnotations;

namespace LeCiel.DTOs.Requests.Product;

public record UpdateProductRequest
{
    [Required]
    [MaxLength(128)]
    public string? Name { get; set; }

    [Required]
    public int? Price { get; set; }

    [MaxLength(512)]
    public string? Description { get; init; }
}
