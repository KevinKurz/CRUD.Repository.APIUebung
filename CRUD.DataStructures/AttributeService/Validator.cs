using System.ComponentModel.DataAnnotations;

namespace CRUD.DataStructures.AttributeService
{
    public static class ValidationRepository
    {
        public static void IsValid(this IDto value)
        {
            ValidationContext validationContext = new ValidationContext(value);

            Validator.ValidateObject(value, validationContext, true);
        }
    }
}
