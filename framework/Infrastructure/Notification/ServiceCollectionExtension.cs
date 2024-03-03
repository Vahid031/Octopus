using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Octopus.Core.Contract.Services;

namespace Octopus.Infrastructure.Notification;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddNotificationServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddSingleton<IMessageDispatcher, FakeMessageDispatcher>();

        return services;
    }
}