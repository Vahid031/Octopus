namespace Octopus.Host;

public static class AppInfo
{
    static AppInfo()
    {
        ServiceVersion = $"{typeof(AppInfo).Assembly.GetName().Version}";
    }

    public static string ServiceVersion { get; }

    public const string ServiceName = "octopus";
}