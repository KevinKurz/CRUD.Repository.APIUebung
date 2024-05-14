using System.ComponentModel.DataAnnotations;
using CRUD.DataStructures.DTOs;

namespace CRUD.DataStructures.AttributeService
{
    public static class Validatior
    {
        /// <summary>
        /// <see cref="IDto"/> can be any DtoObject in this Project. 
        /// </summary>
        /// <param name="value"></param>
        public static void IsValid(this IDto value)
        {
            ValidationContext validationContext = new ValidationContext(value);

            Validator.ValidateObject(value, validationContext, true);
        }
    }
}
