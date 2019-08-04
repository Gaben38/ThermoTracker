using System;
using System.Collections.Generic;

namespace ThermoTracker.Service.Models
{
    public partial class TemperatureRecords
    {
        public int Id { get; set; }
        public decimal Temperature { get; set; }
        public DateTime Timestamp { get; set; }
        public int DeviceId { get; set; }

        public virtual Devices Device { get; set; }
    }
}
