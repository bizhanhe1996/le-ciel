using System.ComponentModel.DataAnnotations;
using LeCiel.Database.Models;
using LeCiel.Extras.Attributes;

namespace LeCiel.DTOs.Requests;

public record ProductUpdateRequestDto
{
    [MaxLength(128)]
    public string? Name { get; init; }

    public int? Price { get; init; }

    [MaxLength(512)]
    public string? Description { get; init; }

    [CategoryExistsAttribute]
    public uint? CategoryId { get; init; }

    [TagIdsExistAttribute]
    public uint[]? TagsIds { get; init; }

}
