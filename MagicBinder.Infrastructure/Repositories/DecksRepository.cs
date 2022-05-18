using MagicBinder.Domain.Aggregates;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace MagicBinder.Infrastructure.Repositories;

public class DecksRepository : MongoRepository<Deck>
{
    public DecksRepository(IMongoDatabase database) : base(database)
    {
    }

    public async Task InsertAsync(Deck deck, CancellationToken cancellationToken = default) => await Collection.InsertOneAsync(deck, null, cancellationToken);

    public async Task<bool> IsNameInUse(string name, Guid ownerId, CancellationToken cancellationToken = default) =>
        await Collection.AsQueryable().AnyAsync(x => x.Name == name && x.OwnerId == ownerId, cancellationToken);

    protected override string CollectionName => "Decks";
}