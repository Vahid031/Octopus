using AutoMapper;
using Octopus.Catalog.Core.Contract.Products.Models;

namespace Octopus.Catalog.Presentation.Http.Products;

public class ProductProfile : Profile
{
	public ProductProfile()
	{
		CreateMap<ProductItemDto, ProductItemResponse>();
	}
}
