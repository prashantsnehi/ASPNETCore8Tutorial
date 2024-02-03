using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace ControllerExamples.CustomValidator;

public class DateRaangeValidatorAttribute : ValidationAttribute
{
    public string OtherPropertyName { get; set; }
    public DateRaangeValidatorAttribute(string otherPropertyName)
    {
        OtherPropertyName = otherPropertyName;
    }

    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if(value is not null)
        {
            DateTime to_date = Convert.ToDateTime(value);
            PropertyInfo? otherProperty = validationContext.ObjectType.GetProperty(OtherPropertyName);

            if (otherProperty is null) return null;

            var from_Date = Convert.ToDateTime(otherProperty.GetValue(validationContext.ObjectInstance));

            if (from_Date > to_date) return new ValidationResult(ErrorMessage,
                new string[] { OtherPropertyName, validationContext.MemberName });
            else
                return ValidationResult.Success;
        }

        return null;
    }
}