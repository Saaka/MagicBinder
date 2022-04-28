using MagicBinder.Domain.Aggregates;
using MongoDB.Driver;

namespace MagicBinder.Infrastructure.Repositories;

public class InventoriesRepository : IMongoRepository
{
    private readonly IMongoDatabase _database;

    public InventoriesRepository(IMongoDatabase database)
    {
        _database = database;
    }

    public virtual async Task UpsertAsync(Inventory inventory, CancellationToken cancellationToken = default)
        => await Inventories
            .ReplaceOneAsync(x => x.Key.UserId == inventory.Key.UserId && x.Key.OracleId == inventory.Key.OracleId,
                inventory,
                new ReplaceOptions { IsUpsert = true },
                cancellationToken);

    private IMongoCollection<Inventory> Inventories => _database.GetCollection<Inventory>("Inventories");
}