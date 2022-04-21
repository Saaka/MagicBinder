using Autofac;
using Autofac.Extensions.DependencyInjection;
using MagicBinder.WebApi.CompositionRoots;
using MagicBinder.WebApi.CompositionRoots.Extensions;

var builder = WebApplication.CreateBuilder(args);
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory())
    .ConfigureContainer<ContainerBuilder>(cb => cb.RegisterAppModules());

builder
    .AddLogging()
    .AddMvc()
    .AddJwtTokenBearerAuthentication()
    .AddSwagger()
    .AddHangfire();

var app = builder.Build();

app.UseCors(app.Configuration);
app.UseSwaggerWithUI();
app.UseHangfire(app.Configuration);
app.UseMvc();

await app.RunInitializationAsync();

app.Run();