using Microsoft.Extensions.DependencyInjection;

namespace Octopus.Partner.Core.Application;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddPartnerApplicationServices(this IServiceCollection services)
    {

        services.AddMediatR(option =>
            option.RegisterServicesFromAssembly(typeof(ServiceCollectionExtension).Assembly));


        return services;
    }
}
