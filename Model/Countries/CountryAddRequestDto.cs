using ControllerExamples.Entity;
namespace ControllerExamples.Model.Countries;

/// <summary>
/// Dto Class to add new country
/// </summary>
public class CountryAddRequestDto
{
	public string? CountryName { get; set; }

	public Country ToCountry()
	{
		return new Country()
		{
			CountryName = this.CountryName
		};
	}
}

