using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using Octopus.Infrastructure.Mongo.Configurations;
using Octopus.Infrastructure.Mongo.Shared;

namespace Octopus.Infrastructure.Mongo;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddMongoServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.TryAddScoped<IUnitOfWork, UnitOfWork>();

        services.AddOptions<MongoOptions>()
            .Bind(configuration.GetSection(nameof(MongoOptions)));

        services.AddSingleton<IMongoClient>(sp =>
        {
            var mongoOptions = sp.GetRequiredService<IOptions<MongoOptions>>();
            return new MongoClient(mongoOptions.Value.ConnectionString);
        });

        services.AddSingleton<IMongoDatabase>(sp =>
        {
            var mongoOptions = sp.GetRequiredService<IOptions<MongoOptions>>();
            var mongoClient = sp.GetRequiredService<IMongoClient>();
            return mongoClient.GetDatabase(mongoOptions.Value.DatabaseName);
        });

        BsonSerializer.RegisterSerializer(new DateTimeOffsetBsonSerializer());

        return services;
    }
}
