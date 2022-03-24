using Autofac;

namespace MagicBinder.WebApi.CompositionRoots;

public static class Main
{
    public static ContainerBuilder RegisterAppModules(this ContainerBuilder builder)
    {
        builder.RegisterModule<WebApiCompositionRoot>();

        return builder;
    }
}