using Serilog;
using ILogger = Serilog.ILogger;

namespace MagicBinder.WebApi.CompositionRoots.Extensions;

public static class WebApplicationBuilderExtensions
{
    public static WebApplicationBuilder AddMvc(this WebApplicationBuilder builder)
    {
        builder.Services.AddControllers();
        
        return builder;
    }
    public static WebApplicationBuilder AddLogging(this WebApplicationBuilder builder)
    {
        builder.Logging.ClearProviders();
        ILogger logger = new LoggerConfiguration()
            .WriteTo.Console()
            .CreateLogger();

        builder.Logging.AddSerilog(logger);

        return builder;
    }

    public static WebApplicationBuilder AddSwagger(this WebApplicationBuilder builder)
    {
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        return builder;
    }
}