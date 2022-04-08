using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddSpaStaticFiles(cfg => { cfg.RootPath = "ClientApp/build"; });

var app = builder.Build();

if (app.Environment.IsProduction())
    app.UseHttpsRedirection()
        .UseHsts();

app.UseStaticFiles()
    .UseSpaStaticFiles();
app.UseSpa(spa =>
{
    spa.Options.SourcePath = "ClientApp";
    if (app.Environment.IsDevelopment())
        spa.UseReactDevelopmentServer(npmScript: "start");
});

app.Run();