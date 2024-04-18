using System.ComponentModel.DataAnnotations;
using CRUD.DataStructures;

namespace CRUD.Repository
{
    public static class ValidationRepository
    {
        public static void IsDtoValid(this IDto value)
        {
            ValidationContext validationContext = new ValidationContext(value);

            Validator.ValidateObject(value, validationContext, true);
        }
    }
}
