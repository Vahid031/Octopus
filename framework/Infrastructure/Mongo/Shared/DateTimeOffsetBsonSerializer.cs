using MongoDB.Bson;
using MongoDB.Bson.Serialization;

namespace Octopus.Infrastructure.Mongo.Shared;

internal class DateTimeOffsetBsonSerializer : IBsonSerializer<DateTimeOffset>
{
    public DateTimeOffsetBsonSerializer()
    {
        ValueType = typeof(DateTimeOffset);
    }

    object IBsonSerializer.Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
    {
        return Deserialize(context, args);
    }

    public void Serialize(BsonSerializationContext context, BsonSerializationArgs args, DateTimeOffset value)
    {
        context.Writer.WriteDateTime(new BsonDateTime(value.DateTime).MillisecondsSinceEpoch);
    }

    public DateTimeOffset Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
    {
        var value = context.Reader.ReadDateTime();
        return new DateTimeOffset((DateTime)new BsonDateTime(value));
    }

    public void Serialize(BsonSerializationContext context, BsonSerializationArgs args, object value)
    {
        Serialize(context, args, (DateTimeOffset)value);
    }

    public Type ValueType { get; }
}