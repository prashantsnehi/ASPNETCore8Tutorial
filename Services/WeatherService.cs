using ControllerExamples.ServiceContracts;
using Microsoft.Net.Http.Headers;

namespace ControllerExamples.Services;

public class WeatherService : IWeatherService
{
    private readonly IHttpClientFactory _httpClient;
    public WeatherService(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory;
    }

    public async Task<HttpResponseMessage> GetWeatherForcast()
    {
        using (var httpClient = _httpClient.CreateClient())
        {
            var headers = new Dictionary<string, string>();
            headers.Add("server", "unknown");
            headers.Add("authentication", $"username:password");
            var httpRequest = new HttpRequestMessage()
            {
                RequestUri = new Uri("https://api.github.com/repos/dotnet/AspNetCore.Docs/branches"),
                Method = HttpMethod.Get,
                Headers =
                {
                    { HeaderNames.Accept, "application/vnd.github.v3+json" },
                    { HeaderNames.UserAgent, "HttpRequestsSample" }
                }
            };

            HttpResponseMessage response = await httpClient.SendAsync(httpRequest);
            string responseMessage = await response.Content.ReadAsStringAsync();

            return await Task.FromResult(response);
        }
    }
}

