using Serilog;
using ILogger = Serilog.ILogger;

namespace MagicBinder.WebApi.CompositionRoots;

public static class WebApplicationBuilderExtensions
{
    public static WebApplicationBuilder AddLogging(this WebApplicationBuilder builder)
    {
        builder.Logging.ClearProviders();
        ILogger logger = new LoggerConfiguration()
            .WriteTo.Console()
            .CreateLogger();

        builder.Logging.AddSerilog(logger);

        return builder;
    }
}