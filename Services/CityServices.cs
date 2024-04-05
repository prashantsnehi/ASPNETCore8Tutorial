using ControllerExamples.ServiceContracts;

namespace ControllerExamples.Services;

public class CityServices : ICityServices
{
	private Guid _instanceId;
	private List<string> _cities;
	public CityServices()
	{
		_instanceId = Guid.NewGuid();
		_cities = new List<string>()
		{
			"Delhi",
			"Panipat",
			"Sonipat",
			"Karnal"
		};
	}

    public Guid InstanceId { get { return _instanceId; } }

    public async Task<List<string>> GetCities() =>
		await Task.FromResult(_cities);
}

