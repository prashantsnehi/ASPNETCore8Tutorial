using System;
namespace ControllerExamples.ServiceContracts;

public interface ICityServices
{
    Guid InstanceId { get; }
    Task<List<string>> GetCities();
}

