namespace ControllerExamples.ServiceContracts;

public interface IWeatherService
{
	Task<HttpResponseMessage> GetWeatherForcast();
}

