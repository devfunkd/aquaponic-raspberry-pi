using System;
using Microsoft.AspNet.SignalR;

namespace AquaCultureMonitor.Hubs
{
    public class SensorHub : Hub
    {
        public static void Update(Guid sensorId, string sensorValue, string sensorDateTime)
        {
            IHubContext context = GlobalHost.ConnectionManager.GetHubContext<SensorHub>();
            context.Clients.All.updateSensor(sensorId, sensorValue, sensorDateTime);
        }
    }
}