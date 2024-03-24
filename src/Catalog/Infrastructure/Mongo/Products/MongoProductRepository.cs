using MongoDB.Driver;
using Octopus.Catalog.Core.Domain.Products.Entities;
using Octopus.Catalog.Core.Domain.Products.Services;
using Octopus.Core.Domain.ValueObjects;
using Octopus.Infrastructure.Mongo.Shared;

namespace Octopus.Catalog.Infrastructure.Mongo.Products;

internal class MongoProductRepository : MongoRepositoryBase<Product, ProductId>, IProductRepository
{
    public const string CollectionName = "Products";
    public MongoProductRepository(IUnitOfWork unitOfWork, IMongoCollection<Product> collection)
        : base(unitOfWork, collection)
    {
    }
}
