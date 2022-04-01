namespace MagicBinder.Infrastructure.Configurations;

public record MongoConfig
{
    public string ConnectionString { get; init; }
    public string Database { get; init; }
}