namespace LeCiel.DTOs.Responses;

public record TagResponseDto(
    uint Id,
    string Name,
    string? Description,
    string CreatedAt,
    string? UpdatedAt
);
