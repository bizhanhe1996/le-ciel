using System.ComponentModel.DataAnnotations;
using LeCiel.Database.Models;

namespace LeCiel.DTOs.Requests;

public record CategoryCreateRequestDto
{
    [Required, MaxLength(128)]
    public string Name { get; set; } = null!;

    [MaxLength(512)]
    public string? Description { get; set; }

    public Category GetModel()
    {
        return new Category() { Name = Name, Description = Description };
    }
};
