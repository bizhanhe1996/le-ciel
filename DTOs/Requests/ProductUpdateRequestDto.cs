using System.ComponentModel.DataAnnotations;

namespace LeCiel.DTOs.Requests;

public record ProductUpdateRequestDto
{
    [MaxLength(128)]
    public string? Name { get; set; }

    public int? Price { get; set; }

    [MaxLength(512)]
    public string? Description { get; init; }

    public uint? CategoryId { get; set; }
}
