namespace Octopus.Core.Contract.Queries;

public interface IPaginationFilter
{
    public int PageNumber { get; }
    public int PageSize { get; }
    public int Skip { get; }
}