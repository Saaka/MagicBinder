using MagicBinder.Domain.Aggregates;
using MongoDB.Driver;

namespace MagicBinder.Infrastructure.Repositories;

public class InventoryRepository : IMongoRepository
{
    private readonly IMongoDatabase _database;

    public InventoryRepository(IMongoDatabase database)
    {
        _database = database;
    }

    public virtual async Task UpsertAsync(Inventory inventory, CancellationToken cancellationToken = default)
        => await Inventory
            .ReplaceOneAsync(x => x.Key.UserId == inventory.Key.UserId && x.Key.OracleId == inventory.Key.OracleId,
                inventory,
                new ReplaceOptions { IsUpsert = true },
                cancellationToken);

    private IMongoCollection<Inventory> Inventory => _database.GetCollection<Inventory>("Inventory");
}