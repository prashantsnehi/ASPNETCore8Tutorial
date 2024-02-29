using ControllerExamples.Entity;

namespace ControllerExamples.Model.Countries
{
    /// <summary>
    /// DTO class that is used to return type for most of the Countries methods
    /// </summary>
    public class CountryResponseDto
    {
        public Guid CountryId { get; set; }
        public string? CountryName { get; set; }

        public override bool Equals(object? obj)
        {
            if (obj is null) return false;
            if (obj.GetType() != typeof(CountryAddRequestDto)) return false;

            CountryResponseDto countryToCompare = obj as CountryResponseDto;
            return countryToCompare?.CountryId == this.CountryId
                && countryToCompare.CountryName == this.CountryName;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }

    /// <summary>
    /// Return CountryResponse dto extension method which convert entity to response dto
    /// </summary>
    public static class CountryExtensions
    {
        public static CountryResponseDto ToCountryResponse(this Country country) =>
            new CountryResponseDto() { CountryId = country.CountryId, CountryName = country.CountryName };
    }
}



