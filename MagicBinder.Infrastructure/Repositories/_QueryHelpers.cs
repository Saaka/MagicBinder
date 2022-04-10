using MagicBinder.Core.Models;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace MagicBinder.Infrastructure.Repositories;

public static class QueryHelpers
{
    public static async Task<PagedList<T>> ToPagedListAsync<T>(this IMongoQueryable<T> query, int pageNumber, int pageSize)
    {
        var totalItemsCount = await query.CountAsync();
        var pagedQuery = query.Skip((pageNumber - 1) * pageSize).Take(pageSize);

        var items = await pagedQuery.ToListAsync();

        return new PagedList<T>(items, pageNumber, pageSize, totalItemsCount);
    }
}