using System.ComponentModel.DataAnnotations;
using LeCiel.Extras.Interfaces;

namespace LeCiel.Database.Models;

public class BaseModel : IModel
{
    [Key]
    public uint Id { set; get; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
}
