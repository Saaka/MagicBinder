using MagicBinder.Domain.Aggregates;
using MongoDB.Driver;

namespace MagicBinder.Infrastructure.Repositories;

public class MongoIndexInitializer
{
    public static async Task CreateIndexes(IMongoDatabase mongoDatabase)
    {
        var cards = mongoDatabase.GetCollection<Card>("Cards");

        var indexBuilder = Builders<Card>.IndexKeys;

        var ascIndex = indexBuilder.Ascending(x => x.Name);
        var descIndex = indexBuilder.Descending(x => x.Name);

        var indexModels = new CreateIndexModel<Card>[]
        {
            new CreateIndexModel<Card>(ascIndex, new CreateIndexOptions { Background = true, Name = "Cards_Name_Asc"}),
            new CreateIndexModel<Card>(descIndex, new CreateIndexOptions { Background = true, Name = "Cards_Name_Desc" })
        };

        await cards.Indexes.CreateManyAsync(indexModels);
    }
}