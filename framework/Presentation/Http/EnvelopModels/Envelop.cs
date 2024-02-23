namespace Octopus.Presentation.Http.EnvelopModels;

public abstract record Envelop
{
    public abstract bool IsSuccess { get; }
}
