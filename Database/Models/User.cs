using System.ComponentModel.DataAnnotations;
using LeCiel.Extras.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace LeCiel.Database.Models;

public class User : IdentityUser<uint>, IModel
{
    [Required, MaxLength(128)]
    public string FirstName { get; set; } = null!;

    [Required, MaxLength(128)]
    public string LastName { get; set; } = null!;

    [Required]
    public DateTime DateOfBirth { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
}
