using Octopus.Core.Domain.Exceptions;

namespace Octopus.Core.Domain.ValueObjects;

public class PaymentId : IdBase<Guid>
{
    private PaymentId(Guid id)
    {
        Value = id;
    }

    public static PaymentId Create(Guid id) => new(id);

    public static PaymentId Create(string value)
    {
        if (Guid.TryParse(value, out Guid id))
            return new(id);

        throw new InvalidArgumentToCreateValueObjectException<PaymentId, Guid>(value);
    }

    public static PaymentId New() => new(Guid.NewGuid());

    public override string ToString() => Value.ToString("N");
}
