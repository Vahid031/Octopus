using Microsoft.Extensions.DependencyInjection;

namespace Octopus.FileManager.Core.Application;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddFileManagerApplicationServices(this IServiceCollection services)
    {

        services.AddMediatR(option =>
            option.RegisterServicesFromAssembly(typeof(ServiceCollectionExtension).Assembly));


        return services;
    }
}
