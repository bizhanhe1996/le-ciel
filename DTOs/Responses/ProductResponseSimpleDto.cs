namespace LeCiel.DTOs.Responses;

public record ProductResponseSimpleDto
{
    public uint Id { get; init; }
    public string Name { get; init; } = null!;
    public int Price { get; init; }
    public string? Description { get; init; }
    public string CreatedAt { get; init; } = null!;
    public string? UpdatedAt { get; init; }
}
