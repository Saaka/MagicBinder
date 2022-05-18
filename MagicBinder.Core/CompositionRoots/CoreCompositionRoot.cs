using Autofac;
using MagicBinder.Core.Requests;
using MagicBinder.Core.Requests.Behaviors;
using MagicBinder.Core.Services;
using MediatR;
using MediatR.Pipeline;

namespace MagicBinder.Core.CompositionRoots;

public class CoreCompositionRoot : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<RequestContextService>()
            .AsImplementedInterfaces()
            .AsSelf()
            .InstancePerLifetimeScope();
        
        builder.RegisterGeneric(typeof(RequestPreProcessorBehavior<,>)).AsImplementedInterfaces();
        builder.RegisterGeneric(typeof(RequestPostProcessorBehavior<,>)).AsImplementedInterfaces();
        builder.RegisterGeneric(typeof(CommandValidationBehavior<,>)).As(typeof(IPipelineBehavior<,>));

        builder.RegisterType<GuidService>().AsSelf();
    }
}