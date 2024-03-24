using Microsoft.Extensions.DependencyInjection;

namespace Octopus.Basket.Core.Application;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddBasketApplicationServices(this IServiceCollection services)
    {

        services.AddMediatR(option =>
            option.RegisterServicesFromAssembly(typeof(ServiceCollectionExtension).Assembly));


        return services;
    }
}
