using System.ComponentModel.DataAnnotations;

namespace LeCiel.Extras.Attributes;

public class TagIdsExistAttribute : ValidationAttribute
{
    private Database.AppContext? _context = null;

    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value == null)
            return ValidationResult.Success;

        if (value is not IEnumerable<uint> tagIds)
            return new ValidationResult("Invalid tag IDs format.");

        SetAppContext(validationContext);

        // Fetch existing tag IDs in one query
        var existingIds = _context
            ?.Tags.Where(t => tagIds.Contains(t.Id))
            .Select(t => t.Id)
            .ToList();

        // Find missing IDs
        var missingIds = tagIds.Except(existingIds ?? []).ToList();

        if (missingIds.Count != 0)
        {
            return new ValidationResult(
                $"These Tag IDs do not exist: {string.Join(", ", missingIds)}",
                [validationContext.MemberName!]
            );
        }

        return ValidationResult.Success;
    }

    private void SetAppContext(ValidationContext validationContext)
    {
        _context = validationContext.GetService<Database.AppContext>();
        if (_context == null)
            throw new Exception("AppContext not available.");
    }
}
