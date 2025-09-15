using System.ComponentModel.DataAnnotations;

namespace LeCiel.Extras.Attributes;

public class CategoryExistsAttribute : ValidationAttribute
{
    private Database.AppContext? appContext = null;

    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value == null)
            return ValidationResult.Success;

        SetAppContext(validationContext);
        var category = appContext?.Categories.Find((uint)value);

        return category != null
            ? ValidationResult.Success
            : new ValidationResult($"Category with ID {(uint)value} does not exist.");
    }

    private void SetAppContext(ValidationContext validationContext)
    {
        appContext = validationContext.GetService<Database.AppContext>();
        if (appContext == null)
            throw new Exception("AppContext not available.");
    }
}
