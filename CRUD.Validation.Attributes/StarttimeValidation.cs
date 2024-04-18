using System.ComponentModel.DataAnnotations;

namespace CRUD.Validation.Attributes
{
    public class EndtimeEarlierThanStarttimeAttribute : ValidationAttribute
    {

        // Set the name of the property to compare
        private readonly string _comparisonProperty;
        public EndtimeEarlierThanStarttimeAttribute(string comparisonProperty) 
        { 
            _comparisonProperty = comparisonProperty;
        }
        public string GetErrorMessage() => "Your Starttime is not allowed to start after your Endtime";

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            TimeOnly startTime = TimeOnly.Parse((string)value);

            // Gets the property and the value what is given with it
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
