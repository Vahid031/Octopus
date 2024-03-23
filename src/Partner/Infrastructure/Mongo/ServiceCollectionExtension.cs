using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using Octopus.Partner.Core.Domain.DistributionCenters.Entities;
using Octopus.Partner.Core.Domain.DistributionCenters.Services;
using Octopus.UserManagement.Core.Mongo.Users;

namespace Octopus.Partner.Core.Mongo;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddPartnerMongoServices(this IServiceCollection services)
    {
        services.AddScoped<IDistributionCenterRepository, MongoDistributionCenterRepository>();

        services.AddSingleton(sp =>
        {
            var database = sp.GetRequiredService<IMongoDatabase>();
            return database.GetCollection<DistributionCenter>(MongoDistributionCenterRepository.CollectionName);
        });

        DistributionCenterMapClassExtension.Register();

        return services;
    }
}
