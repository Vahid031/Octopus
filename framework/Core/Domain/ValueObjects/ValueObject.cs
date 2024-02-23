namespace Octopus.Core.Domain.ValueObjects;

public abstract class ValueObject<TValueObject> : IEquatable<TValueObject>
where TValueObject : ValueObject<TValueObject>
{
    public bool Equals(TValueObject other)
    {
        if (other == null)
            return false;
        return this == other;
    }
    public override bool Equals(object obj)
    {
        if (obj is TValueObject other)
        {
            if (ReferenceEquals(this, other))
                return true;
            return ObjectIsEqual(other);
        }
        return false;
    }
    public override int GetHashCode() => ObjectGetHashCode();
    public abstract bool ObjectIsEqual(TValueObject other);
    public abstract int ObjectGetHashCode();
    public static bool operator ==(ValueObject<TValueObject> right, ValueObject<TValueObject> left)
    {
        if (right is null && left is null)
            return true;
        if (right is null || left is null)
            return false;
        return right.Equals(left);
    }
    public static bool operator !=(ValueObject<TValueObject> right, ValueObject<TValueObject> left)
    {
        return !(right == left);
    }
}