using System;
using System.Collections.Generic;

namespace ThermoTracker.Service.Models
{
    public partial class ClientIpAddresses
    {
        public ClientIpAddresses()
        {
            Devices = new HashSet<Devices>();
        }

        public int Id { get; set; }
        public string Address { get; set; }
        public DateTime FirstSeen { get; set; }
        public DateTime LastSeen { get; set; }

        public virtual ICollection<Devices> Devices { get; set; }
    }
}
