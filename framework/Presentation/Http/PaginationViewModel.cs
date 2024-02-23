namespace Octopus.Presentation.Http;

public record PaginationViewModel<TViewModel>
{
    public PaginationViewModel()
    {
        Items = Array.Empty<TViewModel>();
    }

    [System.Text.Json.Serialization.JsonPropertyOrder(-3)]
    public int TotalCount { get; init; }

    [System.Text.Json.Serialization.JsonPropertyOrder(-2)]
    public int PageNumber { get; init; }

    [System.Text.Json.Serialization.JsonPropertyOrder(-1)]
    public int PageSize { get; init; }

    public TViewModel[] Items { get; init; }
}