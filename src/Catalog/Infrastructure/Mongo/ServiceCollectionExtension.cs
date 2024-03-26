using Microsoft.Extensions.DependencyInjection;
using Octopus.Catalog.Core.Contract.Products.Services;
using Octopus.Catalog.Core.Domain.Products.Entities;
using Octopus.Catalog.Core.Domain.Products.Services;
using Octopus.Catalog.Infrastructure.Mongo.Products;
using Octopus.Core.Domain.ValueObjects;
using Octopus.Infrastructure.Mongo;

namespace Octopus.Catalog.Infrastructure.Mongo;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddCatalogMongoServices(this IServiceCollection services)
    {
        services.AddScoped<IProductRepository, MongoProductRepository>();
        services.AddSingleton<IProductDataService, MongoProductDataService>();

        services.AddMongoCollection<Product, ProductId>(MongoProductRepository.CollectionName);

        return services;
    }
}
