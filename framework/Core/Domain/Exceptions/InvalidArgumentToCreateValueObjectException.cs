using Octopus.Core.Domain.ValueObjects;

namespace Octopus.Core.Domain.Exceptions;

public class InvalidArgumentToCreateValueObjectException<TValueObject, TType> : OctopusDomainException
    where TValueObject : ValueObject<IdBase<TType>>
    where TType : notnull
{
    public InvalidArgumentToCreateValueObjectException(string value)
        : base("Input value to create '{0}' is not guid type, value: '{2}'", 
            nameof(TValueObject), nameof(TType), value)
    {
    }
}
