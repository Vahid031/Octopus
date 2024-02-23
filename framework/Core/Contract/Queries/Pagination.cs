namespace Octopus.Core.Contract.Queries;

public record Pagination<TResponse>
{
    private Pagination(int pageNumber, int pageSize)
    {
        TotalCount = 0;
        PageNumber = pageNumber;
        PageSize = pageSize;
        Items = new List<TResponse>(0).AsReadOnly();
    }

    public Pagination(long totalCount, int pageNumber, int pageSize, IEnumerable<TResponse> items)
    {
        TotalCount = totalCount;
        PageNumber = pageNumber;
        PageSize = pageSize;
        Items = items.ToList().AsReadOnly();
    }

    public static Pagination<TResponse> Empty(int pageNumber, int pageSize)
    {
        return new Pagination<TResponse>(pageSize, pageNumber);
    }

    public long  TotalCount { get; }

    public int PageNumber { get; }

    public int PageSize { get; }

    public IReadOnlyList<TResponse> Items { get; }

    public bool HasItem => Items.Count > 0;
}
