using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace ControllerExamples.CustomValidator;

public class AmountValidationAttribute : ValidationAttribute
{
	private int _value { get; set; } = 50;
	private string _defaultMessage { get; set; } = "Amount must be multiple of Rs. 50/-";
	private readonly string _propertyName;

	public AmountValidationAttribute(int validationAmount, string propertyName)
	{
		_value = validationAmount;
		_propertyName = propertyName;
	}

    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
		if(value is not null)
		{
			PropertyInfo? propertyInfo = validationContext.ObjectType.GetProperty(_propertyName);
			if (propertyInfo is null) return null;

			var amount = Convert.ToInt32(value);
			if (amount % _value != 0 || amount < 1)
				return new ValidationResult(ErrorMessage ?? _defaultMessage);
			else
				return ValidationResult.Success;
		}
        return null;
    }
}

