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

    public virtual async Task UpsertManyAsync(ICollection<Card> cards, CancellationToken cancellationToken = default)
    {
        var models = new List<WriteModel<Card>>();
        foreach (var card in cards)
        {
            WriteModel<Card> model = new ReplaceOneModel<Card>(Builders<Card>.Filter.Where(x => x.OracleId == card.OracleId), card) { IsUpsert = true };
            models.Add(model);
        }

        await Cards.BulkWriteAsync(models, null, cancellationToken);
    }

    public virtual async Task<Card?> GetAsync(Guid oracleId, CancellationToken cancellationToken = default)
        => await Cards.AsQueryable().FirstOrDefaultAsync(x => x.OracleId == oracleId, cancellationToken);

    public async Task<PagedList<Card>> GetCardsListAsync(CardsListQueryParams queryParams, CancellationToken cancellationToken = default)
    {
        var builder = Builders<Card>.Filter;
        var gameFilter = builder.Where(x => x.Games.Contains(GameType.Paper));
        var filter = gameFilter;
        filter = filter.ApplyRegexFilter(x => x.Name, queryParams.Name);
        filter = filter.ApplyRegexFilter(x => x.LatestPrinting.TypeLine, queryParams.TypeLine);

        if (!string.IsNullOrEmpty(queryParams.OracleText))
        {
            var regex = new Regex(queryParams.OracleText.Trim(), RegexOptions.IgnoreCase);
            var oracleTextFilter = builder.Where(x => x.CardPrintings.Any(p => regex.IsMatch(p.OracleText)));
            filter = builder.And(filter, oracleTextFilter);
        }

        var query = Cards.AsQueryable().OrderBy(x => x.Name).Where(x => filter.Inject());

        return await query.ToPagedListAsync(queryParams, cancellationToken);
    }

    private IMongoCollection<Card> Cards => _database.GetCollection<Card>("Cards");
}