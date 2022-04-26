using MagicBinder.Domain.Aggregates;
using MongoDB.Driver;

namespace MagicBinder.Infrastructure.Repositories;

public class SetsRepository : IMongoRepository
{
    private readonly IMongoDatabase _database;

    public SetsRepository(IMongoDatabase database)
    {
        _database = database;
    }

    public virtual async Task UpsertManyAsync(ICollection<Set> sets)
    {
        var models = new List<WriteModel<Set>>();
        foreach (var set in sets)
        {
            WriteModel<Set> model = new ReplaceOneModel<Set>(Builders<Set>.Filter.Where(x => x.SetId == set.SetId), set) { IsUpsert = true };
            models.Add(model);
        }

        await Sets.BulkWriteAsync(models);
    }
    
    private IMongoCollection<Set> Sets => _database.GetCollection<Set>("Sets");
}