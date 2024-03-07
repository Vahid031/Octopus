using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Octopus.Host.HealthChecks;

public static class HealthCheckExtensions
{
    public static IHealthChecksBuilder AddNautilusHealthChecks(this IHealthChecksBuilder builder,
        IConfiguration configuration)
    {
        return builder
            .AddCheck("live", () => HealthCheckResult.Healthy(), new[] { "live" });
        //.AddMongoDb(name: "mongodb", failureStatus: HealthStatus.Unhealthy,
        //	mongodbConnectionString: configuration.GetSection(nameof(MongoOptions)).Get<MongoOptions>()!.Url,
        //	tags: new[] {"ready", "database"})
        //.ForwardToPrometheus();
    }

    public static void MapNautilusHealthChecks(this IEndpointRouteBuilder app)
    {
        app.MapHealthCheck("/__health/live", "live");
        app.MapHealthCheck("/__health/ready", "ready");
        app.MapHealthCheck("/__health", null);
    }

    private static void MapHealthCheck(this IEndpointRouteBuilder app, string route, string tag)
    {
        //app.MapHealthChecks(route, new HealthCheckOptions
        //{
        //	Predicate = reg => string.IsNullOrWhiteSpace(tag) || reg.Tags.Contains(tag),
        //	ResultStatusCodes = Result.GetStatusCodes()
        //});

        //app.MapHealthChecks($"{route}.json", new HealthCheckOptions
        //{
        //	Predicate = reg => string.IsNullOrWhiteSpace(tag) || reg.Tags.Contains(tag),
        //	ResultStatusCodes = Result.GetStatusCodes(),

        //	ResponseWriter = ResponseWriter.Write
        //});
    }
}
