using Octopus.Catalog.Core.Domain.Categories.Entities;
using Octopus.Core.Domain.Services;
using Octopus.Core.Domain.ValueObjects;

namespace Octopus.Catalog.Core.Domain.Categories.Services;

public interface ICategoryRepository : IRepository<Category, CategoryId>
{
}
