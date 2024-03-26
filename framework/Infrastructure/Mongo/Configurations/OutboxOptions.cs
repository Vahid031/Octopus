namespace Octopus.Infrastructure.Mongo.Configurations;

public record OutboxOptions
{
    public int MaxRetryCount { get; init; }
    public int ProcessSize { get; init; }
}
