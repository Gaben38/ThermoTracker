using System;
using System.Collections.Generic;

namespace ThermoTracker.Service.Models
{
    public partial class Devices
    {
        public Devices()
        {
            TemperatureRecords = new HashSet<TemperatureRecords>();
        }

        public int Id { get; set; }
        public string UserAgent { get; set; }
        public int IpId { get; set; }

        public virtual ClientIpAddresses Ip { get; set; }
        public virtual ICollection<TemperatureRecords> TemperatureRecords { get; set; }
    }
}
