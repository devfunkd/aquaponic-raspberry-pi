using System.Collections.Generic;
using AquaCultureMonitor.Core.Domain;

namespace AquaCultureMonitor.Models
{
    public class DashboardModel
    {
        public Sensor Sensor { get; set; }
        public string Icon { get { return GetIcon(); }}
        public string Symbol { get { return GetSymbol(); }}
        public IEnumerable<DataReading> Readings { get; set; }

        private string GetIcon()
        {
            switch (Sensor.Type)
            {
                case SensorType.Ph:
                    return "fa-unlink";

                case SensorType.Temperature:
                    return "fa-cloud";

                case SensorType.Humidity:
                    return "fa-tint";

                default:
                    return "";
            }
        }

        private string GetSymbol()
        {
            switch (Sensor.Type)
            {
                case SensorType.Ph:
                    return "";

                case SensorType.Temperature:
                    return "&deg;";

                case SensorType.Humidity:
                    return "&deg;";

                default:
                    return "";
            }
        }
    }

}