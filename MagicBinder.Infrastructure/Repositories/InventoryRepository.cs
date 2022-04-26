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

    public virtual async Task UpsertAsync(Inventory inventory) => await Inventory
        .ReplaceOneAsync(x => x.UserGuid == inventory.UserGuid && x.OracleId == inventory.OracleId, inventory, new ReplaceOptions { IsUpsert = true });
    
    public IMongoCollection<Inventory> Inventory => _database.GetCollection<Inventory>("Inventory");
}