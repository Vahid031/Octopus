using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Octopus.Core.Application.Outboxes;
using Octopus.Core.Contract.Services;

namespace Octopus.Infrastructure.Mongo;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddHostedService<IntegrationEventPublishWorker>();
        services.AddSingleton<IMessageDispatcher, OutboxMessageDispatcher>();

        services.AddMediatR(option =>
            option.RegisterServicesFromAssembly(typeof(ServiceCollectionExtension).Assembly));

        return services;
    }
}
