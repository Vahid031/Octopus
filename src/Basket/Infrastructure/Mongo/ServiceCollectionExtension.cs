using Microsoft.Extensions.DependencyInjection;

namespace Octopus.Basket.Infrastructure.Mongo;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddBasketMongoServices(this IServiceCollection services)
    {
        //services.AddScoped<IProductRepository, MongoProductRepository>();
        //services.AddSingleton<IProductDataService, MongoProductDataService>();

        //services.AddSingleton(sp =>
        //{
        //    var database = sp.GetRequiredService<IMongoDatabase>();
        //    return database.GetCollection<Product>(MongoProductRepository.CollectionName);
        //});


        return services;
    }
}
