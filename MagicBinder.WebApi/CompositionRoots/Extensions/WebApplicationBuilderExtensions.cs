using Hangfire;
using Hangfire.Mongo;
using Hangfire.Mongo.Migration.Strategies;
using MagicBinder.Core.CompositionRoots;
using MagicBinder.Infrastructure.Configurations;
using MagicBinder.WebApi.Filters;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using ILogger = Serilog.ILogger;

namespace MagicBinder.WebApi.CompositionRoots.Extensions;

public static class WebApplicationBuilderExtensions
{
    public static WebApplicationBuilder AddMvc(this WebApplicationBuilder builder)
    {
        var config = builder.Configuration.GetOptions<AuthConfig>();

        builder.Services
            .AddCors(opt =>
            {
                opt.AddDefaultPolicy(b =>
                {
                    b.AllowCredentials()
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .WithOrigins(config.AllowedOrigin);
                });
            })
            .AddControllers(opt => { opt.Filters.Add<CustomExceptionFilter>(); });

        builder.Services
            .Configure<ApiBehaviorOptions>(opt => opt.SuppressModelStateInvalidFilter = true)
            .AddHttpContextAccessor();

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

    public static WebApplicationBuilder AddHangfire(this WebApplicationBuilder builder)
    {
        var mongoConfig = builder.Configuration.GetOptions<MongoConfig>();
        var hangfireConfig = builder.Configuration.GetOptions<HangfireConfig>();

        builder.Services.AddHangfire(config =>
        {
            config
                .UseSerilogLogProvider()
                .UseRecommendedSerializerSettings()
                .UseSimpleAssemblyNameTypeSerializer()
                .UseMongoStorage(mongoConfig.ConnectionString, hangfireConfig.DatabaseName, new MongoStorageOptions
                {
                    MigrationOptions = new MongoMigrationOptions
                    {
                        MigrationStrategy = new MigrateMongoMigrationStrategy()
                    }
                });
        });

        builder.Services.AddHangfireServer();

        return builder;
    }
}