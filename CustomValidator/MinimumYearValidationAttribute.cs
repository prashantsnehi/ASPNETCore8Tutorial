using System;
using System.ComponentModel.DataAnnotations;

namespace ControllerExamples.CustomValidator;

public class MinimumYearValidationAttribute : ValidationAttribute
{
    private int MinimumYear { get; set; } = 2000;
    private string DefaultErrorMessage { get; set; } = "Year should not be less than {0}";
    // Parameterless constructor
    public MinimumYearValidationAttribute()
    {

    }

    public MinimumYearValidationAttribute(int minimumYear)
    {
        MinimumYear = minimumYear;
    }
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if(value is not null)
        {
            DateTime date = (DateTime)value;
            if (date.Year > MinimumYear)
                return new ValidationResult(string.Format(ErrorMessage ?? DefaultErrorMessage, MinimumYear));
            else
                return ValidationResult.Success;
        }
        return null;
    }
}

