using Microsoft.Extensions.DependencyInjection;

namespace Octopus.Catalog.Core.Application;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddCatalogApplicationServices(this IServiceCollection services)
    {

        services.AddMediatR(option =>
            option.RegisterServicesFromAssembly(typeof(ServiceCollectionExtension).Assembly));


        return services;
    }
}
