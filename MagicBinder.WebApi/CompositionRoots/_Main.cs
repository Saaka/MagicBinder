﻿using Autofac;
using MagicBinder.Infrastructure.CompositionRoots;

namespace MagicBinder.WebApi.CompositionRoots;

public static class Main
{
    public static ContainerBuilder RegisterAppModules(this ContainerBuilder builder)
    {
        builder.RegisterModule<WebApiCompositionRoot>();
        builder.RegisterModule<InfrastructureCompositionRoot>();

        return builder;
    }
}