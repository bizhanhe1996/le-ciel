namespace LeCiel.DTOs.Responses;

public record CategoryResponseSimpleDto
{
    public uint Id { get; init; }
    public string Name { get; init; } = null!;
    public string? Description { get; init; }
    public int ProductsCount { get; init; }
    public string CreatedAt { get; init; } = null!;
    public string? UpdatedAt { get; init; }
};
