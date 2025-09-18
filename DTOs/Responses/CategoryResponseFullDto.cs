using LeCiel.DTOs.Responses;

public record CategoryResponseFullDto : CategoryResponseSimpleDto
{
    public ICollection<ProductResponseSimpleDto> Products { get; init; } = [];
}
