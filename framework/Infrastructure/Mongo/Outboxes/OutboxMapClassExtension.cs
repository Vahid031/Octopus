using MongoDB.Bson.Serialization;
using Octopus.Core.Contract.Events;
using Octopus.Core.Contract.Outboxes;

namespace Octopus.Infrastructure.Mongo.Outboxes;

internal class OutboxMapClassExtension
{
    internal static void Register()
    {
        BsonClassMap.RegisterClassMap<Outbox>(cm =>
        {
            cm.MapMember(m => m.Id).SetElementName("_id");
            cm.MapMember(m => m.IsProcessed).SetElementName("IsProcessed");
            cm.MapMember(m => m.AccuredOn).SetElementName("AccuredOn");
            cm.MapMember(m => m.Event).SetElementName("Event");
            cm.MapMember(m => m.TryCount).SetElementName("TryCount");

            cm.SetIgnoreExtraElements(true);
        });

        BsonClassMap.RegisterClassMap<IntegrationEvent>(cm =>
        {
            cm.MapMember(m => m.EventId).SetElementName("EventId");
            cm.MapMember(m => m.Sender).SetElementName("Sender");
            cm.MapMember(m => m.AccuredBy).SetElementName("AccuredBy");

            cm.SetIgnoreExtraElements(true);
            cm.SetIsRootClass(true);
            cm.AddKnownType(typeof(SendSmsIntegrationEvent));
        });

        BsonClassMap.RegisterClassMap<SendSmsIntegrationEvent>(cm =>
        {
            cm.MapMember(m => m.Message).SetElementName("Message");
            cm.MapMember(m => m.PhoneNumber).SetElementName("PhoneNumber");

            cm.SetIgnoreExtraElements(true);
        });
    }
}