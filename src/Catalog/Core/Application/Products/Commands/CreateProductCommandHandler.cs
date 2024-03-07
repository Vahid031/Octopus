using MediatR;
using Octopus.Catalog.Core.Contract.Products.Commands;
using Octopus.Catalog.Core.Contract.Products.Notifications;
using Octopus.Catalog.Core.Domain.Products.Entities;
using Octopus.Catalog.Core.Domain.Products.Services;
using Octopus.Catalog.Core.Domain.Products.ValueObjects;

namespace Octopus.Catalog.Core.Application.Products.Commands;

internal class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, ProductId>
{
    private readonly IProductRepository _productRepository;
    private readonly IMediator _mediator;

    public CreateProductCommandHandler(IProductRepository productRepository, IMediator mediator)
    {
        _productRepository = productRepository;
        _mediator = mediator;
    }

    public async Task<ProductId> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var product = Product.Create(ProductId.New(), request.Name, request.Code, request.Sku);

        await _productRepository.Insert(product);

        await _productRepository.Commit();

        await _mediator.Publish(new ProductCreatedNotification { Id = product.Id });

        return product.Id;
    }
}
