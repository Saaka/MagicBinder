using MagicBinder.Domain.Aggregates;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace MagicBinder.Infrastructure.Repositories;

public class InventoriesRepository : MongoRepository<Inventory>
{
    public InventoriesRepository(IMongoDatabase database) : base(database)
    {
    }

    public virtual async Task<Inventory?> GetAsync(Guid oracleId, Guid userId, CancellationToken cancellationToken = default)
        => await Collection.AsQueryable().FirstOrDefaultAsync(x => x.Key.OracleId == oracleId && x.Key.UserId == userId, cancellationToken);

    public virtual async Task UpsertAsync(Inventory inventory, CancellationToken cancellationToken = default)
        => await Collection
            .ReplaceOneAsync(x => x.Key == inventory.Key,
                inventory,
                new ReplaceOptions { IsUpsert = true },
                cancellationToken);

    protected override string CollectionName => "Inventories";
}