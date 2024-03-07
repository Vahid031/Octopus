namespace Octopus.Core.Contract.Exceptions;

public class OctopusException : Exception
{
    protected string[] Parameters { get; set; }
    public OctopusException(string message, params string[] parameters)
        : base(message)
    {
        Parameters = parameters;
    }
}