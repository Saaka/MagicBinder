using Autofac;
using MagicBinder.Core.CompositionRoots;
using MagicBinder.WebApi.CompositionRoots.Configurations;
using MagicBinder.WebApi.Services;

namespace MagicBinder.WebApi.CompositionRoots;

public class WebApiCompositionRoot : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder
            .RegisterConfigurationsForAssemblyOfType<SwaggerConfig>();
        
        builder.RegisterType<MediatorCommandSender>()
            .AsSelf()
            .InstancePerLifetimeScope();
    }
}