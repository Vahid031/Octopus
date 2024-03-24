using Microsoft.Extensions.DependencyInjection;

namespace Octopus.Checkout.Core.Application;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddCheckoutApplicationServices(this IServiceCollection services)
    {

        services.AddMediatR(option =>
            option.RegisterServicesFromAssembly(typeof(ServiceCollectionExtension).Assembly));


        return services;
    }
}
