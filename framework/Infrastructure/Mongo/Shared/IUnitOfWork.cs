using MongoDB.Driver;

namespace Octopus.Infrastructure.Mongo.Shared;

public interface IUnitOfWork
{
    IClientSessionHandle Session { get; }

    void AddOperation(Action operation);

    void CleanOperations();

    Task CommitChanges();
}

internal sealed class UnitOfWork : IUnitOfWork
{
    public IClientSessionHandle Session { get; }

    private List<Action> _operations { get; set; }

    public UnitOfWork(IMongoClient mongoClient)
    {
        Session = mongoClient.StartSession();

        _operations = new List<Action>();
    }

    public void AddOperation(Action operation)
    {
        _operations.Add(operation);
    }

    public void CleanOperations()
    {
        _operations.Clear();
    }

    public async Task CommitChanges()
    {
        Session.StartTransaction();

        _operations.ForEach(o =>
        {
            o.Invoke();
        });

        await Session.CommitTransactionAsync();

        CleanOperations();
    }
}
