using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using Octopus.Catalog.Core.Contract.Products.Services;
using Octopus.Catalog.Core.Domain.Products.Entities;
using Octopus.Catalog.Core.Domain.Products.Services;
using Octopus.Catalog.Core.Mongo.Products;
using Octopus.Core.Domain.Entities;
using Octopus.Core.Domain.ValueObjects;

namespace Octopus.Catalog.Core.Mongo;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddCatalogMongoServices(this IServiceCollection services)
    {
        services.AddScoped<IProductRepository, MongoProductRepository>();
        services.AddSingleton<IProductDataService, MongoProductDataService>();

        services.AddSingleton(sp =>
        {
            var database = sp.GetRequiredService<IMongoDatabase>();
            return database.GetCollection<Product>(MongoProductRepository.CollectionName);
        });

        BsonClassMap.RegisterClassMap<Product>(cm =>
        {
            cm.MapMember(m => m.Sku).SetElementName("Sku");
            cm.MapMember(m => m.Price).SetElementName("Price");
            cm.MapMember(m => m.Code).SetElementName("Code");
            cm.MapMember(m => m.Name).SetElementName("Name");

            cm.SetIgnoreExtraElements(true);
        });

        return services;
    }
}
