using System.ComponentModel.DataAnnotations;

namespace CRUD.DataStructures.AttributeService
{
    public class DateValidationAttribute : ValidationAttribute
    {
        public string GetErrorMessage() => "Your Date is not in the right format";

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (DateOnly.TryParse((string)value, out DateOnly parsedDate) == true)
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
