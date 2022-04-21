using Autofac;
using MagicBinder.Core.Requests;

namespace MagicBinder.Core.CompositionRoots;

public class CoreCompositionRoot :  Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<RequestContextService>()
            .AsImplementedInterfaces()
            .AsSelf()
            .InstancePerLifetimeScope();
    }
}