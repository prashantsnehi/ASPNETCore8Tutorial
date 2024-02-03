using System.ComponentModel.DataAnnotations;
using ControllerExamples.CustomValidator;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace ControllerExamples;

public class Person : IValidatableObject
{
	public string Id { get; set; } = Guid.NewGuid().ToString();

	[Display(Name = "First Name")]
	[RegularExpression("^[A-Za-z .]*$",ErrorMessage = "{0} should contain only alphabets, space and (.)")]
	public required string FirstName { get; set; }

    [Display(Name = "Last Name")]
    [RegularExpression("^[A-Za-z .]*$", ErrorMessage = "{0} should contain only alphabets, space and (.)")]
	[ValidateNever]
    public string? LastName { get; set; }

	[Display(Name = "Email Address")]
	[EmailAddress(ErrorMessage = "Invalid {0}")]
	public string? Email { get; set; }

	[Display(Name = "Phone Number")]
	[Phone(ErrorMessage = "Invalid {0}")]
	public required string Phone { get; set; }

	[MinimumYearValidation(2000, ErrorMessage = "Year should not be greater than {0}")]
	public DateTime? DateOfBirth { get; set; }

	[Range(18, 60, ErrorMessage = "{0} must be between {1} and {2}")]
	public int? Age { get; set; }

	[Required(ErrorMessage = "{0} can't be blank")]
	public required string Password { get; set; }

	[Required(ErrorMessage = "{0} can't be blank")]
	[Compare("Password", ErrorMessage = "{0} and {1} do not match")]
	public required string ConfirmPassword { get; set; }

	public DateTime? FromDate { get; set; }

	public string? SessionId { get; set; }

	[DateRaangeValidator("FromDate", ErrorMessage = "'From Date' should be older than 'To Date'")]
	public DateTime? ToDate { get; set; }
	public override string ToString() =>
		$"Person Object => Person Name: {FirstName} {LastName}, Email: {Email}";

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
		if (DateOfBirth.HasValue == false && Age.HasValue == false)
			yield return new ValidationResult($"{nameof(DateOfBirth)} or {nameof(Age)} must be provided",
				new[] { nameof(Age)});
    }
}

