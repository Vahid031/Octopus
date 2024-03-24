using Microsoft.Extensions.DependencyInjection;

namespace Octopus.Checkout.Infrastructure.Mongo;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddCheckoutMongoServices(this IServiceCollection services)
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
