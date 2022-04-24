using Autofac;
using FluentValidation;
using MediatR.Extensions.Autofac.DependencyInjection;

namespace MagicBinder.Application.CompositionRoots;

public class ApplicationCompositionRoot : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        var assembly = typeof(ApplicationCompositionRoot).Assembly;
        builder.RegisterMediatR(assembly);
        
        builder.RegisterAssemblyTypes(assembly)
            .Where(t => t.IsClosedTypeOf(typeof(IValidator<>)))
            .AsImplementedInterfaces();
    }
}