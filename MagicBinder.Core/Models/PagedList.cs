using MagicBinder.Core.Exceptions;

namespace MagicBinder.Core.Models;

public record PagedList<T>
{
    public PagedList(IList<T> items, int pageNumber, int pageSize, int totalItemsCount)
    {
        Validate(items, pageNumber, pageSize, totalItemsCount);
        Items = items?.ToList() ?? new List<T>();
        Options = new PaginationOptions(pageNumber, pageSize, totalItemsCount);
    }

    private PagedList(IReadOnlyCollection<T> items, PaginationOptions options)
    {
        Items = items;
        Options = options;
    }

    public PagedList<TResult> MapToResponse<TResult>(Func<T, TResult> mapping) => new(Items.Select(mapping).ToList(), Options);

    private void Validate(ICollection<T> items, int pageNumber, int pageSize, int totalItemsCount)
    {
        if (pageNumber < 1)
            throw new InvalidPagedRequestException(nameof(pageNumber));
        if (pageSize < 1)
            throw new InvalidPagedRequestException(nameof(pageSize));
        if (totalItemsCount < 0 || totalItemsCount < items.Count())
            throw new Exception($"Invalid {nameof(totalItemsCount)} value.");
    }

    public IReadOnlyCollection<T> Items { get; init; }
    public PaginationOptions Options { get; init; }
}