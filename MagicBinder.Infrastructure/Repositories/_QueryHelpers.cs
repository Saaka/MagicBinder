using MagicBinder.Core.Models;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace MagicBinder.Infrastructure.Repositories;

public static class QueryHelpers
{
    public static async Task<PagedList<T>> ToPagedListAsync<T>(this IMongoQueryable<T> dbQuery, IPageableRequest request)
    {
        var totalItemsCount = await dbQuery.CountAsync();
        var pagedDbQuery = dbQuery.Skip((request.PageNumber - 1) * request.PageSize).Take(request.PageSize);

        var items = await pagedDbQuery.ToListAsync();

        return new PagedList<T>(items, request.PageNumber, request.PageSize, totalItemsCount);
    }
}