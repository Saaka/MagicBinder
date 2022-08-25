using MagicBinder.Core.Models;
using MagicBinder.Domain.Aggregates;
using MagicBinder.Domain.Aggregates.Entities;
using MagicBinder.Infrastructure.Repositories.Models;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace MagicBinder.Infrastructure.Repositories;

public class InventoriesRepository : MongoRepository<Inventory>
{
    public InventoriesRepository(IMongoDatabase database) : base(database)
    {
    }

    public virtual async Task<PagedList<UserInventoryQueryResult>> GetUserInventoryAsync(UserInventoryQueryParams queryParams, CancellationToken cancellationToken = default)
    {
        var inventoryBuilder = Builders<Inventory>.Filter;
        var filter = inventoryBuilder.Where(x => x.Key.UserId == queryParams.UserId);

        if (!string.IsNullOrWhiteSpace(queryParams.CardName))
            filter = filter.ApplyRegexFilter(x => x.CardName, queryParams.CardName);

        var inventoryFilter = Builders<InventoryPrinting>.Filter.Empty;
        if (!string.IsNullOrWhiteSpace(queryParams.SetName))
            inventoryFilter = inventoryFilter.ApplyRegexFilter(p => p.SetName, queryParams.SetName);

        var query = Collection.AsQueryable()
            .Where(x => filter.Inject() && inventoryFilter.Inject())
            .SelectMany(i => i.Printings, (i, p) => new UserInventoryQueryResult
            {
                CardId = p.CardId,
                OracleId = i.Key.OracleId,
                CardName = i.CardName,
                Count = p.Count,
                Image = p.Image,
                Set = p.Set,
                SetName = p.SetName,
                IsFoil = p.IsFoil
            }).OrderBy(x=> x.CardName);

        return await query.ToPagedListAsync(queryParams, cancellationToken);
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