using MagicBinder.Domain.Aggregates;
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

    public virtual async Task UpdateAsync(Card card) => await Cards.ReplaceOneAsync(x => x.CardId == card.CardId, card);

    public virtual async Task UpsertManyAsync(ICollection<Card> cards)
    {
        var models = new List<WriteModel<Card>>();
        foreach (var card in cards)
        {
            WriteModel<Card> model = new ReplaceOneModel<Card>(Builders<Card>.Filter.Where(x => x.CardId == card.CardId), card) { IsUpsert = true };
            models.Add(model);
        }

        await Cards.BulkWriteAsync(models);
    }

    public virtual async Task<Card?> GetAsync(Guid id) => await Cards.AsQueryable().FirstOrDefaultAsync(x => x.CardId == id);

    public IMongoCollection<Card> Cards => _database.GetCollection<Card>("Cards");
}