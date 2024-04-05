using ControllerExamples.Entity;
using ControllerExamples.Model.Countries;
using ControllerExamples.ServiceContracts;
using Microsoft.AspNetCore.Mvc.ApplicationParts;

namespace ControllerExamples.Services;

public class CountryService : ICountryService
{
    private readonly List<Country> _countries;
    public CountryService()
    {
        _countries = new List<Country>();
    }
    public CountryResponseDto AddCountry(CountryAddRequestDto? model)
    {
        // Validation: Country model is null
        if (model is null) throw new ArgumentNullException(nameof(CountryAddRequestDto));

        // Validation: CountryName should not be null
        if (model?.CountryName is null) throw new ArgumentException(nameof(model.CountryName));

        // Validation CountryName can't be duplicate
        if (_countries.Where(temp => temp.CountryName.Equals(model.CountryName)).Count() > 0)
            throw new ArgumentException("CountryName already exists");

        // Convert object from requestdto to entity
        var entity = model.ToCountry();

        // Generate CountryId
        entity.CountryId = Guid.NewGuid();

        // add entity to the list
        _countries.Add(entity);

        // return CountryResponseDto
        return entity.ToCountryResponse();
    }

    public List<CountryResponseDto> GetAllCountries() =>
        _countries.Select(country => country.ToCountryResponse()).ToList();

    public CountryResponseDto? GetCountryById(Guid? countryId)
    {
        if (countryId is null) return null;

        return _countries.Select(countryDto => countryDto.ToCountryResponse()).FirstOrDefault(temp => temp.CountryId.Equals(countryId));
    }
}

