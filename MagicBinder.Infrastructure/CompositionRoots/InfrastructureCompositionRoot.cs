using Autofac;
using MagicBinder.Core.CompositionRoots;
using MagicBinder.Infrastructure.Configurations;
using MagicBinder.Infrastructure.Integrations.IdentityIssuer;
using MagicBinder.Infrastructure.Integrations.Scryfall;
using MagicBinder.Infrastructure.Repositories;
using MagicBinder.Infrastructure.Repositories.Mappings;
using MongoDB.Driver;

namespace MagicBinder.Infrastructure.CompositionRoots;

public class InfrastructureCompositionRoot : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        RegisterMongo(builder);
        RegisterScryfall(builder);
        RegisterIdentityIssuer(builder);
    }

    private static void RegisterScryfall(ContainerBuilder builder)
    {
        builder.RegisterType<JsonCardsParser>()
            .AsSelf()
            .InstancePerLifetimeScope();

        builder.RegisterType<JsonSetsParser>()
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

            var database = client.GetDatabase(config.Database);

            return database;
        }).As<IMongoDatabase>();

        var assembly = typeof(InfrastructureCompositionRoot)
            .Assembly;

        builder.RegisterAssemblyTypes(assembly)
            .Where(x => x.IsAssignableTo<IMongoRepository>() && !x.IsAbstract)
            .AsImplementedInterfaces()
            .AsSelf()
            .InstancePerLifetimeScope();
        
        AggregateMappings.RegisterClassMaps();
    }

    private static void RegisterIdentityIssuer(ContainerBuilder builder)
    {
        builder.RegisterType<IdentityIssuerClient>()
            .AsSelf()
            .SingleInstance();
    }
}