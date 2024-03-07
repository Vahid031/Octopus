using Octopus.Core.Domain.Entities;
using Octopus.Core.Domain.ValueObjects;

namespace Octopus.Core.Domain.Services;

public interface IRepository<TAggregate, TId>
	where TAggregate : AggregateRoot<TId>
	where TId : IIdBase
{
	Task<TAggregate> GetById(TId id);

	Task Insert(TAggregate aggregate);

	Task Update(TAggregate aggregate);

	Task DeleteById(TId id);

	Task<bool> Exists(TId id);

	Task Commit();
}