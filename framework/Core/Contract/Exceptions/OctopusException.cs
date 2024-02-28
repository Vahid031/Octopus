namespace Octopus.Core.Contract.Exceptions;

public class OctopusException : Exception
{
    protected string[] Parameters { get; set; }
    protected OctopusException(string message, params string[] parameters)
        : base(message)
    {
        Parameters = parameters;
    }
}