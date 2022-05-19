using MagicBinder.Core.Models;
using MagicBinder.Domain.Aggregates;
using MagicBinder.Infrastructure.Repositories.Models;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace MagicBinder.Infrastructure.Repositories;

public class DecksRepository : MongoRepository<Deck>
{
    public DecksRepository(IMongoDatabase database) : base(database)
    {
    }

    public virtual async Task<PagedList<Deck>> GetAsync(UserDecksQueryParams queryParams, CancellationToken cancellationToken = default)
    {
        var builder = Builders<Deck>.Filter;

        var filter = builder.Where(x => x.OwnerId == queryParams.OwnerId);

        if (!string.IsNullOrWhiteSpace(queryParams.Name))
            filter = filter.ApplyRegexFilter(x => x.Name, queryParams.Name);

        var query = Collection.AsQueryable().OrderBy(x => x.Name).Where(x => filter.Inject());

        return await query.ToPagedListAsync(queryParams, cancellationToken);
    }

    public async Task InsertAsync(Deck deck, CancellationToken cancellationToken = default) => await Collection.InsertOneAsync(deck, null, cancellationToken);

    public async Task<bool> IsNameInUse(string name, Guid ownerId, CancellationToken cancellationToken = default) =>
        await Collection.AsQueryable().AnyAsync(x => x.Name == name && x.OwnerId == ownerId, cancellationToken);

    protected override string CollectionName => "Decks";
}