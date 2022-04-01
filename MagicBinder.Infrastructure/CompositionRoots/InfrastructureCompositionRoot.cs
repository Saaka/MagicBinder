using Autofac;
using MagicBinder.Core.CompositionRoots;
using MagicBinder.Infrastructure.Configurations;
using MagicBinder.Infrastructure.Integrations.Scryfall;
using MagicBinder.Infrastructure.Repositories;
using MongoDB.Driver;

namespace MagicBinder.Infrastructure.CompositionRoots;

public class InfrastructureCompositionRoot : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        RegisterMongo(builder);

        builder.RegisterType<JsonCardsParser>()
            .AsSelf()
            .InstancePerLifetimeScope();
    }

    private static void RegisterMongo(ContainerBuilder builder)
    {
        builder
            .RegisterConfigurationsForAssemblyOfType<MongoConfig>();

        builder.Register((cr, p) =>
        {
            var config = cr.Resolve<MongoConfig>();
            return new MongoClient(config.ConnectionString);
        }).SingleInstance();

        builder.Register((cr, p) =>
        {
            var client = cr.Resolve<MongoClient>();
            var config = cr.Resolve<MongoConfig>();

            return client.GetDatabase(config.Database);
        }).As<IMongoDatabase>();

        var assembly = typeof(InfrastructureCompositionRoot)
            .Assembly;

        builder.RegisterAssemblyTypes(assembly)
            .Where(x => x.IsAssignableTo<IMongoRepository>())
            .AsImplementedInterfaces()
            .AsSelf()
            .InstancePerLifetimeScope();
    }
}