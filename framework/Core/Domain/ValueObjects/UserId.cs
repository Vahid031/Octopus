using Octopus.Core.Domain.Exceptions;

namespace Octopus.Core.Domain.ValueObjects;

public class UserId : IdBase<Guid>
{
    private UserId(Guid id)
    {
        Value = id;
    }

    public static UserId Create(Guid id) => new(id);

    public static UserId Create(string value)
    {
        if (Guid.TryParse(value, out Guid id))
            return new(id);

        throw new InvalidArgumentToCreateValueObjectException<UserId, Guid>(value);
    }

    public static UserId New() => new(Guid.NewGuid());

    public override string ToString() => Value.ToString("N");
}
