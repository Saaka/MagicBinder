using MagicBinder.Domain.Aggregates;
using MongoDB.Driver;

namespace MagicBinder.Infrastructure.Repositories;

public class MongoIndexInitializer
{
    public static void CreateIndexes(IMongoDatabase mongoDatabase)
    {
        var cards = mongoDatabase.GetCollection<Card>("Cards");

        var indexBuilder = Builders<Card>.IndexKeys;

        var ascIndex = indexBuilder.Ascending(x => x.Name);
        var descIndex = indexBuilder.Descending(x => x.Name);

        var indexModels = new CreateIndexModel<Card>[]
        {
            new CreateIndexModel<Card>(ascIndex, new CreateIndexOptions { Background = true }),
            new CreateIndexModel<Card>(descIndex, new CreateIndexOptions { Background = true })
        };

        cards.Indexes.CreateMany(indexModels);
    }
}