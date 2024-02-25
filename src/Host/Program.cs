using Octopus.Host;
using Serilog;

Console.Title = $"{AppInfo.ServiceName}  --->  {Directory.GetCurrentDirectory()}";

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateBootstrapLogger();

try
{
    var builder = WebApplication.CreateBuilder(args);

    Console.WriteLine();
    Console.WriteLine(
        $"**************** Environment: '{builder.Environment.EnvironmentName}' ****************");
    Console.WriteLine();

    var configuration = builder.Configuration;
    configuration.Sources.Clear();
    configuration.SetBasePath(Directory.GetCurrentDirectory());

    configuration.AddJsonFile("appsettings.json", false, true);
    configuration.AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", false, true);

    configuration.AddEnvironmentVariables();

    builder.Host.UseSerilog((context, serviceProvider, loggerConfiguration) => loggerConfiguration
        .Enrich.WithProperty("serviceName", AppInfo.ServiceName)
        .Enrich.WithProperty("serviceVersion", AppInfo.ServiceVersion)
        .Enrich.WithProperty("env", builder.Environment.EnvironmentName)
        //.Enrich.With<TelemetryEventEnricher>()
        .ReadFrom.Configuration(context.Configuration)
        .ReadFrom.Services(serviceProvider)
    );

    var startup = new Startup(builder.Configuration, builder.Environment);
    startup.ConfigureServices(builder.Services);

    builder.Host.UseConsoleLifetime();

    var app = builder.Build();
    var logger = app.Services.GetRequiredService<ILogger<Startup>>();

    startup.Configure(app, logger);

    await app.RunAsync();
}
catch (Exception ex)
{
    Log.Fatal(ex, $"App '{AppInfo.ServiceName}' startup failed");
    throw;
}
finally
{
    Log.Information($"App '{AppInfo.ServiceName}' shutting down");
    await Log.CloseAndFlushAsync();
}
