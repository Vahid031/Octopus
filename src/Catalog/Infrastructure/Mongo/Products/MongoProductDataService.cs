using MongoDB.Driver;
using Octopus.Catalog.Core.Contract.Products.Models;
using Octopus.Catalog.Core.Contract.Products.Queries;
using Octopus.Catalog.Core.Contract.Products.Services;
using Octopus.Catalog.Core.Domain.Products.Entities;
using Octopus.Core.Contract.Queries;

namespace Octopus.Catalog.Infrastructure.Mongo.Products;

internal class MongoProductDataService(IMongoCollection<Product> collection) : IProductDataService
{
    public async Task<Pagination<ProductItemDto>> GetByFilter(GetProductItemsByFilterQuery query, CancellationToken cancellationToken)
    {
        var filter = Builders<Product>.Filter.Empty;

        var mongoQuery = collection
              .Find(filter);


        var totalCount = await mongoQuery.CountDocumentsAsync(cancellationToken);

        if (totalCount == 0)
            return Pagination<ProductItemDto>.Empty(query.PageNumber, query.PageSize);

        var items = await mongoQuery
              .Skip(query.Skip)
              .Limit(query.PageSize)
              .Project(x => new ProductItemDto(x.Id, x.Name, "", string.Empty))
              .ToListAsync(cancellationToken);

        if (!items.Any())
            return Pagination<ProductItemDto>.Empty(query.PageNumber, query.PageSize);


        return new Pagination<ProductItemDto>(totalCount, query.PageNumber, query.PageSize, items);
    }
}