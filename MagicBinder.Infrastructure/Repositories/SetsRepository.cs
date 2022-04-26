using MagicBinder.Domain.Aggregates;
using MongoDB.Driver;

namespace MagicBinder.Infrastructure.Repositories;

public class SetsRepository : MongoRepository<Set>
{
    public SetsRepository(IMongoDatabase database) : base(database)
    {
    }

    public virtual async Task UpsertManyAsync(ICollection<Set> sets)
    {
        var models = new List<WriteModel<Set>>();
        foreach (var set in sets)
        {
            WriteModel<Set> model = new ReplaceOneModel<Set>(Builders<Set>.Filter.Where(x => x.SetId == set.SetId), set) { IsUpsert = true };
            models.Add(model);
        }

        await Collection.BulkWriteAsync(models);
    }

    protected override string CollectionName => "Sets";
}