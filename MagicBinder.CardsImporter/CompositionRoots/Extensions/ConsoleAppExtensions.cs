using Autofac;
using Hangfire;
using Hangfire.Mongo;
using Hangfire.Mongo.Migration.Strategies;
using MagicBinder.Core.CompositionRoots;
using MagicBinder.Infrastructure.Configurations;
using Microsoft.Extensions.Configuration;

namespace MagicBinder.CardsImporter.CompositionRoots.Extensions;

public static class ConsoleAppExtensions
{
    public static ILifetimeScope AddHangfire(this ILifetimeScope lifetimeScope)
    {
        GlobalConfiguration.Configuration.UseAutofacActivator(lifetimeScope);
        var config = lifetimeScope.Resolve<IConfiguration>();
        var mongoConfig = config.GetOptions<MongoConfig>();
        var hangfireConfig = config.GetOptions<HangfireConfig>();
        
        GlobalConfiguration.Configuration.UseMongoStorage(mongoConfig.ConnectionString, hangfireConfig.DatabaseName, new MongoStorageOptions
        {
            MigrationOptions = new MongoMigrationOptions
            {
                MigrationStrategy = new MigrateMongoMigrationStrategy()
            }
        });
        
        return lifetimeScope;
    }
}