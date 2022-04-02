using Autofac;
using MediatR.Extensions.Autofac.DependencyInjection;

namespace MagicBinder.Application.CompositionRoots;

public class ApplicationCompositionRoot : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterMediatR(typeof(ApplicationCompositionRoot).Assembly);
    }
}