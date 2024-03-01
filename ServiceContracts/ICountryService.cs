using ControllerExamples.Model.Countries;

namespace ControllerExamples.ServiceContracts;

/// <summary>
/// Business logic to manipulate Country Entity 
/// </summary>
public interface ICountryService
{
    /// <summary>
    /// Adds a country object to the list of countries
    /// </summary>
    /// <param name="model"></param>
    /// <returns>Returns the country object after adding the country to the list</returns>
    CountryResponseDto AddCountry(CountryAddRequestDto? model);

    /// <summary>
    /// Get all countries from the country list
    /// </summary>
    /// <returns>List of CountryResponse Dto</returns>
    List<CountryResponseDto> GetAllCountries();
}

