using System.Collections.Generic;

namespace DenonControl.Web.Devices
{
    public class DenonDevice
    {
        public static IEqualityComparer<DenonDevice> IpComparer { get; } = new IpEqualityComparer();
        
        public DenonDevice(string name, string ip)
        {
            Name = name;
            Ip = ip;
        }

        public string Name { get; }
        public string Ip { get; }

        public override string ToString()
        {
            return $"{nameof(Name)}: {Name}, {nameof(Ip)}: {Ip}";
        }

        private sealed class IpEqualityComparer : IEqualityComparer<DenonDevice>
        {
            public bool Equals(DenonDevice? x, DenonDevice? y)
            {
                if (ReferenceEquals(x, y)) return true;
                if (ReferenceEquals(x, null)) return false;
                if (ReferenceEquals(y, null)) return false;
                if (x.GetType() != y.GetType()) return false;
                return x.Ip == y.Ip;
            }

            public int GetHashCode(DenonDevice obj)
            {
                return obj.Ip.GetHashCode();
            }
        }
    }
}