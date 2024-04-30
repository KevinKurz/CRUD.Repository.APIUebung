using System.ComponentModel.DataAnnotations;

namespace CRUD.DataStructures.AttributeService
{
    public class DateValidationAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (DateOnly.TryParse((string)value, out DateOnly parsedDate))
            {
                return ValidationResult.Success;
            }
            else
            {
                return new ValidationResult("Your input is not DateOnly convertable");
            }
        }
    }
}
