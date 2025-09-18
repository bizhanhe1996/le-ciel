using LeCiel.Database.Models;

namespace LeCiel.DTOs.Responses;

public record ProductResponseFullDto : ProductResponseSimpleDto
{
    public CategoryResponseSimpleDto? Category { get; init; }
    public ICollection<TagResponseSimpleDto>? Tags { get; init; }
};
