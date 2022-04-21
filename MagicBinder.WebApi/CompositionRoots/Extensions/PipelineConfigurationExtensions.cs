using MagicBinder.Core.CompositionRoots;
using MagicBinder.WebApi.CompositionRoots.Configurations;
using MagicBinder.WebApi.Middleware;

namespace MagicBinder.WebApi.CompositionRoots.Extensions;

public static class PipelineConfigurationExtensions
{
    public static IApplicationBuilder UseMvc(this WebApplication app)
    {
        app.UseHttpsRedirection();
        app.UseAuthentication();
        app.UseAuthorization();
        app.UseMiddleware<AuthenticatedRequestMiddleware>();
        app.MapControllers();

        return app;
    }

    public static IApplicationBuilder UseSwaggerWithUI(this IApplicationBuilder application, IConfiguration configuration)
    {
        var swagger = configuration.GetOptions<SwaggerConfig>();
        if (swagger.Enabled)
            application
                .UseSwagger()
                .UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "MagicBinder.WebApi v1"));

        return application;
    }
}