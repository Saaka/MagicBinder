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

    public static IApplicationBuilder UseSwaggerWithUI(this IApplicationBuilder application)
    {
        application
            .UseSwagger()
            .UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "MagicBinder.WebApi v1"));

        return application;
    }
}