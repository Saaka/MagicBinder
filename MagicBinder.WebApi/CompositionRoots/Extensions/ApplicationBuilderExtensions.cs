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

    public static IApplicationBuilder UseCors(this IApplicationBuilder application, IConfiguration configuration)
    {
        var config = configuration.GetOptions<AuthConfig>();

        application.UseCors(x => x.AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials()
            .WithOrigins(config.AllowedOrigin));

        return application;
    }
}