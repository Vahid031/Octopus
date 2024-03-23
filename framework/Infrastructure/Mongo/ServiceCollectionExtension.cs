﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using Octopus.Core.Domain.Entities;
using Octopus.Core.Domain.ValueObjects;
using Octopus.Infrastructure.Mongo.Configurations;
using Octopus.Infrastructure.Mongo.Shared;
using Octopus.Infrastructure.Mongo.Shared.BsonSerializers;

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

        BsonSerializer.RegisterSerializer(new UserIdBsonSerializer());
        BsonSerializer.RegisterSerializer(new ProductIdBsonSerializer());
        BsonSerializer.RegisterSerializer(new DistributionCenterIdBsonSerializer());
        BsonSerializer.RegisterSerializer(new BrandIdBsonSerializer());

        BsonClassMap.RegisterClassMap<EntityBase<DistributionCenterId>>(cm =>
        {
            cm.MapMember(m => m.Id).SetElementName("_id");

            cm.SetIgnoreExtraElements(true);
        });

        BsonClassMap.RegisterClassMap<EntityBase<UserId>>(cm =>
        {
            cm.MapMember(m => m.Id).SetElementName("_id");

            cm.SetIgnoreExtraElements(true);
        });

        BsonClassMap.RegisterClassMap<EntityBase<ProductId>>(cm =>
        {
            cm.MapMember(m => m.Id).SetElementName("_id");

            cm.SetIgnoreExtraElements(true);
        });

        BsonClassMap.RegisterClassMap<EntityBase<BrandId>>(cm =>
        {
            cm.MapMember(m => m.Id).SetElementName("_id");

            cm.SetIgnoreExtraElements(true);
        });

        return services;
    }
}
