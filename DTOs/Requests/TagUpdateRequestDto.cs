using System.ComponentModel.DataAnnotations;

namespace LeCiel.DTOs.Requests;

public record TagUpdateRequestDto
{
    [MaxLength(128)]
    public string? Name { get; init; }

    [MaxLength(512)]
    public string? Description { get; init; }
};
