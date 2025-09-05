using LeCiel.Database.Models;

namespace LeCiel.DTOs.Responses;

public record ProductResponseDto(
    uint Id,
    string Name,
    int Price,
    string? Description,
    CategoryResponseDto? Category,
    string CreatedAt,
    string? UpdatedAt
);
