using Autofac;
using MagicBinder.CardsImporter.Services;
using Microsoft.Extensions.Configuration;

namespace MagicBinder.CardsImporter.CompositionRoots;

public class CardsImporterCompositionRoot : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        IConfiguration config = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .AddEnvironmentVariables()
            .Build();

        builder.RegisterInstance(config)
            .As<IConfiguration>();

        builder.RegisterType<CardsJsonService>()
            .AsSelf()
            .InstancePerLifetimeScope();
    }
}