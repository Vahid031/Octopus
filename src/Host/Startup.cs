using Microsoft.AspNetCore.Identity;
using Octopus.Catalog.Core.Application;
using Octopus.Catalog.Core.Domain;
using Octopus.Catalog.Core.Mongo;
using Octopus.Catalog.Presentation.Http;
using Octopus.Host.Middlewares;
using Octopus.Presentation.Http;
using Octopus.UserManagement.Core.Identity;

namespace Octopus.Host;

public class Startup
{
    private readonly IConfiguration _configuration;
    private readonly IWebHostEnvironment _env;

    public Startup(IConfiguration configuration, IWebHostEnvironment env)
    {
        _configuration = configuration;
        _env = env;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        services
            .AddMongoServices(_configuration)
            .AddHttpServices()
            .AddUserManagementIdentityServices()
            //catalog
            .AddCatalogDomainServices()
            .AddCatalogApplicationServices()
            .AddCatalogMongoServices()
            .AddCatalogHttpServices();


        services.AddSwaggerGen();

        //services.AddAutoMapper(cfg => { cfg.ShouldUseConstructor = constructor => constructor.IsPublic; },
        //    typeof(Squidward.Application.AssemblyPointer).Assembly, typeof(AssemblyPointer).Assembly);

        //services.AddHealthChecks()
        //    .AddNautilusHealthChecks(_configuration);

        services.AddSingleton<OctopusExceptionHandlerMiddleware>();

        ////distribution tracing
        //if (_configuration.GetValue<bool>("Telemetry:IsEnable"))
        //{
        //    var otel = services.AddOpenTelemetry();

        //    otel.ConfigureResource(resource => resource
        //        .AddService(serviceName: AppInfo.ServiceName, serviceVersion: AppInfo.ServiceVersion));

        //    otel.WithMetrics(metrics => metrics
        //        .AddMeter("Microsoft.Orleans")
        //        .AddMeter("orleans.runtime.graincall")
        //        .AddAspNetCoreInstrumentation()
        //        .AddMeter("Microsoft.AspNetCore.Hosting")
        //        .AddMeter("Microsoft.AspNetCore.Server.Kestrel")
        //        .AddPrometheusExporter());

        //    otel.WithTracing(tracing =>
        //    {
        //        tracing.AddAspNetCoreInstrumentation(aspOptions =>
        //        {
        //            aspOptions.Filter = httpContext =>
        //                !$"{httpContext.Request.Path}".StartsWith("/__", StringComparison.OrdinalIgnoreCase) &&
        //                !$"{httpContext.Request.Path}".Contains("swagger") &&
        //                !$"{httpContext.Request.Path}".Contains("DashboardCounters") &&
        //                !$"{httpContext.Request.Path}".Contains("assets") &&
        //                !$"{httpContext.Request.Path}".Contains("grainState") &&
        //                !$"{httpContext.Request.Path}".Contains("sentry")
        //                ;
        //        });
        //        tracing.AddSource("Microsoft.Orleans.Application");
        //        tracing.AddHttpClientInstrumentation(httpOptions =>
        //        {
        //            var ignoredUris = _configuration.GetIgnoredOutgoingTelemetryUris();

        //            httpOptions.FilterHttpRequestMessage = httpMessage =>
        //            {
        //                var isIgnored = ignoredUris
        //                    .Any(iu => $"{httpMessage.RequestUri}"
        //                        .StartsWith(iu, StringComparison.OrdinalIgnoreCase));

        //                return !isIgnored;
        //            };

        //        });
        //        tracing.AddGrpcClientInstrumentation();
        //        tracing.AddSqlClientInstrumentation(opt => opt.SetDbStatementForText = true);
        //        var tracingOtlpEndpoint = _configuration.GetValue<string>("Telemetry:Exporters:OtlpAddress");
        //        if (!string.IsNullOrWhiteSpace(tracingOtlpEndpoint))
        //            tracing.AddOtlpExporter(otlpOptions =>
        //            {
        //                otlpOptions.Endpoint = new Uri(tracingOtlpEndpoint);
        //            });
        //        if (_configuration.GetValue<bool>("Telemetry:Exporters:Console"))
        //            tracing.AddConsoleExporter();
        //    });
        //}
    }

    public void Configure(IApplicationBuilder app, ILogger<Startup> logger)
    {
        logger.LogInformation($"App '{AppInfo.ServiceName}' starting");

        //app.UsePersistenceMongoDb();

        app.UseMiddleware<OctopusExceptionHandlerMiddleware>();

        var allowedOrigins = _configuration.GetSection("AllowedOrigins").Get<string[]>() ?? Array.Empty<string>();
        if (allowedOrigins.Any())
            app.UseCors(policy => policy
                .WithOrigins(allowedOrigins)
                .WithHeaders("Authorization", "Content-Type")
                .WithMethods("GET", "POST", "PUT", "PATCH", "DELETE")
                .WithExposedHeaders("Content-Disposition"));

        //app.UseHttpMetrics();

        app.UseRouting();

        //app.UseSwagger();
        //app.UseSwaggerUI();

        app.UseAuthentication();
        app.UseAuthorization();

        app.UseEndpoints(routeBuilder =>
        {
            routeBuilder.MapGet("/__about/version", () => AppInfo.ServiceVersion);
            //routeBuilder.MapNautilusHealthChecks();
            //routeBuilder.MapMetrics();
            routeBuilder.MapIdentityApi<IdentityUser>();
            routeBuilder.MapControllers();

            routeBuilder.MapPost("/logout", async (SignInManager<IdentityUser> signInManager) =>
            {
                await signInManager.SignOutAsync().ConfigureAwait(false);
            }).RequireAuthorization(); // So that only authorized users can use this endpoint
        });

        logger.LogInformation($"App '{AppInfo.ServiceName}' started");
    }
}
