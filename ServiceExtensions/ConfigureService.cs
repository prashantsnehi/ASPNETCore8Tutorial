using System;
namespace ControllerExamples.ServiceExtensions;

public static class ConfigureService
{
	public static IServiceCollection ConfigureSession(this IServiceCollection services)
	{
		services.AddMvc();
		services.AddSession(options =>
		{
			options.IdleTimeout = TimeSpan.FromMinutes(30);
		});

		return services;
	}
}

