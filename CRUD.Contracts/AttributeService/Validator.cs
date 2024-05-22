using System.ComponentModel.DataAnnotations;
using CRUD.Contracts.Queries;

namespace CRUD.Contracts.AttributeService
{
    public static class Validatior
    {
        /// <summary>
        /// <see cref="IQuery"/> can be any DtoObject in this Project. 
        /// </summary>
        /// <param name="value"></param>
        public static void IsValid(this IQuery value)
        {
            ValidationContext validationContext = new ValidationContext(value);

            Validator.ValidateObject(value, validationContext, true);
        }
    }
}
