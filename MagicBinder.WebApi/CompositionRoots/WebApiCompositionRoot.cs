using Autofac;
using MagicBinder.WebApi.Services;

namespace MagicBinder.WebApi.CompositionRoots;

public class WebApiCompositionRoot : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<MediatorCommandSender>()
            .AsSelf()
            .InstancePerLifetimeScope();
    }
}