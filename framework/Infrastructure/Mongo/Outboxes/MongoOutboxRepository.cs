using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Octopus.Core.Contract.Events;
using Octopus.Core.Contract.Outboxes;
using Octopus.Core.Contract.Services;
using Octopus.Infrastructure.Mongo.Configurations;

namespace Octopus.Infrastructure.Mongo.Outboxes;

internal class MongoOutboxRepository(IMongoCollection<Outbox> collection, IOptions<OutboxOptions> options)
    : IOutboxRepository
{

    internal const string CollectionName = "Outboxes";

    public async Task Add<TEvent>(TEvent @event, CancellationToken cancellationToken) where TEvent : IntegrationEvent
    {
        var outbox = Outbox.Create(@event);

        await collection.InsertOneAsync(outbox);

    }

    public async Task<List<IntegrationEvent>> GetEvents(CancellationToken cancellationToken)
    {
        var filterDefinition = Builders<Outbox>.Filter.And(
            Builders<Outbox>.Filter.Eq(m => m.IsProcessed, false),
            Builders<Outbox>.Filter.Lt(m => m.TryCount, options.Value.MaxRetryCount));
        var sortDefinition = Builders<Outbox>.Sort.Ascending(m => m.AccuredOn);

        var events = await collection
            .Find(filterDefinition)
            .Limit(options.Value.ProcessSize)
            .ToListAsync(cancellationToken);

        filterDefinition = Builders<Outbox>.Filter.In(x => x.Id, events.Select(e => e.Id).ToList());
        var updateDefinition = Builders<Outbox>.Update.Inc(x => x.TryCount, 1);

        await collection.UpdateManyAsync(filterDefinition, updateDefinition);

        return events.Select(o => o.Event).ToList();
    }

    public async Task MarkAsProceed(List<Guid> ids, CancellationToken cancellationToken)
    {
        var updateDefinition = Builders<Outbox>.Update.Set(x => x.IsProcessed, true);
        var filterDefinition = Builders<Outbox>.Filter.In(x => x.Id, ids);

        var result = await collection.UpdateManyAsync(filterDefinition, updateDefinition);
    }
}
