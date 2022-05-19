using MagicBinder.Domain.Aggregates;
using MongoDB.Driver;

namespace MagicBinder.Infrastructure.Repositories;

public class MongoIndexInitializer
{
    public static async Task CreateIndexes(IMongoDatabase mongoDatabase)
    {
        await CreateCardsIndexes(mongoDatabase);
        await CreateDecksIndexes(mongoDatabase);
    }

    private static async Task CreateCardsIndexes(IMongoDatabase mongoDatabase)
    {
        var cards = mongoDatabase.GetCollection<Card>("Cards");

        var indexBuilder = Builders<Card>.IndexKeys;

        var ascIndex = indexBuilder.Ascending(x => x.Name);
        var descIndex = indexBuilder.Descending(x => x.Name);

        var indexModels = new CreateIndexModel<Card>[]
        {
            new(ascIndex, new CreateIndexOptions { Background = true, Name = "Cards_Name_Asc" }),
            new(descIndex, new CreateIndexOptions { Background = true, Name = "Cards_Name_Desc" })
        };

        await cards.Indexes.CreateManyAsync(indexModels);
    }

    private static async Task CreateDecksIndexes(IMongoDatabase mongoDatabase)
    {
        var decks = mongoDatabase.GetCollection<Deck>("Decks");

        var indexBuilder = Builders<Deck>.IndexKeys;

        var ascIndex = indexBuilder.Ascending(x => x.Name);
        var descIndex = indexBuilder.Descending(x => x.Name);

        var indexModels = new CreateIndexModel<Deck>[]
        {
            new(ascIndex, new CreateIndexOptions { Background = true, Name = "Decks_Name_Asc" }),
            new(descIndex, new CreateIndexOptions { Background = true, Name = "Decks_Name_Desc" })
        };

        await decks.Indexes.CreateManyAsync(indexModels);
    }
}