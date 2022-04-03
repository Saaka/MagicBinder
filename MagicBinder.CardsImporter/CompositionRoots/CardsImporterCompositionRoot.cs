using Autofac;
using MagicBinder.CardsImporter.Services;
using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Extensions.Autofac.DependencyInjection;

namespace MagicBinder.CardsImporter.CompositionRoots;

public class CardsImporterCompositionRoot : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        var environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
        
        IConfiguration config = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .AddJsonFile($"appsettings.{environmentName}.json", true)
            .AddEnvironmentVariables()
            .Build();

        builder.RegisterInstance(config)
            .As<IConfiguration>();

        builder.RegisterType<CardsJsonService>()
            .AsSelf()
            .InstancePerLifetimeScope();

        var logConfig = new LoggerConfiguration()
            .WriteTo.Console();
        builder.RegisterSerilog(logConfig);
    }
}