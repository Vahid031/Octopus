using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using Octopus.Catalog.Core.Domain.Products.Entities;
using Octopus.Catalog.Core.Domain.Products.Services;
using Octopus.Catalog.Core.Mongo.Products;

namespace Octopus.Catalog.Core.Mongo;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddCatalogMongoServices(this IServiceCollection services)
    {
        services.AddScoped<IProductRepository, MongoProductRepository>();






        services.AddSingleton<IMongoDatabase>(sp =>
        {
            var mongoClient = sp.GetRequiredService<IMongoClient>();
            return mongoClient.GetDatabase("UnitOfWorkWithMongoDb");
        });
        services.AddSingleton<IMongoClient>(sp =>
        {
            var mongoClient = new MongoClient("mongodb://vahid:admin1234@localhost:27017/UnitOfWorkWithMongoDb?retryWrites=false");
            return mongoClient;
        });
        services.AddSingleton<IMongoCollection<Product>>(sp =>
        {
            var database = sp.GetRequiredService<IMongoDatabase>();
            return database.GetCollection<Product>("Products");
        });

        return services;
    }
}
