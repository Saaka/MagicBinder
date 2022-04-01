using Autofac;
using MagicBinder.Core.CompositionRoots;
using MagicBinder.Infrastructure.Configurations;

namespace MagicBinder.Infrastructure.CompositionRoots;

public class InfrastructureCompositionRoot : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder
            .RegisterConfigurationsForAssemblyOfType<MongoConfig>();
    }
}