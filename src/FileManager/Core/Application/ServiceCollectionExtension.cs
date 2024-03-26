using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Octopus.FileManager.Core.Application.Configuration;

namespace Octopus.FileManager.Core.Application;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddFileManagerApplicationServices(this IServiceCollection services, 
        IConfiguration configuration)
    {

        services.AddMediatR(option =>
            option.RegisterServicesFromAssembly(typeof(ServiceCollectionExtension).Assembly));

        services.AddOptions<CdnOptions>()
            .Bind(configuration.GetSection(nameof(CdnOptions)));

        return services;
    }
}
