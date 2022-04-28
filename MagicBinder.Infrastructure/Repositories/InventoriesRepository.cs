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
            .ReplaceOneAsync(x => x.Key == inventory.Key,
                inventory,
                new ReplaceOptions { IsUpsert = true },
                cancellationToken);

    private IMongoCollection<Inventory> Inventories => _database.GetCollection<Inventory>("Inventories");
}