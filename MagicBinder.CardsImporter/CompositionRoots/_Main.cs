using Autofac;
using MagicBinder.Application.CompositionRoots;
using MagicBinder.Infrastructure.CompositionRoots;

namespace MagicBinder.CardsImporter.CompositionRoots;

public static class Main
{
    public static ContainerBuilder RegisterAppModules(this ContainerBuilder builder)
    {
        builder.RegisterModule<CardsImporterCompositionRoot>();
        builder.RegisterModule<InfrastructureCompositionRoot>();
        builder.RegisterModule<ApplicationCompositionRoot>();

        return builder;
    }
}