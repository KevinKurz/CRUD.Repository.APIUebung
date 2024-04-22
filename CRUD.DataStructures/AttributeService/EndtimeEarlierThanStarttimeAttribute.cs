using System.ComponentModel.DataAnnotations;

namespace CRUD.DataStructures.AttributeService
{
    public class EndtimeEarlierThanStarttimeAttribute : ValidationAttribute
    {

        // Set the name of the property to compare
        private readonly string _comparisonProperty;
        public EndtimeEarlierThanStarttimeAttribute(string comparisonProperty)
        {
            _comparisonProperty = comparisonProperty;
        }
        public string GetErrorMessage() => "Your starttime is not allowed to start after your endtime";

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            // Reads the value of the property the attribute is attached to as a string and parse it to a TimeOnly
            TimeOnly startTime = TimeOnly.Parse((string)value);

            // Reads the value of the property given in the string parameter and cast it as a string
            string comparisonValue = (string)validationContext.ObjectType.GetProperty(_comparisonProperty).GetValue(validationContext.ObjectInstance);

            TimeOnly endTime = TimeOnly.Parse(comparisonValue);

            int earlier = startTime.CompareTo(endTime);

            if (earlier >= 0)
            {
                return new ValidationResult(GetErrorMessage());
            }

            return ValidationResult.Success;
        }
    }
}
