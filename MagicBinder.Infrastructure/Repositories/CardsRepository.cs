using MagicBinder.Domain.Aggregates;
using MongoDB.Driver;

namespace MagicBinder.Infrastructure.Repositories;

public class CardsRepository
{
    private readonly IMongoDatabase _database;

    public CardsRepository(IMongoDatabase database)
    {
        _database = database;
    }

    public virtual async Task Insert(Card value) =>
        await Cards.InsertOneAsync(value);

    public IMongoCollection<Card> Cards => _database.GetCollection<Card>("Cards");
}