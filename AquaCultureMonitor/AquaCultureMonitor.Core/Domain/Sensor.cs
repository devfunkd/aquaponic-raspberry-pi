using System;

namespace AquaCultureMonitor.Core.Domain
{
    public class Sensor
    {
        public Guid Id { get; set; }
        public Guid ApiKey { get; set; }
        public SensorType Type { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }

        public string Symbol
        {
            get
            {
                switch (Type)
                {
                    case SensorType.Humidity:
                        return "%";

                    case SensorType.Temperature:
                        return "°C";

                    case SensorType.Ph:
                        return "";

                    case SensorType.Flow:
                        return "L/s";

                    default:
                        return "";
                }
            }
        }
    }

    public enum SensorType
    {
        Ph = 1,
        Temperature = 2,
        Humidity = 3,
        Flow = 4
    }


}
