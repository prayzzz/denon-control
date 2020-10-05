using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace DenonControl.Web.Devices
{
    public class DeviceLocatorHostedService : IHostedService, IDisposable
    {
        private readonly DeviceLocator _deviceLocator;
        private readonly ILogger<DeviceLocatorHostedService> _logger;
        private Timer? _timer;

        public DeviceLocatorHostedService(ILogger<DeviceLocatorHostedService> logger, DeviceLocator deviceLocator)
        {
            _logger = logger;
            _deviceLocator = deviceLocator;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }

        public Task StartAsync(CancellationToken stoppingToken)
        {
            _timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromSeconds(60));
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken stoppingToken)
        {
            _timer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }

        private async void DoWork(object? state)
        {
            try
            {
                _logger.LogDebug("Searching for Devices");
                await _deviceLocator.SearchForDevices();
                _logger.LogDebug("Done searching for Devices");
            }
            catch (Exception e)
            {
                _logger.LogError("Search for devices failed", e);
            }
        }
    }
}