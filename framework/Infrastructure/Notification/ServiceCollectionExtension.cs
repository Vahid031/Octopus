using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Octopus.Infrastructure.Notification;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddNotificationServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        return services;
    }
}