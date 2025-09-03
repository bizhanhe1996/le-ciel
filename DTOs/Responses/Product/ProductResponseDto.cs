namespace LeCiel.DTOs.Responses.Product;

public record ProductResponseDto(
    uint Id,
    string Name,
    int Price,
    string? Description,
    string CreatedAt,
    string? UpdatedAt
);
