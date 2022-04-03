using Hangfire;
using MagicBinder.Core.CompositionRoots;
using MagicBinder.Infrastructure.Configurations;

namespace MagicBinder.WebApi.CompositionRoots.Extensions;

public static class ApplicationBuilderExtensions
{
    public static IApplicationBuilder UseHangfire(this IApplicationBuilder application, IConfiguration configuration)
    {
        var hangfireConfig = configuration.GetOptions<HangfireConfig>();
        if (hangfireConfig.DashboardEnabled)
            application.UseHangfireDashboard();

        return application;
    }
}