using Microsoft.Extensions.Hosting;

namespace Dinja.Tests.Container.Services;

[ServiceTypes.HostedService]
public class HostedService : BackgroundService
{
    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        return Task.CompletedTask;
    }
}