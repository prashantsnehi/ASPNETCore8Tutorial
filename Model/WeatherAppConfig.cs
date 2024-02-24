namespace ControllerExamples.Model;

public class WeatherAppConfig : IWeatherAppConfig
{
	public string? ClientId { get; set; }
	public string? ClientSecret { get; set; }
}

