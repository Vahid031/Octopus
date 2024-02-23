using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Octopus.Infrastructure.Mongo.Shared;

namespace Octopus.Presentation.Http;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddMongoServices(this IServiceCollection services)
    {
        services.TryAddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }
}
