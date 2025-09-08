using System.ComponentModel.DataAnnotations;
using LeCiel.Extras.Attributes;

namespace LeCiel.DTOs.Requests;

public record ProductUpdateRequestDto
{
    [MaxLength(128)]
    public string? Name { get; set; }

    public int? Price { get; set; }

    [MaxLength(512)]
    public string? Description { get; init; }

    [CategoryExistsAttribute]
    public uint? CategoryId { get; set; }
}
