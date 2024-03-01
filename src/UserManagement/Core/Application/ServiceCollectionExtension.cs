using Microsoft.Extensions.DependencyInjection;

namespace Octopus.UserManagement.Core.Application;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddUserManagmentApplicationServices(this IServiceCollection services)
    {

        services.AddMediatR(option =>
            option.RegisterServicesFromAssembly(typeof(ServiceCollectionExtension).Assembly));


        return services;
    }
}
