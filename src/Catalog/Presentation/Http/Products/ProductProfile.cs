using AutoMapper;
using Octopus.Catalog.Core.Contract.Products.Models;

namespace Octopus.Catalog.Presentation.Http.Products;

public class ProductMappingProfile : Profile
{
	public ProductMappingProfile()
	{
		CreateMap<ProductItemDto, ProductItemResponse>();
	}
}
