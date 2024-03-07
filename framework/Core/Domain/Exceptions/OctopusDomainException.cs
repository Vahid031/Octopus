namespace Octopus.Core.Domain.Exceptions;

public abstract class OctopusDomainException : Exception
{
    protected string[] Parameters { get; set; }
    protected OctopusDomainException(string message, params string[] parameters)
    : base(message)
    {
        Parameters = parameters;
    }
}
