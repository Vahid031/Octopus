using Octopus.Core.Domain.ValueObjects;

namespace Octopus.Core.Contract.Events;

public class SendSmsIntegrationEvent : IntegrationEvent
{
    public string Message { get; }
    public string PhoneNumber { get; }

    public SendSmsIntegrationEvent(string sender, UserId userid, string message, string phoneNumber)
        : base(Guid.NewGuid(), sender, userid)
    {
        Message = message;
        PhoneNumber = phoneNumber;
    }
}
