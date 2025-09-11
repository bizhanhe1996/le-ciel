using System.ComponentModel.DataAnnotations;
using LeCiel.Database.Models;

namespace LeCiel.DTOs.Requests;

public record TagCreateRequestDto
{
    [Required, MaxLength(128)]
    public required string Name { get; init; }

    public Tag GetModel()
    {
        return new Tag() { Name = Name };
    }
}
