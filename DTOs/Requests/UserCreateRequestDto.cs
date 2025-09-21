using System.ComponentModel.DataAnnotations;

public record UserCreateRequestDto
{
    [Required, MaxLength(128)]
    public string FirstName { get; set; } = null!;

    [Required, MaxLength(128)]
    public string LastName { get; set; } = null!;

    [Required, EmailAddress]
    public string Email { get; set; } = null!;
}
