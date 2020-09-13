using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DenonControl.Web.Devices
{
    public class DeviceInformationService
    {
        private readonly Commander _commander;

        public DeviceInformationService(Commander commander)
        {
            _commander = commander;
        }

        public async Task<DeviceInformation> GetDeviceInformation(DenonDevice denonDevice)
        {
            var deviceInformation = new DeviceInformation(
                await GetMuteState(denonDevice),
                await GetMasterVolume(denonDevice),
                await GetMainZoneState(denonDevice),
                await GetZone2State(denonDevice)
            );

            return deviceInformation;
        }

        private async Task<ZoneState> GetMainZoneState(DenonDevice denonDevice)
        {
            var state = await _commander.GetMainZoneState(denonDevice);
            return state switch
            {
                "ZMOFF" => ZoneState.Off,
                "ZMON" => ZoneState.On,
                _ => ZoneState.Unknown
            };
        }

        private async Task<ZoneState> GetZone2State(DenonDevice denonDevice)
        {
            var state = await _commander.GetZone2State(denonDevice);
            return state switch
            {
                "Z2OFF" => ZoneState.Off,
                "Z2ON" => ZoneState.On,
                _ => ZoneState.Unknown
            };
        }

        private async Task<string> GetMasterVolume(DenonDevice denonDevice)
        {
            var value = await _commander.GetMasterVolume(denonDevice);
            var match = Regex.Match(value, ".*MV(?<volume>[0-9]+).*");

            return match.Success ? match.Groups["volume"].Value : "-1";
        }

        private async Task<MuteState> GetMuteState(DenonDevice denonDevice)
        {
            var state = await _commander.GetMuteState(denonDevice);

            return state switch
            {
                "MUON" => MuteState.MuteOn,
                "MUOFF" => MuteState.MuteOff,
                _ => MuteState.Unknown
            };
        }
    }
}