using System.Linq.Expressions;
using System.Text.RegularExpressions;
using MagicBinder.Core.Models;
using MagicBinder.Domain.Aggregates;
using MagicBinder.Domain.Enums;
using MagicBinder.Infrastructure.Repositories.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace MagicBinder.Infrastructure.Repositories;

public class CardsRepository : IMongoRepository
{
    private readonly IMongoDatabase _database;

    public CardsRepository(IMongoDatabase database)
    {
        _database = database;
    }

    public virtual async Task InsertAsync(Card card) => await Cards.InsertOneAsync(card);

    public virtual async Task UpdateAsync(Card card) => await Cards.ReplaceOneAsync(x => x.OracleId == card.OracleId, card);

    public virtual async Task UpsertManyAsync(ICollection<Card> cards)
    {
        var models = new List<WriteModel<Card>>();
        foreach (var card in cards)
        {
            WriteModel<Card> model = new ReplaceOneModel<Card>(Builders<Card>.Filter.Where(x => x.OracleId == card.OracleId), card) { IsUpsert = true };
            models.Add(model);
        }

        await Cards.BulkWriteAsync(models);
    }

    public virtual async Task<Card?> GetAsync(Guid id) => await Cards.AsQueryable().FirstOrDefaultAsync(x => x.OracleId == id);

    public IMongoCollection<Card> Cards => _database.GetCollection<Card>("Cards");

    public async Task<PagedList<Card>> GetCardsListAsync(CardsListQueryParams queryParams)
    {
        var builder = Builders<Card>.Filter;
        var gameFilter = builder.Where(x => x.Games.Contains(GameType.Paper));
        var filter = gameFilter;
        filter = ApplyRegexFilter(filter, x => x.Name, queryParams.Name);
        filter = ApplyRegexFilter(filter, x => x.LatestPrinting.TypeLine, queryParams.TypeLine);

        if (!string.IsNullOrEmpty(queryParams.OracleText))
        {
            var regex = new Regex(queryParams.OracleText, RegexOptions.IgnoreCase);
            var oracleTextFilter = builder.Where(x => x.CardPrintings.Any(p => regex.IsMatch(p.OracleText)));
            filter = builder.And(filter, oracleTextFilter);
        }

        var query = Cards.AsQueryable().OrderBy(x => x.Name).Where(x => filter.Inject());

        return await query.ToPagedListAsync(queryParams);
    }

    private static FilterDefinition<Card> ApplyRegexFilter(FilterDefinition<Card> filter, Expression<Func<Card, object>> property, string? value)
    {
        var builder = Builders<Card>.Filter;
        if (string.IsNullOrEmpty(value)) return filter;

        var regex = new Regex(value, RegexOptions.IgnoreCase);
        var newFilter = builder.Regex(property, BsonRegularExpression.Create(regex));
        return builder.And(filter, newFilter);
    }
}