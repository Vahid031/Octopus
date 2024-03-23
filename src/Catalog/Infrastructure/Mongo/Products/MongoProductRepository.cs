using MongoDB.Driver;
using Octopus.Catalog.Core.Domain.Products.Entities;
using Octopus.Catalog.Core.Domain.Products.Services;
using Octopus.Core.Domain.ValueObjects;
using Octopus.Infrastructure.Mongo.Shared;

namespace Octopus.Catalog.Core.Mongo.Products;

internal class MongoProductRepository : MongoRepositoryBase<Product, ProductId>, IProductRepository
{
    public const string CollectionName = "Products";
    public MongoProductRepository(IUnitOfWork unitOfWork, IMongoCollection<Product> collection)
        : base(unitOfWork, collection)
    {
    }
}
