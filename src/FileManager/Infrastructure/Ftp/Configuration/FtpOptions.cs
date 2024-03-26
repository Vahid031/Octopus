namespace Octopus.FileManager.Infrastructure.Ftp.Configuration;

public record FtpOptions
{
    public string Address { get; init; }

    public int Port { get; init; }

    public string UserName { get; init; }

    public string Password { get; init; }
}
