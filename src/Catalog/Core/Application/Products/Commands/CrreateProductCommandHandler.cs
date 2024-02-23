using MediatR;
using Octopus.Catalog.Core.Contract.Products.Commands;
using Octopus.Catalog.Core.Domain.Products.Entities;
using Octopus.Catalog.Core.Domain.Products.Services;
using Octopus.Catalog.Core.Domain.Products.ValueObjects;

namespace Octopus.Catalog.Core.Application.Products.Commands;

internal class CrreateProductCommandHandler : IRequestHandler<CreateProductCommand, ProductId>
{
    private readonly IProductRepository _productRepository;

    public CrreateProductCommandHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<ProductId> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var product = Product.Create(ProductId.New(), request.Name, request.Code, request.Sku);

        await _productRepository.Insert(product);

        await _productRepository.Commit();

        return product.Id;
    }
}
