using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using PrimS.Telnet;

namespace DenonControl.Web.Devices
{
    public class Commander
    {
        private readonly ILogger<Commander> _logger;

        public Commander(ILogger<Commander> logger)
        {
            _logger = logger;
        }

        public async void Mute(DenonDevice denonPlayer)
        {
            await IssueCommand(denonPlayer.Ip, "MUON");
        }

        public async void Unmute(DenonDevice denonPlayer)
        {
            await IssueCommand(denonPlayer.Ip, "MUOFF");
        }

        public async Task<string> GetMuteState(DenonDevice denonPlayer)
        {
            return await IssueCommand(denonPlayer.Ip, "MU?");
        }

        public async void MainZoneOn(DenonDevice denonPlayer)
        {
            await IssueCommand(denonPlayer.Ip, "ZMON");
        }

        public async void MainZoneOff(DenonDevice denonPlayer)
        {
            await IssueCommand(denonPlayer.Ip, "ZMOFF");
        }

        public async Task<string> GetMainZoneState(DenonDevice denonPlayer)
        {
            return await IssueCommand(denonPlayer.Ip, "ZM?");
        }

        public async void Zone2On(DenonDevice denonPlayer)
        {
            await IssueCommand(denonPlayer.Ip, "Z2ON");
        }

        public async void Zone2Off(DenonDevice denonPlayer)
        {
            await IssueCommand(denonPlayer.Ip, "Z2OFF");
        }

        public async Task<string> GetZone2State(DenonDevice denonPlayer)
        {
            return await IssueCommand(denonPlayer.Ip, "Z2?");
        }

        public async Task SetMasterVolume(DenonDevice denonPlayer, int value)
        {
            value = Math.Min(Math.Max(value, 0), 98);
            await IssueCommand(denonPlayer.Ip, $"MV{value}");
        }

        public async Task<string> GetMasterVolume(DenonDevice denonPlayer)
        {
            return await IssueCommand(denonPlayer.Ip, "MV?");
        }

        private async Task<string> IssueCommand(string ip, string command)
        {
            using Client client = new Client(ip, 23, new CancellationToken());

            _logger.LogDebug($"Command: {command}");

            await client.WriteLine(command);
            var response = await client.TerminatedReadAsync(">", TimeSpan.FromMilliseconds(100));
            response = response.Split('\r')[0].Trim();

            _logger.LogDebug($"Response: {response}");

            return response;
        }
    }
}