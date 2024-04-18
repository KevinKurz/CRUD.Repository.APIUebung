using System.ComponentModel.DataAnnotations;

namespace CRUD.Validation.Attributes
{
    public class TimeValidationAttribute : ValidationAttribute
    {
        public string GetErrorMessage() => "Your Time is not in the right format";

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (TimeOnly.TryParse((string)value, out TimeOnly parsedTime))
            {
                return ValidationResult.Success;
            }
            else
            {
                return new ValidationResult(GetErrorMessage());
            }
        }
    }
}
