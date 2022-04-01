using Autofac;
using MagicBinder.Infrastructure.CompositionRoots;

namespace MagicBinder.CardsImporter.CompositionRoots;

public static class Main
{
    public static ContainerBuilder RegisterAppModules(this ContainerBuilder builder)
    {
        builder.RegisterModule<CardsImporterCompositionRoot>();
        builder.RegisterModule<InfrastructureCompositionRoot>();

        return builder;
    }
}