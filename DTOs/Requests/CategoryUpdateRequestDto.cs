using System.ComponentModel.DataAnnotations;

namespace LeCiel.DTOs.Requests;

public record CategoryUpdateRequestDto
{
    [MaxLength(128)]
    public string? Name { get; set; }

    [MaxLength(512)]
    public string? Description { get; set; }
}
