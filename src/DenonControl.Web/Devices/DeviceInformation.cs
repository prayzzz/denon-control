namespace DenonControl.Web.Devices
{
    public enum MuteState
    {
        Unknown,
        MuteOff,
        MuteOn
    }

    public enum ZoneState
    {
        Unknown,
        Off,
        On
    }
    
    public class DeviceInformation
    {
        public DeviceInformation(MuteState muteState, string masterVolume, ZoneState mainZoneState, ZoneState zone2State, string subwooferLevel)
        {
            MuteState = muteState;
            MasterVolume = masterVolume;
            MainZoneState = mainZoneState;
            Zone2State = zone2State;
            SubwooferLevel = subwooferLevel;
        }

        public MuteState MuteState { get; }
        public string MasterVolume { get; }
        public ZoneState MainZoneState { get; }
        public ZoneState Zone2State { get; }
        public string SubwooferLevel { get; set; }
    }
}