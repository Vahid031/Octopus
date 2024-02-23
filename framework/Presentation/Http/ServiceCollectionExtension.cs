using Microsoft.Extensions.DependencyInjection;

namespace Octopus.Presentation.Http;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddHttpServices(this IServiceCollection services)
    {
        services.AddControllers();


        return services;
    }
}
