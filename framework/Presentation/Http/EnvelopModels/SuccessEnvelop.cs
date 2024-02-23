namespace Octopus.Presentation.Http.EnvelopModels;

public record SuccessEnvelop<TData> : Envelop
{
    protected SuccessEnvelop(TData data)
    {
        Data = data;
    }

    [System.Text.Json.Serialization.JsonPropertyOrder(-1)]
    public override bool IsSuccess => true;

    public TData Data { get; }
}

public record SuccessEnvelop : SuccessEnvelop<object>
{
    public SuccessEnvelop(object data) : base(data)
    {
    }
}

