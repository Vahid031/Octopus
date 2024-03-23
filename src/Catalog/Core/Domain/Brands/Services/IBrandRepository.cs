using Octopus.Catalog.Core.Domain.Brands.Entities;
using Octopus.Core.Domain.Services;
using Octopus.Core.Domain.ValueObjects;

namespace Octopus.Catalog.Core.Domain.Brands.Services;

public interface IBrandRepository : IRepository<Brand, BrandId>
{
}
