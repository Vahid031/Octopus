using MongoDB.Driver;
using Octopus.Core.Domain.Entities;
using Octopus.Core.Domain.Services;
using Octopus.Core.Domain.ValueObjects;

namespace Octopus.Infrastructure.Mongo.Shared;

public abstract class MongoRepositoryBase<TAggregate, TId>
    : IRepository<TAggregate, TId>
    where TAggregate : AggregateRoot<TId>
    where TId : IIdBase
{
    private readonly IUnitOfWork _unitOfWork;
    protected readonly IMongoCollection<TAggregate> _collection;

    public MongoRepositoryBase(IUnitOfWork unitOfWork,
        IMongoCollection<TAggregate> collection)
    {
        _unitOfWork = unitOfWork;
        _collection = collection;
    }

    public Task Commit() => _unitOfWork.CommitChanges();

    public Task DeleteById(TId id)
    {
        Action operation = () => _collection.DeleteOne(_unitOfWork.Session, x => x.Id.Equals(id));
        _unitOfWork.AddOperation(operation);

        return Task.CompletedTask;
    }

    public Task<bool> Exisits(TId id) => _collection.Find(x => x.Id.Equals(id)).AnyAsync();

    public Task<TAggregate> GetById(TId id) => _collection.Find(x => x.Id.Equals(id)).SingleOrDefaultAsync();

    public Task Insert(TAggregate aggregate)
    {
        Action operation = () => _collection.InsertOne(_unitOfWork.Session, aggregate);
        _unitOfWork.AddOperation(operation);

        return Task.CompletedTask;
    }

    public Task Update(TAggregate aggregate)
    {
        Action operation = () => _collection.ReplaceOne(_unitOfWork.Session, x => x.Id.Equals(aggregate.Id), aggregate);
        _unitOfWork.AddOperation(operation);

        return Task.CompletedTask;
    }
}
