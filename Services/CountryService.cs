using ControllerExamples.Model.Countries;
using ControllerExamples.ServiceContracts;
using Microsoft.AspNetCore.Mvc.ApplicationParts;

namespace ControllerExamples.Services;

public class CountryService : ICountryService
{
    public CountryResponseDto AddCountry(CountryAddRequestDto? model)
    {
        throw new NotImplementedException();
    }
}

