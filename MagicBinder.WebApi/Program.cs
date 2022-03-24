using Autofac;
using Autofac.Extensions.DependencyInjection;
using MagicBinder.WebApi.CompositionRoots;
using MagicBinder.WebApi.CompositionRoots.Extensions;

var builder = WebApplication.CreateBuilder(args);
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory())
    .ConfigureContainer<ContainerBuilder>(cb => cb.RegisterAppModules());

builder.AddLogging();
builder.AddMvc();
builder.AddSwagger();

var app = builder.Build();

app.UseSwaggerWithUI();
app.UseMvc();

app.Run();