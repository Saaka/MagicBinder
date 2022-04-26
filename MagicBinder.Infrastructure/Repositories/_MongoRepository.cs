using MongoDB.Driver;

namespace MagicBinder.Infrastructure.Repositories;

public interface IMongoRepository
{
}

public abstract class MongoRepository<T> : IMongoRepository
{
    protected readonly IMongoDatabase Database;

    protected MongoRepository(IMongoDatabase database)
    {
        Database = database;
    }

    protected abstract string CollectionName { get; }
    protected IMongoCollection<T> Collection => Database.GetCollection<T>(CollectionName);
}