namespace Octopus.Core.Application.Outboxes;

internal record ProcessEventResult
{
    public Guid EventId { get; private init; }
    public bool IsSuccess { get; private set; }

    internal ProcessEventResult(Guid eventId)
    {
        EventId = eventId;
        IsSuccess = true;
    }

    internal void Fail() => IsSuccess = false;
}