namespace Octopus.Presentation.Http.EnvelopModels;

public record FailureEnvelop : Envelop
{
    public FailureEnvelop(EnvelopError envelopError) : this(new[] { envelopError })
    {

    }

    public FailureEnvelop(IEnumerable<EnvelopError> envelopErrors)
    {
        Errors = envelopErrors.ToArray().AsReadOnly();
    }

    [System.Text.Json.Serialization.JsonPropertyOrder(-1)]
    public override bool IsSuccess => false;

    public IReadOnlyList<EnvelopError> Errors { get; }
}

public sealed record FailureEnvelop<T> : FailureEnvelop
{
    public FailureEnvelop(EnvelopError[] errors, T data) : base(errors)
    {
        Data = data ?? throw new ArgumentNullException(nameof(data));
    }

    public T Data { get; }
}
