using Autofac;
using MagicBinder.Application.CompositionRoots;
using MagicBinder.Core.CompositionRoots;
using MagicBinder.Infrastructure.CompositionRoots;

namespace MagicBinder.WebApi.CompositionRoots;

public static class Main
{
    public static ContainerBuilder RegisterAppModules(this ContainerBuilder builder)
    {
        builder.RegisterModule<WebApiCompositionRoot>();
        builder.RegisterModule<InfrastructureCompositionRoot>();
        builder.RegisterModule<ApplicationCompositionRoot>();
        builder.RegisterModule<CoreCompositionRoot>();

        return builder;
    }
}