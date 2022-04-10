namespace MagicBinder.Core.Models;

public class PaginationOptions
{
    public PaginationOptions(int pageNumber, int pageSize, int totalItemsCount)
    {
        PageNumber = pageNumber;
        PageSize = pageSize;
        TotalItemsCount = totalItemsCount;
        TotalPages = (int) Math.Ceiling((decimal) TotalItemsCount / PageSize);
        HasNextPage = PageNumber < TotalPages;
        HasPreviousPage = PageNumber > 1;
    }
        
    public int PageNumber { get; }
    public int PageSize { get; }
    public int TotalItemsCount { get; }
    public int TotalPages { get; }
    public bool HasNextPage { get; }
    public bool HasPreviousPage { get; }
}