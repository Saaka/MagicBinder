using MagicBinder.Domain.Aggregates;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace MagicBinder.Infrastructure.Repositories;

public class UsersRepository : IMongoRepository
{
    private readonly IMongoDatabase _database;

    public UsersRepository(IMongoDatabase database)
    {
        _database = database;
    }

    public virtual async Task InsertAsync(User user) => await Users.InsertOneAsync(user);

    public virtual async Task UpdateAsync(User user) => await Users.ReplaceOneAsync(x => x.UserGuid == user.UserGuid, user);

    public virtual async Task UpsertAsync(User user) => await Users.ReplaceOneAsync(x => x.UserGuid == user.UserGuid, user, new ReplaceOptions { IsUpsert = true });

    public virtual async Task<User?> GetAsync(Guid id, CancellationToken cancellationToken = default) 
        => await Users.AsQueryable().FirstOrDefaultAsync(x => x.UserGuid == id, cancellationToken);

    public IMongoCollection<User> Users => _database.GetCollection<User>("Users");
}