using Autofac;
using Microsoft.Extensions.Configuration;

namespace MagicBinder.Core.CompositionRoots;

public static class CompositionRootExtensions
{
    public static ContainerBuilder RegisterConfigurationsForAssemblyOfType<T>(this ContainerBuilder builder)
    {
        var configTypes = typeof(T).Assembly.GetTypes()
            .Where(t => t.Name.EndsWith("Config", StringComparison.InvariantCulture));

        foreach (var configType in configTypes)
        {
            builder.RegisterConfiguration(configType);
        }

        return builder;
    }

    public static TModel GetOptions<TModel>(this IConfiguration configuration, string? sectionName = null)
        where TModel : new()
    {
        if (string.IsNullOrEmpty(sectionName))
            sectionName = typeof(TModel).GetConfigSectionName();

        var model = new TModel();
        configuration.GetSection(sectionName).Bind(model);
        return model;
    }

    private static void RegisterConfiguration(this ContainerBuilder builder, Type configType)
    {
        builder.Register(c =>
            {
                var configuration = c.Resolve<IConfiguration>();
                var configSectionName = configType.GetConfigSectionName();

                return configuration.GetSection(configSectionName).Get(configType);
            })
            .As(configType)
            .SingleInstance();
    }

    private static string GetConfigSectionName(this Type configType) => configType.Name.Replace("Config", "");
}