namespace Octopus.Core.Domain.ValueObjects;

public class BrandId : IdBase<Guid>
{
    private BrandId(Guid id)
    {
        Value = id;
    }

    public static BrandId Create(Guid id) => new(id);

    public static BrandId Create(string value)
    {
        if (Guid.TryParse(value, out Guid id))
            return new(id);

        throw new ArgumentException();
    }

    public static BrandId New() => new(Guid.NewGuid());

    public override string ToString() => Value.ToString("N");
}
