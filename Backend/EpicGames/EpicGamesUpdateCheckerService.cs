using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;

public class EpicGamesUpdateCheckerService : IHostedService, IDisposable
{
    private Timer? _timer;
    private EpicGamesUpdateCheck _epicCheckController = new();

    public Task StartAsync(CancellationToken cancellationToken)
    {
        _timer = new Timer(_ => DoWork(), null, TimeSpan.Zero, TimeSpan.FromSeconds(30));
        return Task.CompletedTask;
    }

    private void DoWork()
    {
        _epicCheckController.EpicGamesUpdateChecker();
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _timer?.Change(Timeout.Infinite, 0);
        return Task.CompletedTask;
    }

    public void Dispose()
    {
        _timer?.Dispose();
    }
}
