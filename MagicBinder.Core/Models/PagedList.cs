using MagicBinder.Core.Exceptions;

namespace MagicBinder.Core.Models;

public class PagedList<T>
{
    public PagedList(IList<T> items, int pageNumber, int pageSize, int totalItemsCount)
    {
        Validate(items, pageNumber, pageSize, totalItemsCount);
        Items = items?.ToList() ?? new List<T>();
        Options = new PaginationOptions(pageNumber, pageSize, totalItemsCount);
    }

    private void Validate(IList<T> items, int pageNumber, int pageSize, int totalItemsCount)
    {
        if (pageNumber < 1)
            throw new InvalidPagedRequestException(nameof(pageNumber));
        if (pageSize < 1)
            throw new InvalidPagedRequestException(nameof(pageSize));
        if (totalItemsCount < 0 || totalItemsCount < items.Count())
            throw new Exception($"Invalid {nameof(totalItemsCount)} value.");
    }

    public IReadOnlyCollection<T> Items { get; }
    public PaginationOptions Options { get; }
}