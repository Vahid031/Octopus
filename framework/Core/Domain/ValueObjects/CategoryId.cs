using Octopus.Core.Domain.Exceptions;

namespace Octopus.Core.Domain.ValueObjects;

public class CategoryId : IdBase<Guid>
{
    private CategoryId(Guid id)
    {
        Value = id;
    }

    public static CategoryId Create(Guid id) => new(id);

    public static CategoryId Create(string value)
    {
        if (Guid.TryParse(value, out Guid id))
            return new(id);

        throw new InvalidArgumentToCreateValueObjectException<CategoryId, Guid>(value);
    }

    public static CategoryId New() => new(Guid.NewGuid());

    public override string ToString() => Value.ToString("N");
}

