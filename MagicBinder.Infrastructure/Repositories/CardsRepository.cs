using System.Text.RegularExpressions;
using MagicBinder.Core.Models;
using MagicBinder.Domain.Aggregates;
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

    public async Task<PagedList<Card>> GetCardsListAsync(string search, IPageableRequest request)
    {
        var builder = Builders<Card>.Filter;
        var nameFilter = builder.Regex(x => x.Name, BsonRegularExpression.Create(new Regex(search, RegexOptions.IgnoreCase)));
        var typeFilter = builder.Regex(x => x.TypeLine, BsonRegularExpression.Create(new Regex(search, RegexOptions.IgnoreCase)));
        var filter = builder.Or(nameFilter, typeFilter);
        filter &= builder.Where(x => x.Games.Any(g => g == "paper"));
        
        var query = Cards.AsQueryable().OrderBy(x => x.Name).Where(x => filter.Inject());

        return await query.ToPagedListAsync(request);
    }
}