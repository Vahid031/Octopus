namespace Octopus.Core.Domain.Exceptions;

public abstract class DomainException : Exception
{
    protected string[] Parameters { get; set; }
    protected DomainException(string message, params string[] parameters)
    : base(message)
    {
        Parameters = parameters;
    }
}
