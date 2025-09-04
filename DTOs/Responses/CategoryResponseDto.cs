namespace LeCiel.DTOs.Responses;

public record CategoryResponseDto(
    uint Id,
    string Name,
    string? Description,
    string CreatedAt,
    string? UpdatedAt
);
