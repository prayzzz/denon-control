using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Rssdp;

namespace DenonControl.Web.Devices
{
    public delegate void SearchForDevicesFinished();

    public class DeviceLocator
    {
        private static readonly List<string> AllowedDeviceTypes = new List<string>
        {
            "urn:schemas-denon-com:device:AiosDevice:1"
        };

        private readonly ILogger<DeviceLocator> _logger;

        public DeviceLocator(ILogger<DeviceLocator> logger)
        {
            _logger = logger;
        }

        public IEnumerable<DenonDevice> Devices { get; private set; } = new List<DenonDevice>();
        public SearchForDevicesFinished? OnSearchForDevicesFinished { get; set; }

        public async Task SearchForDevices()
        {
            using var deviceLocator = new SsdpDeviceLocator();
            var foundDevices = await deviceLocator.SearchAsync(TimeSpan.FromSeconds(5));

            var denonDevices = new List<DenonDevice>();
            foreach (var foundDevice in foundDevices)
            {
                var fullDevice = await foundDevice.GetDeviceInfo();
                _logger.LogDebug("Found " + foundDevice.Usn + " at " + foundDevice.DescriptionLocation);

                if (AllowedDeviceTypes.Contains(fullDevice.FullDeviceType)) denonDevices.Add(new DenonDevice(fullDevice.FriendlyName, foundDevice.DescriptionLocation.Host));
            }

            Devices = denonDevices.Distinct(DenonDevice.IpComparer);

            OnSearchForDevicesFinished?.Invoke();
        }
    }
}