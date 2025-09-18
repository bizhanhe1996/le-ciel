namespace LeCiel.DTOs.Responses;

public record TagResponseFullDto : TagResponseSimpleDto
{
    public ICollection<ProductResponseSimpleDto> Products { get; init; } = [];
}
