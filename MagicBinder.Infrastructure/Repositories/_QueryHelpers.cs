using System.Linq.Expressions;
using System.Text.RegularExpressions;
using MagicBinder.Core.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace MagicBinder.Infrastructure.Repositories;

public static class QueryHelpers
{
    public static async Task<PagedList<T>> ToPagedListAsync<T>(this IMongoQueryable<T> dbQuery, IPageableRequest request, CancellationToken cancellationToken = default)
    {
        var totalItemsCount = await dbQuery.CountAsync(cancellationToken);
        var pagedDbQuery = dbQuery.Skip((request.PageNumber - 1) * request.PageSize).Take(request.PageSize);

        var items = await pagedDbQuery.ToListAsync(cancellationToken);

        return new PagedList<T>(items, request.PageNumber, request.PageSize, totalItemsCount);
    }

    public static FilterDefinition<T> ApplyRegexFilter<T>(this FilterDefinition<T> filter, Expression<Func<T, object>> property, string? value)
    {
        var builder = Builders<T>.Filter;
        if (string.IsNullOrEmpty(value)) return filter;

        var regex = new Regex(value.Trim(), RegexOptions.IgnoreCase);
        var newFilter = builder.Regex(property, BsonRegularExpression.Create(regex));
        return builder.And(filter, newFilter);
    }
}