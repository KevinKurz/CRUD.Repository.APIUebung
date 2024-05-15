using System.ComponentModel.DataAnnotations;

namespace CRUD.DataStructures.AttributeService
{
    public class EndtimeEarlierThanStarttimeAttribute : ValidationAttribute
    {

        // Set the name of the property to compare with
        private readonly string _comparisonProperty;
        public EndtimeEarlierThanStarttimeAttribute(string comparisonProperty)
        {
            _comparisonProperty = comparisonProperty;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            string comparisonValue = (string)validationContext.ObjectType.GetProperty(_comparisonProperty).GetValue(validationContext.ObjectInstance);

            // Reads the value of the property the attribute is attached to as a string and parse it to a TimeOnly
            if (TimeOnly.TryParse(((string?)value), out TimeOnly startTime) == false)
            {
                return new ValidationResult("Your input is not TimeOnly convertable");
            }

            // Reads the value of the property given in the string parameter and cast it as a string
            if (TimeOnly.TryParse((comparisonValue), out TimeOnly endTime) == false)
            {
                return new ValidationResult("Your input is not TimeOnly convertable");
            }

            int earlier = startTime.CompareTo(endTime);

            if (earlier >= 0)
            {
                return new ValidationResult("Your starttime is not allowed to start after your endtime");
            }

            return ValidationResult.Success;
        }
    }
}
