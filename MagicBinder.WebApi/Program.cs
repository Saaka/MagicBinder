using Autofac;
using Autofac.Extensions.DependencyInjection;
using MagicBinder.WebApi.CompositionRoots;
using Serilog;
using ILogger = Serilog.ILogger;

var builder = WebApplication.CreateBuilder(args);
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory())
    .ConfigureContainer<ContainerBuilder>(cb => cb.RegisterAppModules());

// Add services to the container.

builder.Logging.ClearProviders();
ILogger logger = new LoggerConfiguration().WriteTo.Console().CreateLogger();
builder.Services.AddSingleton(logger);
builder.Logging.AddSerilog(logger);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();