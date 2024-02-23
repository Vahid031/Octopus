namespace Octopus.Infrastructure.Mongo.Configurations;

public record MongoOptions
{
    public string ConnectionString { get; init; }
    public string DatabaseName { get; init; }
}
