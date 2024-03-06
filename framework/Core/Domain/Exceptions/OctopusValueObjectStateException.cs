namespace Octopus.Core.Domain.Exceptions;

public class OctopusValueObjectStateException : OctopusDomainException
{
    public OctopusValueObjectStateException(string message, params string[] parameters) : base(message)
    {
        Parameters = parameters;
    }
}