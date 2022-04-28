using MagicBinder.Domain.Aggregates;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace MagicBinder.Infrastructure.Repositories;

public class SetsRepository : MongoRepository<Set>
{
    public SetsRepository(IMongoDatabase database) : base(database)
    {
    }

    public virtual async Task<ICollection<Set>> GetSets(CancellationToken cancellationToken = default)
    {
        return await Collection.AsQueryable().OrderByDescending(x => x.ReleasedAt).ToListAsync(cancellationToken);
    }

    public virtual async Task UpsertManyAsync(ICollection<Set> sets, CancellationToken cancellationToken = default)
    {
        var models = new List<WriteModel<Set>>();
        foreach (var set in sets)
        {
            WriteModel<Set> model = new ReplaceOneModel<Set>(Builders<Set>.Filter.Where(x => x.SetId == set.SetId), set) { IsUpsert = true };
            models.Add(model);
        }

        await Collection.BulkWriteAsync(models, null, cancellationToken);
    }

    protected override string CollectionName => "Sets";
}