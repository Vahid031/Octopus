namespace Octopus.Core.Contract.Queries;

public abstract record PaginationFilterBase : IPaginationFilter
{
    protected PaginationFilterBase(int? pageNumber = 1, int? pageSize = 10)
    {
        PageSize = pageSize ?? 1;
        PageNumber = pageNumber ?? 10;
    }

    private readonly int _pageNumber;

    public int PageNumber
    {
        get => _pageNumber;
        init => _pageNumber = value < 1 ? 1 : value;
    }

    private readonly int _pageSize;

    public int PageSize
    {
        get => _pageSize;
        init => _pageSize = value is < 1 or > 100 ? 10 : value;
    }

    public int Skip => (PageNumber - 1) * PageSize;
}
