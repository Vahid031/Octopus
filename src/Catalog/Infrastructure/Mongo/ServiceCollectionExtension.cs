using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using Octopus.Catalog.Core.Contract.Products.Services;
using Octopus.Catalog.Core.Domain.Products.Entities;
using Octopus.Catalog.Core.Domain.Products.Services;
using Octopus.Catalog.Core.Mongo.Products;

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


        return services;
    }
}
