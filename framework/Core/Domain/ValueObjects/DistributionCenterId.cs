using Octopus.Core.Domain.Exceptions;

namespace Octopus.Core.Domain.ValueObjects;

public class DistributionCenterId : IdBase<Guid>
{
    private DistributionCenterId(Guid id)
    {
        Value = id;
    }

    public static DistributionCenterId Create(Guid id) => new(id);

    public static DistributionCenterId Create(string value)
    {
        if (Guid.TryParse(value, out Guid id))
            return new(id);

        throw new InvalidArgumentToCreateValueObjectException<DistributionCenterId, Guid>(value);
    }

    public static DistributionCenterId New() => new(Guid.NewGuid());

    public override string ToString() => Value.ToString("N");
}