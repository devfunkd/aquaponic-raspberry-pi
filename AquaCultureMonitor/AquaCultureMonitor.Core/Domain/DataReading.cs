using System;

namespace AquaCultureMonitor.Core.Domain
{
    public class DataReading
    {
        public Guid Id { get; set; }
        public Guid SensorId { get; set; }
        public DateTime DateTime { get; set; }
        public decimal MetaValue { get; set; }

        public virtual Sensor Sensor { get; set; }
    }
}
