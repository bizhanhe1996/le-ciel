using System.ComponentModel.DataAnnotations;
using LeCiel.Extras.Attributes;

namespace LeCiel.DTOs.Requests;

public record ProductCreateRequestDto
{
    [Required]
    [MaxLength(128)]
    public string Name { get; init; } = null!;

    [Required]
    public int Price { get; init; }

    [MaxLength(512)]
    public string? Description { get; init; }

    [CategoryExistsAttribute]
    public uint? CategoryId { get; init; }

    [TagIdsExistAttribute]
    public uint[]? TagsIds { get; init; }

    public virtual Database.Models.Product GetModel()
    {
        return new Database.Models.Product()
        {
            Name = Name,
            Price = Price,
            Description = Description,
            CategoryId = CategoryId,
            TagsIds = TagsIds,
        };
    }
}
