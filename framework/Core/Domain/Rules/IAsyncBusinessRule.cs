namespace Octopus.Core.Domain.Rules;

public interface IAsyncBusinessRule
{
    Task Validate();
}