using System.ComponentModel.DataAnnotations;

namespace LeCiel.DTOs.Requests.Product;

public record UpdateProductRequest : CreateProductRequest
{
    [Required]
    public uint Id { get; set; }

    public override Database.Models.Product GetModel()
    {
        return new Database.Models.Product()
        {
            Id = Id,
            Name = Name,
            Price = Price,
            Description = Description,
        };
    }
}
