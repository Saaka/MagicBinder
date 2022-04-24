using System.Text;
using FluentValidation.AspNetCore;
using Hangfire;
using Hangfire.Mongo;
using Hangfire.Mongo.Migration.Strategies;
using MagicBinder.Application.CompositionRoots;
using MagicBinder.Core.CompositionRoots;
using MagicBinder.Infrastructure.Configurations;
using MagicBinder.WebApi.CompositionRoots.Configurations;
using MagicBinder.WebApi.Filters;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
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
            .AddControllers(opt => { opt.Filters.Add<CustomExceptionFilter>(); })
            .AddFluentValidation(c =>
            {
                c.RegisterValidatorsFromAssembly(typeof(ApplicationCompositionRoot).Assembly);
                c.LocalizationEnabled = false;
                c.DisableDataAnnotationsValidation = true;
            });

        builder.Services
            .Configure<ApiBehaviorOptions>(opt => opt.SuppressModelStateInvalidFilter = true)
            .AddHttpContextAccessor();

        return builder;
    }

    public static WebApplicationBuilder AddJwtTokenBearerAuthentication(this WebApplicationBuilder builder)
    {
        var config = builder.Configuration.GetOptions<AuthConfig>();

        var key = Encoding.ASCII.GetBytes(config.Secret);

        builder.Services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),

                    ValidateIssuer = true,
                    ValidIssuer = config.Issuer,

                    ValidateAudience = false
                };
            })
            ;

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
        var swagger = builder.Configuration.GetOptions<SwaggerConfig>();

        if (swagger.Enabled)
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "MagicBinder.WebApi", Version = "v1" });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Scheme = "bearer"
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }
                });

                Directory
                    .GetFiles(AppContext.BaseDirectory, "*.xml", SearchOption.TopDirectoryOnly)
                    .ToList().ForEach(x => c.IncludeXmlComments(x));
            });

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